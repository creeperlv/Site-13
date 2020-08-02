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

    public class Delta12EnterMainBuildingSideControl : MonoBehaviour
    {
        public List<NavMeshAgent> Delta12Team;
        public Animator PlayerCam;
        public List<SingleSubtitle> SubtitlesStage00;
        public List<SingleSubtitle> SubtitlesStage00_5;
        public List<SingleSubtitle> SubtitlesStage01;
        public float Wait01 = 1f;
        public List<SingleSubtitle> SubtitlesStage02;
        public List<GameObject> Lights;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(StartStory());
        }
        IEnumerator StartStory()
        {
            yield return new WaitForSeconds(0.2f);
            RenderSettings.fog = false;
            Delta12Team[0].transform.GetChild(0).GetComponent<Animator>().enabled = true;
            PlayerCam.enabled = false;
            Delta12Team[0].transform.GetChild(0).GetComponent<Animator>().SetTrigger("WalkWithRiffle");
            //for (int i = 1; i < 5; i++)
            //{
            //    Delta12Team[i].transform.GetChild(0).GetComponent<Animator>().SetTrigger("WalkWithRiffle");
            //}
            //Delta12Team[0].GetComponent<Delta12Teammate>().PlayWalkSound = true;
            yield return new WaitForSeconds(1f);
            //for (int i = 1; i < 5; i++)
            //{
            //    Delta12Team[i].transform.GetChild(0).GetComponent<Animator>().enabled = false;
            //}
            //Delta12Team[0].transform.GetChild(0).GetComponent<Animator>().enabled = true;
            //Delta12Team[0].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Walk");
            yield return new WaitForSeconds(3f);
            Delta12Team[0].transform.GetChild(0).GetComponent<Animator>().enabled = false;
            //Delta12Team[0].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Idle1");
            Delta12Team[0].GetComponent<Delta12Teammate>().PlayWalkSound = false;
            yield return new WaitForSeconds(1f);
            yield return new WaitForSeconds(8f);
            PlayerCam.enabled = true;
            foreach (var item in Delta12Team)
            {
                item.transform.GetChild(0).GetComponent<Animator>().enabled = true;
                item.transform.GetChild(0).GetComponent<Animator>().SetTrigger("WalkWithRiffle");
            }
            foreach (var item in Delta12Team)
            {
                yield return new WaitForSeconds(.01f);
                //item.transform.GetChild(0).GetComponent<Animator>().SetTrigger("WalkWithRiffle");
                item.GetComponent<Delta12Teammate>().PlayWalkSound = true;
            }
            yield return new WaitForSeconds(4f);
            RenderSettings.fog = true;
            yield return new WaitForSeconds(4f);
            {
                //SubtitlesStage00

                foreach (var item in SubtitlesStage00)
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
                {
                    foreach (var item in Lights)
                    {
                        item.SetActive(true);

                    }
                }
            }
            {
                foreach (var item in Delta12Team)
                {
                    item.enabled = false;
                }
            }
            {
                foreach (var item in SubtitlesStage00_5)
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
            }
            foreach (var item in Delta12Team)
            {

                item.GetComponent<Delta12Teammate>().PlayWalkSound = true;
                item.transform.GetChild(0).GetComponent<Animator>().SetTrigger("IdleAiming");
            }
            {
                //SubtitlesStage01

                foreach (var item in SubtitlesStage01)
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
                {
                    foreach (var item in Lights)
                    {
                        item.SetActive(true);

                    }
                }
            }
            {
                yield return new WaitForSeconds(Wait01);
                foreach (var item in Delta12Team)
                {

                    item.GetComponent<Delta12Teammate>().PlayWalkSound = true;
                    item.transform.GetChild(0).GetComponent<Animator>().SetTrigger("WalkWithRiffle");
                }
                //SubtitlesStage02
                Delta12Team[0].transform.GetChild(0).GetComponent<Animator>().SetTrigger("GiveForwardOrder");
                foreach (var item in SubtitlesStage02)
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
                {
                }
            }
            {
                foreach (var item in Delta12Team)
                {
                    item.enabled = false;
                }
            }

            yield break;
        }
    }

}