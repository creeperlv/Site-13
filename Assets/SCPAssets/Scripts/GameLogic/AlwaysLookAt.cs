using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Animations
{
    public class AlwaysLookAt : MonoBehaviour
    {
        public Transform Target;

        void Update()
        {
            this.transform.LookAt(Target);
        }
    }

}