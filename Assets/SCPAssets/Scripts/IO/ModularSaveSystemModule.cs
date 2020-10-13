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
        public void Load()
        {
            ByteBuffer TotalBuffer = SaveFileWR.Read((int)SaveFileWR.Length, 0);
            ByteBuffer[] Datas = TotalBuffer / 4;
            foreach (var item in TargetRecursiveObjectBytable)
            {
                ApplyBytablesRecursively(item, ref Datas[0]);
            }
            foreach (var item in TargetRecursiveObject)
            {
                ApplyTransformRecursively(item, ref Datas[1]);
            }
        }
        public void ApplyTransformRecursively(Transform transform, ref ByteBuffer buffer)
        {

        }
        public void ApplyBytablesRecursively(GameObject Object,ref ByteBuffer buffer)
        {

        }

        void Start()
        {
            SaveFileWR = new FileWR(new FileInfo(Path.Combine(((ModularSaveSystem)GameInfo.CurrentGame.CurrentSceneSaveSystem).SavePath, TargetSaveSystemID + ".bin")));
        }
        public void LoadByteableObjectRecursively(GameObject Father)
        {
            ByteBuffer vs = new ByteBuffer();
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
            TraverseTransform(TraverseTransformData);
            TraverseObjects(TraverseBytableObjectData);
            ByteBuffer TotalBuffer = new ByteBuffer();
            TotalBuffer *= RecursiveObjectsData;
            TotalBuffer *= RecursiveTransformData;
            TotalBuffer *= TraverseBytableObjectData;
            TotalBuffer *= TraverseTransformData;
            byte[] Data = TotalBuffer.GetTotalData();
            SaveFileWR.SetLength(0);
            SaveFileWR.Flush();
            SaveFileWR.WriteBytes(Data, Data.Length, 0);
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
            ByteBuffer vs = new ByteBuffer();
            vs.AppendGroup(Utilities.FromTransform(obj.transform));
            foreach (var item in c)
            {
                vs *= item.Serialize();
            }
            Buffer *= vs;
        }
        public ByteBuffer SaveBytablesRecursively(GameObject Father, ByteBuffer vs)
        {
            if (vs == null) vs = new ByteBuffer();
            AnalyzeObject(Father, ref vs);
            for (int i = 0; i < Father.transform.childCount; i++)
            {
                Father.transform.GetChild(i);
                SaveBytablesRecursively(Father, vs);
            }
            return vs;
        }
        public ByteBuffer TraverseTransform(ByteBuffer vs)
        {
            if (vs == null) vs = new ByteBuffer();
            foreach (var item in TargetNonRecursiveObject)
            {
                AnalyzeTransform(item, ref vs);
            }
            return vs;
        }
        public ByteBuffer TraverseObjects(ByteBuffer vs)
        {
            if (vs == null) vs = new ByteBuffer();
            foreach (var item in TargetNonRecursiveObjectBytable)
            {
                AnalyzeObject(item, ref vs);
            }
            return vs;
        }
        public ByteBuffer SaveTransformRecursively(Transform Father, ByteBuffer vs)
        {
            if (vs == null) vs = new ByteBuffer();
            AnalyzeTransform(Father, ref vs);
            for (int i = 0; i < Father.transform.childCount; i++)
            {
                Father.transform.GetChild(i);
                SaveTransformRecursively(Father, vs);
            }
            return vs;
        }
        public void Unregister()
        {

        }
        public void Register()
        {

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
            return new ByteBuffer();
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