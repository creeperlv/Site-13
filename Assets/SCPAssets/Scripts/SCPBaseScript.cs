using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{

    public class SCPBaseScript : MonoBehaviour
    {
        [HideInInspector]
        public bool isStarted = false;
        public virtual void StartScript()
        {

        }
        private void Start()
        {
            StartScript();
        }
    }

}