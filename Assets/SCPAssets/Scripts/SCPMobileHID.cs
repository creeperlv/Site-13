using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Weapon
{
    public class SCPMobileHID : MonoBehaviour
    {
        bool isHolding = false;
        GameObject shellTmp;
        public GameObject Shell;
        public GameObject OriPoint;
        public AudioSource AS;
        private void Update()
        {
            if (Input.GetButton("Fire1"))
                FirePrimary();
        }
        public void FirePrimary()
        {
            if (shellTmp == null)
            {
                isHolding = true;
                AS.Play();
                shellTmp = GameObject.Instantiate(Shell, OriPoint.transform);
            }
        }
    }

}