using CLUNL.Data.Layer0.Buffers;
using Site13Kernel.DynamicScene;
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
            foreach (var item in TargetRecursiveObjectBytable)
            {

            }
        }

        public ByteBuffer SaveRecursively(GameObject Father)
        {
            ByteBuffer vs = new ByteBuffer();
            for (int i = 0; i < Father.transform.childCount; i++)
            {
                Father.transform.GetChild(i);
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

}