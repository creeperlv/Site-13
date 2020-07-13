﻿using Site13Kernel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.FPSSystem
{
    public class NonWeaponItem : MonoBehaviour, IHandItem
    {
        public void Fight()
        {
        }

        public string GetData()
        {
            return "";
        }

        public void Init(string data)
        {

        }

        public bool IsFightCompleted() => true;

        public bool IsFPSSystemV3Enabled() => true;

        public bool IsPrimaryCompleted() => true;

        public bool IsReloadCompleted() => true;
        public bool IsSecondaryCompleted() => true;

        public void Primary(ref bool isHolding)
        {
            isHolding = false;
        }

        public void Primary()
        {

        }
        public void Reload()
        {
        }
        public void Secondary()
        {
        }
    }
}