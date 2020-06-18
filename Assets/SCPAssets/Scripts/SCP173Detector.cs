using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.SCPs.Skip173
{
    public class SCP173Detector : SCPStoryNodeBaseCode
    {
        public GameObject Skip173;
        public AudioSource Horror01;
        public AudioSource Horror02;
        public AudioSource NeckSnap;
        public AudioSource BreakGalss;
        IEnumerator RealStory()
        {
            //GameInfo.CurrentGame.achievement.ShowAchievement()

            if (GameInfo.Achievements.Count == 0)
            {
                GameInfo.CurrentGame.achievement.ShowAchievement(14);
            }
            else
            if (14 != -1)
                if (GameInfo.Achievements[14 - 1] == false)
                {
                    GameInfo.Achievements[14 - 1] = true;
                    GameInfo.SaveAchievements();
                    GameInfo.CurrentGame.achievement.ShowAchievement(14);
                }
            GameInfo.CurrentGame.DeathText =
                "Your death body was later found by <color=#28E>Delta-12</color> and informed them the sign of presence of <color=red>SCP-173</color>, however they failed to deal with <color=red>SCP-173</color>, all MTFs was killed by <color=red>SCP-173</color>. Finally, Earth was destoryed by containments inside Site-13.";
            GameInfo.CurrentGame.PlayerBlink.Blink();
            GameInfo.CurrentGame.isHealthSpeedDisabled = true;
            GameInfo.CurrentGame.FirstPerson.WalkSpeed = 0.1f;
            GameInfo.CurrentGame.FirstPerson.RunSpeed= 0.1f;
            GameInfo.CurrentGame.FirstPerson.WalkSpeed = 0.1f;
            //GameInfo.CurrentGame.FirstPerson.Stamina = 0f;
            //GameInfo.CurrentGame.FirstPerson.staminaInternal = 0f;
            GameInfo.CurrentGame.FirstPerson.RunSpeed = 0.1f;
            yield return new WaitForSeconds(1.5f);
            BreakGalss.Play();
            yield return new WaitForSeconds(1f);
            Horror01.Play();
            {
                var lp = Skip173.transform.localPosition;
                lp.x = -2.45f;
                lp.z = 0;
                Skip173.transform.localPosition = lp;

                var lr = Skip173.transform.localRotation;
                var angle = lr.eulerAngles;
                angle.y = 90;
                lr.eulerAngles = angle;
                Skip173.transform.localRotation = lr;
            }
            GameInfo.CurrentGame.PlayerBlink.Blink();
            yield return new WaitForSeconds(2f);
            Horror02.Play();
            {
                var lp = Skip173.transform.position;
                lp.x = GameInfo.CurrentGame.FirstPerson.gameObject.transform.position.x;
                lp.z = GameInfo.CurrentGame.FirstPerson.gameObject.transform.position.z;
                Skip173.transform.position = lp;

            }
            yield return new WaitForSeconds(2f);
            GameInfo.CurrentGame.PlayerBlink.Blink();
            yield return new WaitForSeconds(2f);
            {
                var lp = Skip173.transform.position;
                lp.x = GameInfo.CurrentGame.FirstPerson.gameObject.transform.position.x - 0.5f;
                lp.z = GameInfo.CurrentGame.FirstPerson.gameObject.transform.position.z - 0.5f;
                Skip173.transform.position = lp;

            }
            NeckSnap.Play();
            GameInfo.CurrentGame.PlayerHealth.ChangeHealth(-(GameInfo.CurrentGame.PlayerHealth.MaxHealth + 1));
            //Skip173.transform.position = GameInfo.CurrentGame.FirstPerson.gameObject.transform.position;
            yield break;
        }
        public override void StartStory()
        {
            isStarted = true;
            StartCoroutine(RealStory());
        }
    }

}