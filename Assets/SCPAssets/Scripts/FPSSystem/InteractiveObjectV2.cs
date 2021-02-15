using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.FPSSystem
{

    public class InteractiveObjectV2 : MonoBehaviour
    {
        public InteractAction InteractAction;
        public string DisplayNameID;
        public string DisplayNameFallback;
        private void OnTriggerEnter(Collider other)
        {
            try
            {
                var kl = other.gameObject.GetComponent<SCPPlayerKeyListener>();
                if (kl != null)
                {
                    kl.AddInteractiveObject(this);
                }
            }
            catch { }
        }
        private void OnTriggerExit(Collider other)
        {
            try
            {
                var kl = other.gameObject.GetComponent<SCPPlayerKeyListener>();
                if (kl != null)
                {
                    kl.RemoveInteractiveObject(this);
                }
            }
            catch { }
        }
    }
    public enum InteractAction
    {
        SwitchWeapon, PickUp, Use, None, Check
    }
}