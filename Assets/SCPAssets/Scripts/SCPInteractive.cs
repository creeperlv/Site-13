using System.Collections;
using UnityEngine;
namespace Site13Kernel
{

    public class SCPInteractive : SCPBaseScript
    {
        [HideInInspector]
        public bool isOperating = false;
        [Tooltip("Checking this option will cause system to lock player's movement when interacting with current object.")]
        public bool isRequireLockCamera = false;
        public virtual IEnumerator Move()
        {
            yield break;
        }
        public virtual IEnumerator Move(GameObject player)
        {
            yield break;
        }

    }

}