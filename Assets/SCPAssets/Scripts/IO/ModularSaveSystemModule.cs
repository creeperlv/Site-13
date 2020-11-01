using CLUNL.Data.Layer0.Buffers;
using CLUNL.DirectedIO;
using Site13Kernel.DynamicScene;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
namespace Site13Kernel.IO
{
    public partial class ModularSaveSystemModule : BaseModularSceneComponent, ISave
    {
        public int TargetSaveSystemID = -1;
        bool isRegistered = false;
        [Header("Transform Info Only")]
        public List<Transform> TargetRecursiveObject;
        [Header("Exclude Transform")]
        public List<GameObject> TargetRecursiveObjectBytable;
        [Header("Transform Info Only")]
        public List<Transform> TargetNonRecursiveObject;
        [Header("Exclude Transform")]
        public List<GameObject> TargetNonRecursiveObjectBytable;
        IBaseWR SaveFileWR;
        public ByteBuffer NULL_BUFFER = new ByteBuffer();
        public void Load()
        {
            if (SaveFileWR.Length == 0) return;
            ByteBuffer TotalBuffer = SaveFileWR.Read((int)SaveFileWR.Length, 0);
            //Debug.Log(TotalBuffer.GetTotalData().Length+"/"+ (int)SaveFileWR.Length);
            ByteBuffer[] Datas = new ByteBuffer[4];
            Datas[0] = TotalBuffer.GetGroup();
            Datas[1] = TotalBuffer.GetGroup();
            Datas[2] = TotalBuffer.GetGroup();
            Datas[3] = TotalBuffer.GetGroup();
            if(Datas[0].GetTotalData().Length!=0)
            foreach (var item in TargetRecursiveObjectBytable)
            {
                ApplyBytablesRecursively(item, ref Datas[0]);
            }
            if(Datas[1].GetTotalData().Length!=0)
            foreach (var item in TargetRecursiveObject)
            {
                ApplyTransformRecursively(item, ref Datas[1]);
            }
            if(Datas[2].GetTotalData().Length!=0)
            ApplyBytableTranverse(Datas[2]);
            if(Datas[3].GetTotalData().Length!=0)
            ApplyTransformTranverse(Datas[3]);
        }
        public void ApplyTransformRecursively(Transform trans, ref ByteBuffer buffer)
        {

            var ep = trans.GetComponent<SaveSystemEndPoint>();
            if (ep != null)
            {
                if (ep.EndPointType == EndPointType.Exclusive)
                {
                    return;
                }
            }
            ApplyTransform(trans, ref buffer);
            if (ep.EndPointType == EndPointType.Inclusive) return;
            for (int i = 0; i < trans.childCount; i++)
            {
                ApplyTransformRecursively(trans.GetChild(i), ref buffer);
            }

        }
        public void ApplyBytablesRecursively(GameObject GObject, ref ByteBuffer buffer)
        {
            var ep = GObject.transform.GetComponent<SaveSystemEndPoint>();
            if (ep != null)
            {
                if (ep.EndPointType == EndPointType.Exclusive)
                {
                    return;
                }
            }
            ApplyBytable(GObject, ref buffer);
            if (ep != null)
                if (ep.EndPointType == EndPointType.Inclusive) return;
            for (int i = 0; i < GObject.transform.childCount; i++)
            {
                ApplyBytablesRecursively(GObject.transform.GetChild(i).gameObject, ref buffer);
            }
        }
        public void ApplyTransformTranverse(ByteBuffer buffer)
        {
            foreach (var item in TargetNonRecursiveObject)
            {
                ApplyTransform(item, ref buffer);
            }
        }
        public void ApplyBytableTranverse(ByteBuffer MainBuffer)
        {
            foreach (var item in TargetNonRecursiveObjectBytable)
            {
                ApplyBytable(item, ref MainBuffer);
            }
        }
        void ApplyBytable(GameObject obj, ref ByteBuffer ParentBuffer)
        {
            var items = obj.GetComponents<IByteBufferable>();
            ByteBuffer ObjectBuffer = ParentBuffer.GetGroup();
            foreach (var item in items)
            {
                ByteBuffer BufferableBuffe = ObjectBuffer.GetGroup();
                item.Deserialize(BufferableBuffe);
            }
        }
        void ApplyTransform(Transform trans, ref ByteBuffer buffer)
        {
            ByteBuffer vs = buffer.GetGroup();
            ResolveTransform(ref trans, ref vs);
        }
        void Start()
        {
            NULL_BUFFER = new ByteBuffer();
            NULL_BUFFER.AppendGroup(new byte[] { 0, 0, 0, 0 });
        }
        public void Save()
        {
            ByteBuffer RecursiveObjectsData = new ByteBuffer();
            ByteBuffer RecursiveTransformData = new ByteBuffer();
            ByteBuffer TraverseTransformData = new ByteBuffer();
            ByteBuffer TraverseBytableObjectData = new ByteBuffer();
            foreach (var item in TargetRecursiveObject)
            {
                RecursiveTransformData = SaveTransformRecursively(item, RecursiveTransformData);
            }
            foreach (var item in TargetRecursiveObjectBytable)
            {
                RecursiveObjectsData = SaveBytablesRecursively(item, RecursiveObjectsData);
            }
            TraverseTransform(ref TraverseTransformData);
            TraverseObjects(ref TraverseBytableObjectData);
            ByteBuffer TotalBuffer = new ByteBuffer();
            TotalBuffer.AppendGroup(RecursiveObjectsData);
            TotalBuffer.AppendGroup(RecursiveTransformData);
            TotalBuffer.AppendGroup(TraverseBytableObjectData);
            TotalBuffer.AppendGroup(TraverseTransformData);
            byte[] Data = TotalBuffer.GetTotalData();
            SaveFileWR.SetLength(0);
            SaveFileWR.Flush();
            SaveFileWR.WriteBytes(Data, Data.Length, 0);
            SaveFileWR.Flush();
        }
        public void AnalyzeTransform(Transform obj, ref ByteBuffer Buffer)
        {
            ByteBuffer vs = new ByteBuffer();
            vs.AppendGroup(Utilities.FromTransform(obj));
            Buffer *= vs;
        }
        public void AnalyzeObject(GameObject obj, ref ByteBuffer Buffer)
        {
            var c = obj.GetComponents<IByteBufferable>();
            ByteBuffer ObjectBuffer = new ByteBuffer();
            foreach (var item in c)
            {
                var b = item.Serialize();
                if (b != null)
                    ObjectBuffer.AppendGroup(b);
                else ObjectBuffer.AppendGroup(NULL_BUFFER);
            }
            Buffer.AppendGroup(ObjectBuffer);
        }
        public ByteBuffer SaveBytablesRecursively(GameObject Father, ByteBuffer vs)
        {
            var ep = Father.GetComponent<SaveSystemEndPoint>();
            if (ep != null)
            {
                if (ep.EndPointType == EndPointType.Exclusive)
                {
                    return vs;
                }
            }
            if (vs == null) vs = new ByteBuffer();
            AnalyzeObject(Father, ref vs);
            if (ep != null)
                if (ep.EndPointType == EndPointType.Inclusive) return vs;
            for (int i = 0; i < Father.transform.childCount; i++)
            {
                var item = Father.transform.GetChild(i);
                SaveBytablesRecursively(item.gameObject, vs);
            }
            return vs;
        }
        public ByteBuffer TraverseTransform(ref ByteBuffer vs)
        {
            if (vs == null) vs = new ByteBuffer();
            foreach (var item in TargetNonRecursiveObject)
            {
                AnalyzeTransform(item, ref vs);
            }
            return vs;
        }
        public ByteBuffer TraverseObjects(ref ByteBuffer vs)
        {
            if (vs == null)
                vs = new ByteBuffer();
            foreach (var item in TargetNonRecursiveObjectBytable)
            {
                AnalyzeObject(item, ref vs);
            }
            return vs;
        }
        public ByteBuffer SaveTransformRecursively(Transform Father, ByteBuffer vs)
        {
            var ep = Father.GetComponent<SaveSystemEndPoint>();
            if (ep != null)
            {
                if (ep.EndPointType == EndPointType.Exclusive)
                {
                    return vs;
                }
            }
            if (vs == null) vs = new ByteBuffer();
            AnalyzeTransform(Father, ref vs);
            if (ep.EndPointType == EndPointType.Inclusive) return vs;
            for (int i = 0; i < Father.transform.childCount; i++)
            {
                var item = Father.transform.GetChild(i);
                SaveTransformRecursively(item, vs);
            }
            return vs;
        }
        public void Unregister()
        {
                ((ModularSaveSystem)GameInfo.CurrentGame.CurrentSceneSaveSystem).Modules.Remove(TargetSaveSystemID + "");
            SaveFileWR.Flush();
            SaveFileWR.Dispose();
        }
        public void Register()
        {
            try
            {
                ((ModularSaveSystem)GameInfo.CurrentGame.CurrentSceneSaveSystem).Modules.Add(TargetSaveSystemID + "", this);
                var fi = new FileInfo(Path.Combine(((ModularSaveSystem)GameInfo.CurrentGame.CurrentSceneSaveSystem).SavePath, TargetSaveSystemID + ".bin"));

                if (!fi.Exists)
                {
                    fi.CreateText().Close();
                }
                SaveFileWR = new FileWR(fi);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        public override void Init()
        {
            Register();
        }

        public override void OnDispose()
        {
            Unregister();
        }

        public override void Deserialize(ByteBuffer Data)
        {
            //Remains Empty, Save Module should not be able to save. Avoid saving itself :P
        }

        public override ByteBuffer Serialize()
        {
            return null;
        }
    }
    public partial class Utilities
    {
        public static byte[] FromTransform(Transform transform)
        {
            //Amount: 3*4+4*4+3*4.
            byte[] vs = new byte[40];
            {
                var a = BitConverter.GetBytes(transform.position.x);
                vs[0] = a[0];
                vs[1] = a[1];
                vs[2] = a[2];
                vs[3] = a[3];
            }
            {
                var a = BitConverter.GetBytes(transform.position.y);
                vs[4] = a[0];
                vs[5] = a[1];
                vs[6] = a[2];
                vs[7] = a[3];
            }
            {
                var a = BitConverter.GetBytes(transform.position.z);
                vs[8] = a[0];
                vs[9] = a[1];
                vs[10] = a[2];
                vs[11] = a[3];
            }
            {
                var a = BitConverter.GetBytes(transform.rotation.x);
                vs[12] = a[0];
                vs[13] = a[1];
                vs[14] = a[2];
                vs[15] = a[3];
            }
            {
                var a = BitConverter.GetBytes(transform.rotation.y);
                vs[16] = a[0];
                vs[17] = a[1];
                vs[18] = a[2];
                vs[19] = a[3];
            }
            {
                var a = BitConverter.GetBytes(transform.rotation.z);
                vs[20] = a[0];
                vs[21] = a[1];
                vs[22] = a[2];
                vs[23] = a[3];
            }
            {
                var a = BitConverter.GetBytes(transform.rotation.w);
                vs[24] = a[0];
                vs[25] = a[1];
                vs[26] = a[2];
                vs[27] = a[3];
            }
            {
                var a = BitConverter.GetBytes(transform.localScale.x);
                vs[28] = a[0];
                vs[29] = a[1];
                vs[30] = a[2];
                vs[31] = a[3];
            }
            {
                var a = BitConverter.GetBytes(transform.localScale.y);
                vs[32] = a[0];
                vs[33] = a[1];
                vs[34] = a[2];
                vs[35] = a[3];
            }
            {
                var a = BitConverter.GetBytes(transform.localScale.z);
                vs[36] = a[0];
                vs[37] = a[1];
                vs[38] = a[2];
                vs[39] = a[3];
            }
            return vs;
        }
        public static (Vector3, Quaternion, Vector3) FromBytes(byte[] vs)
        {
            Vector3 P = Vector3.zero;
            {
                byte[] a = { vs[0], vs[1], vs[2], vs[3] };
                P.x = BitConverter.ToSingle(a, 0);
            }
            {
                byte[] a = { vs[4], vs[5], vs[6], vs[7] };
                P.y = BitConverter.ToSingle(a, 0);
            }
            {
                byte[] a = { vs[8], vs[9], vs[10], vs[11] };
                P.z = BitConverter.ToSingle(a, 0);
            }
            Quaternion R;
            {
                float a, b, c, d;
                {
                    byte[] e = { vs[12], vs[13], vs[14], vs[15] };
                    a = BitConverter.ToSingle(e, 0);
                }
                {
                    byte[] e = { vs[16], vs[17], vs[18], vs[19] };
                    b = BitConverter.ToSingle(e, 0);
                }
                {
                    byte[] e = { vs[20], vs[21], vs[22], vs[23] };
                    c = BitConverter.ToSingle(e, 0);
                }
                {
                    byte[] e = { vs[24], vs[25], vs[26], vs[27] };
                    d = BitConverter.ToSingle(e, 0);
                }
                R = new Quaternion(a, b, c, d);
            }

            Vector3 S = Vector3.zero;
            {
                byte[] a = { vs[28], vs[29], vs[30], vs[31] };
                P.x = BitConverter.ToSingle(a, 0);
            }
            {
                byte[] a = { vs[32], vs[33], vs[34], vs[35] };
                P.y = BitConverter.ToSingle(a, 0);
            }
            {
                byte[] a = { vs[36], vs[37], vs[38], vs[39] };
                P.z = BitConverter.ToSingle(a, 0);
            }
            return (P, R, S);
        }
    }
}