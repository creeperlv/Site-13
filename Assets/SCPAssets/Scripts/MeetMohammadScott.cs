using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Site13Kernel.Stories
{
    public class MeetMohammadScott : SCPStoryNodeBaseCode
    {
        public Animator Scott;
        public SCPDoor door;
        public Animator MainAnimation;
        public override void StartStory()
        {
            {
                Debug.Log("Started 00");
                MainAnimation.SetTrigger("Meet");
                Debug.Log("Started 01");
                StartCoroutine(RealStory());
                isStarted = true;
            }
        }
        public IEnumerator RealStory()
        {
            Debug.Log("Started 02");
            StartCoroutine(ShowSubtitles());
            yield return new WaitForSeconds(5.0f);
            Scott.SetTrigger("Walk");
            yield return new WaitForSeconds(1f);
            GameInfo.CurrentGame.FirstPerson.gameObject.SetActive(false);
            yield return new WaitForSeconds(7.0f);
            StartCoroutine(door.Open());
            yield return new WaitForSeconds(10.0f);
            GameInfo.CurrentGame.CurrentSceneSaveSystem.Save();
            GameInfo.CurrentGame.NextScene = 14;
            SceneManager.LoadScene(2);
            yield break;
        }
        public IEnumerator ShowSubtitles()
        {
            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Scott: Hey! There! I am Dr. Scott Mohammad!");
            yield return new WaitForSeconds(3.0f);
            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Scott: Follow me!");
            yield return new WaitForSeconds(3.0f);
            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Scott: We built a 'safe' area. And we are searching for survivors.");
            yield return new WaitForSeconds(3.0f);
            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Scott: Securities are down. 079 is not responding.");
            yield return new WaitForSeconds(3.0f);
            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Scott: We can only count on ourselves now");
            yield break;
        }
    }

}