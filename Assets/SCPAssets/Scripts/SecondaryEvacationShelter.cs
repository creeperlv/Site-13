using Site13Kernel.Globalization;
using Site_13ToolLib.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Site13Kernel
{

    public class SecondaryEvacationShelter : SCPStoryNodeBaseCode
    {
        public SCPDoor outDoor;
        public SCPDoor CornerDoor;
        public SCPLangScreen TipText;
        public AudioSource Warhead1;
        public AudioSource WarheadThenThresher;

        public GameObject BlackCover;

        public GameObject BeforeThresher;
        public GameObject AfterThresher;
        public GameObject Lights;
        public GameObject RandomAnnounc;
        public Combat1 CharileYukon;
        [System.Serializable]
        public class Combat1
        {
            public GameObject Cam;
            public GameObject Cam2;
            public GameObject Soldiers;
            public List<AudioSource> CombatSounds;
            public AudioSource CYTeam01;
            public List<GameObject> Splashes;
        }

        [HideInInspector]
        public SCPEntity entity;
        void Start()
        {
            isStoryRequiresPlayer = true;
        }
        public override void StartStory(GameObject Player)
        {
            isStarted = true;
            GameInfo.CurrentGame.BGMManager.StopBGM();
            outDoor.IsLocked = true;
            entity = Player.GetComponent<SCPEntity>();
            string WarHeadTip = "";
            try
            {
                WarHeadTip = GameInfo.CurrentGame.FlagsGroup["Warhead"];
            }
            catch (System.Exception)
            {
            }
            if (WarHeadTip != "Disabled")
            {
                StartCoroutine(WarheadAndDetonate());
            }
            else
            {
                StartCoroutine(WarheadFailedThenThresher());
            }
        }
        IEnumerator WarheadAndDetonate()
        {
            TipText.SetText("Text (1)", "Screen.SBL1.ES.3");
            yield return new WaitForSeconds(1f);
            StartCoroutine(outDoor.Close());
            GameInfo.CurrentGame.DeathText = "You are killed by on-site nuclear warheads, however, warheads are unable to stop escaping SCPs, they destoryed the entire earth.";
            Warhead1.Play();
            try
            {
                GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle(Language.Language_Plot["Announc.5"], 4f);
            }
            catch (System.Exception)
            { }
            yield return new WaitForSeconds(99f);
            BlackCover.SetActive(true);
            entity.ChangeHealth(-200f);

        }
        IEnumerator WarheadFailedThenThresher()
        {
            {
                GameInfo.CurrentGame.secondaryNotification.ShowNotification("115 Seconds Before Thresher Protocol");
            }
            {
                if (GameInfo.Achievements.Count != 0)
                {
                    if (GameInfo.Achievements[3 - 1] == false)
                    {
                        GameInfo.Achievements[3 - 1] = true;
                        GameInfo.SaveAchievements();
                        GameInfo.CurrentGame.achievement.ShowAchievement(3);
                    }
                }
                TipText.SetText("Text (1)", "Screen.SBL1.ES.3");
                yield return new WaitForSeconds(1f);
                StartCoroutine(outDoor.Close());
                //GameInfo.CurrentGame.DeathText = "You are killed by on-site nuclear warheads, however, warheads are unable to stop escaping SCPs, they destoryed the entire earth.";
                WarheadThenThresher.Play();
                try
                {
                    GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle(Language.Language_Plot["Announc.5"], 4f);
                }
                catch (System.Exception)
                {
                    GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Announcement: We detonating warheads in T minus 90 seconds.", 4f);
                }
                yield return new WaitForSeconds(99.5f);
                try
                {
                    GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle(Language.Language_Plot["Announc.6"], 4f);
                }
                catch (System.Exception)
                {
                    GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Announcement: We are unable to detonate warheads for unknown reasons.", 4f);
                }
                yield return new WaitForSeconds(4f);
                try
                {
                    GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle(Language.Language_Plot["Announc.7"], 4f);
                }
                catch (System.Exception)
                {
                    GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Announcement: I want Thresher Protocol be activated immediately, all personnel go back to evacuation shelter now!", 4f);
                }
                yield return new WaitForSeconds(7.4f);
                TipText.SetText("Text (1)", "Screen.SBL1.ES.4");
                yield return new WaitForSeconds(4f);
                TipText.SetText("Text (1)", "Screen.SBL1.ES.5");
            }
            {

                var c = BlackCover.GetComponent<Image>().color;
                c.a = 1;
                BlackCover.GetComponent<Image>().color = c;
            }
            BlackCover.SetActive(true);
            entity.gameObject.GetComponent<SCPFirstController>().enabled = false;
            yield return new WaitForSeconds(3f);
            BlackCover.SetActive(false);
            CharileYukon.Cam.SetActive(true);
            entity.gameObject.SetActive(false);
            Lights.SetActive(false);
            foreach (var item in CharileYukon.CombatSounds)
            {
                item.Play();
                yield return new WaitForSeconds(0.1f);
            }
            for (int i = 0; i < 3; i++)
            {
                var KyoAni = CharileYukon.Soldiers.transform.GetChild(i).GetComponent<Animator>();
                KyoAni.applyRootMotion = false;

                KyoAni.Play("WalkAimingRifle");
            }
            float time1 = 0;
            float time2 = 0;
            bool isPlayed = false;
            while (time1 < 7)
            {
                CharileYukon.Soldiers.transform.Translate(Vector3.forward * 2 * Time.deltaTime);
                if (time2 > 0.1f)
                {
                    if (CharileYukon.Splashes[0].activeSelf == false)
                    {
                        foreach (var item in CharileYukon.Splashes)
                        {
                            item.SetActive(true);
                        }
                    }
                    else
                    {
                        foreach (var item in CharileYukon.Splashes)
                        {
                            item.SetActive(false);
                        }
                    }
                    time2 = 0;
                }
                if (time1 > 3)
                {
                    if (isPlayed == false)
                    {
                        isPlayed = true;
                        CharileYukon.CYTeam01.Play();
                    }
                }
                time2 += Time.deltaTime;
                time1 += Time.deltaTime;
                yield return null;
            }
            BlackCover.SetActive(true);
            time1 = 0;
            while (time1 < 3)
            {
                CharileYukon.Soldiers.transform.Translate(Vector3.forward * 2 * Time.deltaTime);
                if (time2 > 0.1f)
                {
                    if (CharileYukon.Splashes[0].activeSelf == false)
                    {
                        foreach (var item in CharileYukon.Splashes)
                        {
                            item.SetActive(true);
                        }
                    }
                    else
                    {
                        foreach (var item in CharileYukon.Splashes)
                        {
                            item.SetActive(false);
                        }
                    }
                    time2 = 0;
                }
                time2 += Time.deltaTime;
                time1 += Time.deltaTime;
                yield return null;
            }
            time1 = 0;
            CharileYukon.Cam.SetActive(false);
            CharileYukon.Cam2.SetActive(true);
            BlackCover.SetActive(false);
            while (time1 < 5)
            {
                CharileYukon.Soldiers.transform.Translate(Vector3.forward * 2 * Time.deltaTime);
                if (time2 > 0.1f)
                {
                    if (CharileYukon.Splashes[0].activeSelf == false)
                    {
                        foreach (var item in CharileYukon.Splashes)
                        {
                            item.SetActive(true);
                        }
                    }
                    else
                    {
                        foreach (var item in CharileYukon.Splashes)
                        {
                            item.SetActive(false);
                        }
                    }
                    time2 = 0;
                }
                time2 += Time.deltaTime;
                time1 += Time.deltaTime;
                yield return null;
            }
            CharileYukon.Cam2.SetActive(false);
            entity.gameObject.SetActive(true);
            AfterThresher.SetActive(true);
            BlackCover.SetActive(true);
            yield return new WaitForSeconds(2f);
            GameInfo.CurrentGame.BGMManager.PlayBGM();
            RandomAnnounc.SetActive(true);

            {

                var c = BlackCover.GetComponent<Image>().color;
                c.a = 0;
                BlackCover.GetComponent<Image>().color = c;
            }
            GameInfo.CurrentGame.secondaryNotification.ShowNotification("1 Day After Thresher Protocol");
            outDoor.IsLocked = false;
            BeforeThresher.SetActive(false);
            BlackCover.SetActive(false);
            CornerDoor.IsLocked = false;
            entity.gameObject.GetComponent<SCPFirstController>().enabled = true;

        }
    }

}