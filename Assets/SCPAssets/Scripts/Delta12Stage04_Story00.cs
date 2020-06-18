using Site_13ToolLib.Globalization;
using Site13Kernel.MTF;
using Site13Kernel.Stories.Universal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Site13Kernel.Stories.Delta12
{
    public class Delta12Stage04_Story00 : MonoBehaviour
    {
        public List<NavMeshAgent> agents;
        public List<SingleSubtitle> Subtitles;
        #region Finalizing Settings
        public List<GameObject> ToActive;
        public List<GameObject> ToDeactive;
        public bool willJump = false;
        public bool UseLoader = false;
        public int SceneID = 0;
        #endregion
        void Start()
        {

        }
        private void OnEnable()
        {
            StartCoroutine(StartStory());
        }
        IEnumerator StartStory()
        {
            yield return new WaitForSeconds(1f);

            foreach (var item in agents)
            {
                yield return new WaitForSeconds(.01f);
                item.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Walk");
                item.GetComponent<Delta12Teammate>().PlayWalkSound = true;
            }
            foreach (var item in agents)
            {
                item.SetDestination(this.transform.position);
                item.isStopped = false;
            }
            yield break;
        }
        IEnumerator RealStory()
        {
            foreach (var item in agents)
            {
                //yield return new WaitForSeconds(.01f);
                item.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Idle1");
                item.GetComponent<Delta12Teammate>().PlayWalkSound = false;
            }
            foreach (var item in agents)
            {
                item.isStopped = true;
            }
            foreach (var item in Subtitles)
            {
                if (item.LanguageAlia == "")
                {
                    GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle(item.fallback);
                }
                else
                {

                    if (Language.Language_Plot.ContainsKey(item.LanguageAlia))
                    {
                        GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle(Language.Language_UI[item.LanguageAlia]);
                    }
                    else
                        GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle(item.fallback);
                }
                try
                {
                    item._AudioSource.clip = item.audio;
                    item._AudioSource.Play();
                }
                catch (Exception)
                {
                }

                yield return new WaitForSeconds(item.WaitLength);
            }
            //Start Finalization
            foreach (var item in ToActive)
            {
                item.SetActive(true);
            }
            foreach (var item in ToDeactive)
            {
                item.SetActive(false);
            }
            if (willJump == true)
            {
                if (UseLoader)
                {
                    GameInfo.CurrentGame.NextScene = SceneID;
                    SceneManager.LoadScene(2);
                }
                else
                {
                    SceneManager.LoadScene(SceneID);
                }
            }
            //
            yield break;
        }
        bool isNextStage = false;
        private void OnTriggerEnter(Collider other)
        {
            if (isNextStage == false)
            {

                var A = other.GetComponent<Delta12Teammate>();
                if (A != null)
                {

                    StartCoroutine(RealStory());
                    isNextStage = true;
                }
            }
        }
    }

}
namespace Site13Kernel.Stories.Universal
{


    [Serializable]
    public class SingleSubtitle
    {
        public string fallback = "";
        public string LanguageAlia = "";
        public float Duration = 0f;
        public float WaitLength = 0f;
        public AudioClip audio;
        public AudioSource _AudioSource;
    }
}