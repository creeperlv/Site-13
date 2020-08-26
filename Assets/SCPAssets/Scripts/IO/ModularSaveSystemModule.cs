using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.IO
{
    public class ModularSaveSystemModule : MonoBehaviour,ISave
    {
        public List<GameObject> TargetRecursiveObject;
        public List<GameObject> TargetRecursiveObjectBytable;
        public List<GameObject> TargetNonRecursiveObject;
        public List<GameObject> TargetNonRecursiveObjectBytable;
        public void Load()
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        // Start is called before the first frame update
        void Start()
        {

        }
        public void Unregister()
        {

        }
        // Update is called once per frame
        void Update()
        {

        }
    }

}