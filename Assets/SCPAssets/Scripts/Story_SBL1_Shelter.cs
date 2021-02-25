using Site_13ToolLib.Globalization;
using Site13Kernel.DynamicScene;
using Site13Kernel.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Site13Kernel.Stories
{

    public class Story_SBL1_Shelter : SCPStoryNodeBaseCode
    {
        public SCPDoor outDoor;
        public SCPDoor outDoor2;
        public SCPLangScreen TipText;
        public AudioSource Warhead1;
        public AudioSource WarheadThenThresher;
        public GameObject BeforeThresherExplode;
        GameObject BlackCover;
        public GameObject AirBarrier;
        SCPEntity entity;
        #region CharlieYukon
        public GameObject CharlieYukonStory;
        public GameObject Barrier;
        public List<AudioSource> CombatSounds;
        public AudioSource CYTeam01;
        public List<GameObject> Splashes;
        #endregion
        // Start is called before the first frame update
        void Start()
        {
            isStoryRequiresPlayer = true;
            BlackCover = GlobalSceneExecutor.CurrentExecutor.BlackCover;
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
            AirBarrier.SetActive(true);
            if (WarHeadTip != "Disabled")
            {
                StartCoroutine(WarheadAndDetonate());
            }
            else
            {
                StartCoroutine(WarheadFailedThenThresher());
            }
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

                {

                    var c = BlackCover.GetComponent<Image>().color;
                    c.a = 1;
                    BlackCover.GetComponent<Image>().color = c;
                }
                BlackCover.SetActive(true);
                entity.gameObject.GetComponent<SCPFirstController>().enabled = false;
                yield return new WaitForSeconds(3f);
                BlackCover.SetActive(false);
                entity.gameObject.SetActive(false);
                foreach (var item in CombatSounds)
                {
                    item.Play();
                    yield return new WaitForSeconds(0.1f);
                }
                CharlieYukonStory.gameObject.SetActive(true);
                yield return new WaitForSeconds(15f);
                CharlieYukonStory.gameObject.SetActive(false);
                entity.gameObject.SetActive(true);
                foreach (var item in CombatSounds)
                {
                    item.Stop();
                }
                BlackCover.SetActive(true);
                yield return new WaitForSeconds(3f);
                BlackCover.SetActive(false);
                Barrier.SetActive(true);
                entity.gameObject.GetComponent<SCPFirstController>().enabled = true;
                GameInfo.CurrentGame.secondaryNotification.ShowNotification("1 Day After Thresher Protocol");
                outDoor.IsLocked = false;
                outDoor2.IsLocked = false;
                AirBarrier.SetActive(false);
                BeforeThresherExplode.SetActive(false);
            }
            yield return null;
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
            entity.Die();

        }
    }

}