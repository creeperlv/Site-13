using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Weapon
{
    public class SCPPickableWeapon : SCPInteractive
    {
        public int ItemID;
        // Start is called before the first frame update
        void Start()
        {

        }
        public override IEnumerator Move()
        {
            GameInfo.CurrentGame.EquippedItems[ItemID] = true;
            this.gameObject.SetActive(false);
            yield break;
        }
        // Update is called once per frame
        void Update()
        {

        }
    }

}