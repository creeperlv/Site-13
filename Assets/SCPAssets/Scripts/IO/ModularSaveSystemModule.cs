using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.IO
{
    public class ModularSaveSystemModule : MonoBehaviour,ISave
    {
        public int TargetSaveSystemID = -1;
        bool isRegistered = false;
        public List<GameObject> TargetRecursiveObject;
        public List<GameObject> TargetRecursiveObjectBytable;
        public List<GameObject> TargetNonRecursiveObject;
        public List<GameObject> TargetNonRecursiveObjectBytable;
        public void Load()
        {
        }

        public void Save()
        {
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
    }

}