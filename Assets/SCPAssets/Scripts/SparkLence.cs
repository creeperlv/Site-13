using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public class SparkLence : SCPInteractive
    {
        public SCPDoor ElevatorDoor;
        public override IEnumerator Move()
        {
            isOperating = true;
            StartCoroutine(ElevatorDoor.Close());
            ElevatorDoor.LockMessage = "Your light power has effected the door and it stopped responding.";
            ElevatorDoor.IsLocked = true;
            GetComponent<AudioSource>().Play();
            GetComponent<Animator>().enabled = true;
            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("HIKARIAHHHHHHHH!", 1f);
            GameInfo.CurrentGame.isPauseAllowed = false;
            yield return new WaitForSeconds(2f);
            GetComponent<Animator>().enabled = false;
            GameObject Hikari = GameObject.Instantiate(transform.Find("Point Light (1)").gameObject, GameInfo.CurrentGame.FirstPerson.gameObject.transform); Hikari.SetActive(true);
            {
                var AchievementID = 15;
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
            GameInfo.CurrentGame.BGMManager.ChangeBGM("M-3,11");
            this.transform.Find("TIGA").gameObject.SetActive(false);
            this.transform.Find("Point Light").gameObject.SetActive(false);
            GameInfo.CurrentGame.isUltraState = true;
            GameInfo.CurrentGame.PlayerHealth.MaxHealth = 500000;
            GameInfo.CurrentGame.PlayerHealth.CurrentHealth = 500000;
            yield return new WaitForSeconds(120f);
            {
                Hikari.GetComponent<Light>().intensity = 3;
                transform.Find("Audio Source").GetComponent<AudioSource>().Play();
            }
            yield return new WaitForSeconds(60f);
            GameInfo.CurrentGame.DeathText = "You are died from depleting all lights in you.";
            GameInfo.CurrentGame.PlayerHealth.ChangeHealth(-500001);
            transform.Find("Audio Source").GetComponent<AudioSource>().Stop();
            yield break;
        }
    }
}
