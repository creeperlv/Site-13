using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Site13Kernel.Weapon
{
    public class MobilePhone : GenericWeapon
    {
        public GameObject SpotLight;
        public GameObject Normal;
        public GameObject NoBat;
        public Slider BatBar;
        bool isLightOn = false;
        public override void SideUpdate()
        {
            GameInfo.CurrentGame.LeftAmmos[AmmoID] -= Time.deltaTime*0.2f;
            if (isLightOn == true)
            {
                GameInfo.CurrentGame.LeftAmmos[AmmoID] -= Time.deltaTime * 0.2f;
            }
            BatBar.value =GameInfo.CurrentGame.LeftAmmos[AmmoID];
            if (GameInfo.CurrentGame.LeftAmmos[AmmoID] <= 0)
            {
                if (Normal.activeSelf == true)
                {
                    Normal.SetActive(false);
                }
                if (NoBat.activeSelf == false)
                {
                    NoBat.SetActive(true);
                }
                isLightOn = false;
                SpotLight.SetActive(false);
            }
            else
            {
                if (Normal.activeSelf == false)
                {
                    Normal.SetActive(true);
                }
                if (NoBat.activeSelf == true)
                {
                    NoBat.SetActive(false);
                }
            }
        }
        public override void FirePrimary()
        {
            if (SpotLight.activeSelf == false)
            {
                isLightOn = true;
                SpotLight.SetActive(true);
            }
            else
            {
                isLightOn = false;
                SpotLight.SetActive(false);
            }
        }
        public override void FireSecondary()
        {
            if (GameInfo.CurrentGame.isPaused == false)
            {
                if (GameInfo.CurrentGame.Bats > 0)
                {
                    GameInfo.CurrentGame.Bats--;
                    GameInfo.CurrentGame.LeftAmmos[AmmoID] = 100;
                }
            }
        }
    }

}