using Site13Kernel.DynamicScene;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Site13Kernel
{

    public class LightControl : SCPInteractive
    {
        public GameObject LightsCollection;
        public bool isNewLightControl = false;
        public int AchievementID = -1;
        public AudioSource SFX;
        public Text Tip;
        public override IEnumerator Move()
        {
            isOperating = true;
            SFX.Play();
            if (isNewLightControl == false)
            {

                if (LightsCollection.activeInHierarchy == true)
                {
                    Tip.text = "LIGHTS OFF";
                    LightsCollection.SetActive(false);
                }
                else
                {
                    if (AchievementID != -1)
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
                    Tip.text = "LIGHTS ON";
                    LightsCollection.SetActive(true);
                }
            }
            else
            {
                SceneLightManager.CurrentManager.SetLightState(!SceneLightManager.CurrentManager.GetCurrentLightState());
                if (SceneLightManager.CurrentManager.GetCurrentLightState())
                {
                    Tip.text = "LIGHTS ON";
                }
                else
                {
                    Tip.text = "LIGHTS OFF";
                }
            }
            yield return new WaitForSeconds(0.5f);
            isOperating = false;
            yield break;
        }
    }

}