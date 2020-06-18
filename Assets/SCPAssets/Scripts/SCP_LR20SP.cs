using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Site13Kernel.Weapon
{
    public class SCP_LR20SP : GenericWeapon
    {
        public int MaxSP = 150;
        public Slider SPBar;
        public void Start()
        {
            ShowSP();
        }
        public override void FirePrimary()
        {
            if (GameInfo.CurrentGame.LeftAmmos[AmmoID] < MaxSP)
            {
                GameInfo.CurrentGame.LeftAmmos[AmmoID]++;
            }
            base.FirePrimary();
            ShowSP();
        }
        public override void FireSecondary()
        {
            if (GameInfo.CurrentGame.LeftAmmos[AmmoID] >= 20)
            {
                GameInfo.CurrentGame.LeftAmmos[AmmoID] -= 20;
                //天火出鞘！
                GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Judgement of Shamash!");
                base.FireSecondary();
                ShowSP();
            }
        }
        void ShowSP()
        {
            SPBar.maxValue = MaxSP;
            SPBar.value = GameInfo.CurrentGame.LeftAmmos[AmmoID];
            AmmoShower.text = $"{GameInfo.CurrentGame.LeftAmmos[AmmoID]}/{MaxSP}";
        }
    }

}