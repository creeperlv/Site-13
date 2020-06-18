using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public class AreaAchievementDetector : SCPStoryNodeBaseCode
    {
        public int AchievementID = -1;
        public override void StartStory()
        {
            isStarted = true;
            StartCoroutine(RealStory());
        }
        IEnumerator RealStory()
        {
            Debug.Log("Try to show achievement:"+AchievementID);
            //try
            {
                if (GameInfo.Achievements.Count==0)
                {
                    GameInfo.CurrentGame.achievement.ShowAchievement(AchievementID);
                }else
                if (AchievementID != -1)
                    if (GameInfo.Achievements[AchievementID - 1] == false)
                    {
                        GameInfo.Achievements[AchievementID - 1] = true;
                        GameInfo.SaveAchievements();
                        GameInfo.CurrentGame.achievement.ShowAchievement(AchievementID);
                    }
            }
            //catch (System.Exception)
            //{
            //}

            yield break;
        }
    }

}