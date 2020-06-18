using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{

    public class SCPLargeLiftControl01 : SCPInteractive
    {
        public GameObject Platform;
        public AnimationClip ShowUp;
        public AudioClip MovingSFX;
        public override IEnumerator Move()
        {
            isOperating = true;

            var ani=Platform.GetComponent<Animation>();
            var AS=Platform.transform.Find("Audio Source").GetComponent<AudioSource>();
            AS.clip = MovingSFX;
            AS.Play();
            //ani.clip = ShowUp;
            ani.AddClip(ShowUp, "Show");
            ani.Play("Show");
            yield return new WaitForSeconds(5);
            ani.Stop();
            if (GameInfo.Achievements.Count == 0)
            {
                GameInfo.CurrentGame.achievement.ShowAchievement(10);
            }
            else
                if (GameInfo.Achievements[10 - 1] == false)
                {
                    GameInfo.Achievements[10 - 1] = true;
                    GameInfo.SaveAchievements();
                    GameInfo.CurrentGame.achievement.ShowAchievement(10);
                }
            yield break;
        }
    }

}