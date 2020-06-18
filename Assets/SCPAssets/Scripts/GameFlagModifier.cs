using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{

    public class GameFlagModifier : SCPInteractive
    {
        public string key;
        public string value;
        public SecurityCredential RequiredSecurityCredential = SecurityCredential.None;
        public AudioSource WillSFX;
        public AudioSource RefuseSFX;
        public bool WillBeep = false;
        public override IEnumerator Move()
        {
            isOperating = true;
            bool willdo = false;

            switch (RequiredSecurityCredential)
            {
                case SecurityCredential.None:
                    willdo = true;
                    break;
                case SecurityCredential.Level3:
                    if (GameInfo.CurrentGame.HandingItem.SecurityClearance >= 3)
                    {
                        willdo = true;
                    }
                    break;
                case SecurityCredential.Level4:
                    if (GameInfo.CurrentGame.HandingItem.SecurityClearance >= 4)
                    {
                        willdo = true;
                    }
                    break;
                case SecurityCredential.Level5:
                    if (GameInfo.CurrentGame.HandingItem.SecurityClearance >= 5)
                    {
                        willdo = true;
                    }
                    break;
                default:
                    break;
            }
            if (willdo)
            {
                if (key == "Warhead")
                {
                    try
                    {

                    }
                    catch (System.Exception)
                    {
                    }
                    if (GameInfo.Achievements.Count == 0)
                    {
                        GameInfo.CurrentGame.achievement.ShowAchievement(2);
                    }
                    else if (GameInfo.Achievements[2 - 1] == false)
                    {
                        GameInfo.Achievements[2 - 1] = true;
                        GameInfo.SaveAchievements();
                        GameInfo.CurrentGame.achievement.ShowAchievement(2);
                    }
                }
                if (WillSFX != null)
                WillSFX.Play();
                try
                {
                    GameInfo.CurrentGame.FlagsGroup[key] = value;
                }
                catch (System.Exception)
                {
                    try
                    {
                        GameInfo.CurrentGame.FlagsGroup.Add(key, value);
                    }
                    catch (System.Exception)
                    {
                    }
                }
            }
            else
            {
                if(RefuseSFX!=null)
                RefuseSFX.Play();
                GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("You need a higher security credential.");
                yield return new WaitForSeconds(1f);
                isOperating = false;
            }
            yield break;
        }
    }

}