using CLUNL.Data.Layer0.Buffers;
using Site13Kernel.DynamicScene;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.IO
{
    public class ModularSaveSystemModule : BaseModularSceneComponent, ISave
    {
        public int TargetSaveSystemID = -1;
        bool isRegistered = false;
        [Header("Transform Info Only")]
        public List<GameObject> TargetRecursiveObject;
        [Header("Exclude Transform")]
        public List<GameObject> TargetRecursiveObjectBytable;
        [Header("Transform Info Only")]
        public List<GameObject> TargetNonRecursiveObject;
        [Header("Exclude Transform")]
        public List<GameObject> TargetNonRecursiveObjectBytable;

        public void Load()
        {

        }
        public void LoadRecursively(GameObject Father)
        {
            ByteBuffer vs = new ByteBuffer();
        }
        public void Save()
        {

            ByteBuffer RecursiveObjectsData = new ByteBuffer();
            foreach (var item in TargetRecursiveObjectBytable)
            {
                SaveRecursively(item, RecursiveObjectsData);
            }
        }
        public void AnalyzeObject(GameObject obj, ref ByteBuffer Buffer)
        {
            var c = obj.GetComponents<BytableBehavior>();
            ByteBuffer vs = new ByteBuffer();
            vs.AppendGroup(Utilities.FromTransform(obj.transform));
            foreach (var item in c)
            {
                vs = vs * item.Serialize();
            }
            Buffer = Buffer * vs;
        }
        public ByteBuffer SaveRecursively(GameObject Father, ByteBuffer vs)
        {
            if (vs == null) vs = new ByteBuffer();
            AnalyzeObject(Father, ref vs);
            for (int i = 0; i < Father.transform.childCount; i++)
            {
                Father.transform.GetChild(i);
                SaveRecursively(Father, vs);
            }
            return vs;
        }
        // Start is called before the first frame update
        void Start()
        {

        }
        public void Unregister()
        {

        }
        public void Register()
        {

        }
        // Update is called once per frame
        void Update()
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
    }
    public partial class Utilities
    {
        public static byte[] FromTransform(Transform transform)
        {
            //Amount: 3*4+4*4+3*4.
            byte[] vs = new byte[40];
            {
                var a=BitConverter.GetBytes(transform.position.x);
                vs[0] = a[0];
                vs[1] = a[1];
                vs[2] = a[2];
                vs[3] = a[3];
            }
            {
                var a=BitConverter.GetBytes(transform.position.y);
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
    }
}