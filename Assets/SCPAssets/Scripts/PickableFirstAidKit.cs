using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{

    public class PickableFirstAidKit : SCPInteractive
    {
        public int FAKCount = 3;
        public override IEnumerator Move()
        {
            isOperating = true;
            GameInfo.CurrentGame.PossessingFAK+=FAKCount;
            gameObject.SetActive(false);
            isOperating = false;
            yield break;
        }
    }

}