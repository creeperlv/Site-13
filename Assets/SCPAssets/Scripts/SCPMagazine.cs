using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public class SCPMagazine : SCPInteractive
    {
        public bool isBat = false;
        public int AmmoID;
        public float Capacity=100f;
        public override IEnumerator Move()
        {
            if (isBat == true)
            {
                GameInfo.CurrentGame.Bats++;
            }
            else
            {
                GameInfo.CurrentGame.LeftAmmos[AmmoID] += Capacity;
            }
            this.gameObject.SetActive(false);
            yield break;
        }
    }
}
