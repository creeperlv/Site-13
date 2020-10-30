using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public class AutoDeactivateObject : MonoBehaviour
    {
        public float Cutdown =0;
        public bool Pause=false;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Cutdown > 0)
            {
                if(Pause==false)
                Cutdown -= Time.deltaTime;
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
    }

}