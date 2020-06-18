using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public class SCPResponsiveLight : MonoBehaviour
    {
        public GameObject Light;
        public List<MeshRenderer> Lights;
        public Material Close;
        public Material On;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<SCPEntity>()!= null){
                if (Light.activeSelf == false)
                {
                    Light.SetActive(true);
                    foreach (var item in Lights)
                    {
                        item.material = On;
                    }
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<SCPEntity>() != null)
            {
                if (Light.activeSelf == true)
                {
                    Light.SetActive(false);
                    foreach (var item in Lights)
                    {
                        item.material = Close;
                    }
                }
            }
        }
    }

}