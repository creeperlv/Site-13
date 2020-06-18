using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.SCPs.JOJO
{
    public class StarPlatinum : SCPInteractive
    {
        public GameObject Door1;
        public GameObject Door2;
        public override IEnumerator Move()
        {
            bool isEscaped = false;
            try
            {
                if (
                    GameInfo.CurrentGame.FlagsGroup["JOJO-01"] == "ESC") isEscaped = true;
            }
            catch (System.Exception)
            {
            }
            if (isEscaped == false)
            {
                if (GameInfo.CurrentGame.HandingItem.SecurityClearance >= 2)
                {
                    isOperating = true;
                    {
                        var AchievementID = 17;
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
                    GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Star Platinum -- The World!", 2f);
                    yield return new WaitForSeconds(2f);
                    Time.timeScale = 0;
                    AudioListener.pause = true;
                    yield return new WaitForSecondsRealtime(5f);
                    AudioListener.pause = false;
                    Time.timeScale = 1;
                    {
                        {
                            var lp = Door1.transform.localPosition;
                            lp.x = -500;
                            lp.y = 451;
                            lp.z = 2200;
                            Door1.transform.localPosition = lp;
                        }
                        {
                            var ro = Door1.transform.localRotation;
                            var ang = ro.eulerAngles;
                            ang.x = 90;
                            ang.z = 39;
                            ro.eulerAngles = ang;
                            Door1.transform.localRotation = ro;
                        }
                    }
                    {
                        {
                            var lp = Door2.transform.localPosition;
                            lp.x = 750;
                            lp.y = 451;
                            lp.z = 3000;
                            Door2.transform.localPosition = lp;
                        }
                        {
                            var ro = Door2.transform.localRotation;
                            var ang = ro.eulerAngles;
                            ang.x = 90;
                            ang.z = -39;
                            ro.eulerAngles = ang;
                            Door2.transform.localRotation = ro;
                        }
                    }
                    try
                    {

                        GameInfo.CurrentGame.FlagsGroup["JOJO-01"] = "ESC";
                    }
                    catch (System.Exception)
                    {
                        GameInfo.CurrentGame.FlagsGroup.Add("JOJO-01", "ESC");
                    }
                }
            }
            yield break;
        }
    }

}