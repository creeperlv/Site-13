using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.FPSSystem
{
    public class WeaponV3 : MonoBehaviour,IHandItem
    {
        public Animator CentralAnimator;
        public string DefaultHoldTrigger;
        public string FireTrigger;
        public string ReloadTrigger;
        public string FightTrigger;
        public string AimTrigger0;
        public string AimTrigger1;
        public string RunTrigger;
        public Light FlashLightObject;
        public void Fight()
        {
        
        }

        public void FlashLight()
        {
            throw new System.NotImplementedException();
        }

        public string GetData()
        {
            throw new System.NotImplementedException();
        }

        public Sprite GetOverriddenCrosshair()
        {
            throw new System.NotImplementedException();
        }

        public Vector3 GetOverriddenViewportShakingIntensity()
        {
            throw new System.NotImplementedException();
        }

        public void Init(string data)
        {
            throw new System.NotImplementedException();
        }

        public bool IsCrosshairOverridden()
        {
            throw new System.NotImplementedException();
        }

        public bool IsFightCompleted()
        {
            throw new System.NotImplementedException();
        }

        public bool IsFPSSystemV3Enabled()
        {
            throw new System.NotImplementedException();
        }

        public bool IsOnOperation()
        {
            throw new System.NotImplementedException();
        }

        public bool IsPrimaryCompleted()
        {
            throw new System.NotImplementedException();
        }

        public bool IsReloadCompleted()
        {
            throw new System.NotImplementedException();
        }

        public bool IsSecondaryCompleted()
        {
            throw new System.NotImplementedException();
        }

        public void Primary()
        {
            throw new System.NotImplementedException();
        }

        public void Reload()
        {
            throw new System.NotImplementedException();
        }

        public void Secondary()
        {
            throw new System.NotImplementedException();
        }

        public void UnPrimary()
        {
            throw new System.NotImplementedException();
        }

        public void UnSecondary()
        {
            throw new System.NotImplementedException();
        }
    }

}