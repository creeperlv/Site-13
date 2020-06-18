using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Site13Kernel.Weapon
{

    public class SCPD12StandardRiffle : TraditionalWeapon
    {
        public override void SinglePrimaryOperate()
        {
            timed2 = 0.2f;
            try
            {
                AS.Play();
            }
            catch (System.Exception)
            {
            }
            try
            {
                //ani.Play("Recoil");
            }
            catch (System.Exception)
            {
            }
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0F));
            {
                var shell = GameObject.Instantiate(PrimaryShell, ray.origin, OriPoint.transform.rotation, ShellPresenter.transform).GetComponent<SCP_R_13_Shell>();
                shell.Damage = 200;
                shell.CriticalDamage = 200;
                shell.isFromPlayer=true;
                shell.Sender = Pocesser;
            }
            SecondaryRecoilRate += 0.1f;
            if (SecondaryRecoilRate > 1)
            {
                SecondaryRecoilRate = 1;
            }
            //a.layer = 8;
            //a.transform.GetChild(0).gameObject.layer = 8;
            if (MaxCap != 0)
            {
                GameInfo.CurrentGame.FlagsGroup[BagAlias + ":" + ID] = "" + (int.Parse(GameInfo.CurrentGame.FlagsGroup[BagAlias + ":" + ID]) - 1);
            }
        }

    }

}