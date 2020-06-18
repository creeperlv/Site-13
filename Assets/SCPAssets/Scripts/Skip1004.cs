using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Site13Kernel.SCPs.Skip1004
{

    public class Skip1004 : SCPInteractive
    {
        public GameObject Phone;
        public Image EroContent;
        public List<Sprite> sprites=new List<Sprite>();
        public override IEnumerator Move()
        {
            isOperating = true;
            if (GameInfo.CurrentGame.HandingItem.ItemID == "Foundation Phone")
            {
                Phone.SetActive(false);
                EroContent.gameObject.SetActive(true);
                EroContent.sprite = sprites[Random.Range(0, sprites.Count)];
                GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Wow... PRPRPRPRPR!");
                GameInfo.CurrentGame.cameraShake.StartShakingRandom(3, 2, 20, 20);
                 yield return new WaitForSeconds(3);
                GameInfo.CurrentGame.DeathText = "You are exhausted to death from viewing too much pornographic contents.";
                GameInfo.CurrentGame.PlayerHealth.ChangeHealth(-30);
                Phone.SetActive(true);
                EroContent.gameObject.SetActive(false);
            }
            else
            {
                GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Err...I need an electric device to scan the code.");
                yield return new WaitForSeconds(0.5f);
            }
            isOperating = false;

            yield break;
        }

    }

}