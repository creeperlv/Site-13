using Site_13ToolLib.Globalization;
using Site13Kernel.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Stories
{

    public class Story_SBL1_Shelter : SCPStoryNodeBaseCode
    {
        public SCPDoor outDoor;
        public SCPLangScreen TipText;
        public AudioSource Warhead1;
        public AudioSource WarheadThenThresher;
        public GameObject BlackCover;
        SCPEntity entity;
        // Start is called before the first frame update
        void Start()
        {

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
        IEnumerator WarheadFailedThenThresher() { 
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

        }
    }

}