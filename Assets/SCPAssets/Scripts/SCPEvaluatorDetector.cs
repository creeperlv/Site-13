using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{

    public class SCPEvaluatorDetector : MonoBehaviour
    {
        [HideInInspector]
        public List<SCPEntity> TargetEntities = new List<SCPEntity>();

        public float DeltaY = 0;
        public float DeltaX = 0;
        public float DeltaZ = 0;
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<SCPEntity>() != null)
            {
                TargetEntities.Add(other.GetComponent<SCPEntity>());
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<SCPEntity>() != null)
            {
                TargetEntities.Remove(other.GetComponent<SCPEntity>());
            }
        }

    }

}