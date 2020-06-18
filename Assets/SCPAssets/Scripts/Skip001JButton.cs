using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.SCPs.Skip001J
{

    public class Skip001JButton : SCPInteractive
    {
        public GameObject Facility;
        public override IEnumerator Move()
        {
            isOperating = true;
            {
                var AchievementID = 16;
                {
                    if (GameInfo.Achievements.Count == 0)
                    {
                        GameInfo.CurrentGame.achievement.ShowAchievement(AchievementID);
                    }
                    else
                    if (AchievementID != -1)
                        if (GameInfo.Achievements[AchievementID - 1] == false)
                        {
                            GameInfo.Achievements[AchievementID - 1] = true;
                            GameInfo.SaveAchievements();
                            GameInfo.CurrentGame.achievement.ShowAchievement(AchievementID);
                        }
                }
            }
            GetComponent<Animator>().enabled = true;
            yield return new WaitForSeconds(0.4f);
            GetComponent<Animator>().enabled = false;
            yield return new WaitForSeconds(0.5f);
            Facility.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            GameInfo.CurrentGame.DeathText = "You pressed SCP-001-J and successfully destroyed the entire universe.";
            GameInfo.CurrentGame.PlayerHealth.ChangeHealth(-1000);
            yield break;
        }
    }

}