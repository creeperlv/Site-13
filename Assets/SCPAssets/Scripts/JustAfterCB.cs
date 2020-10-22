using Site_13ToolLib.Globalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Stories
{
    [Obsolete]
    public class JustAfterCB : SCPStoryNodeBaseCode
    {
        public SCPDoor ESDoor;
        public GameObject ToShow;
        public override void StartStory()
        {
            isStarted = true;
            StartCoroutine(RealStory());  
        }
        IEnumerator RealStory()
        {
            try
            {
                GameInfo.CurrentGame.notification.ShowNotification(Language.Language_Plot["AreaNames.1"]);
            }
            catch (System.Exception)
            {
                GameInfo.CurrentGame.notification.ShowNotification("Level 1, Section B");
            }
            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Guard: Do you really want to obey it?");
            yield return new WaitForSeconds(3f);
            transform.Find("JACB").GetComponent<AudioSource>().Play();
            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Guard: I won't, it's 079! Come back. It just a trash computer!");
            yield return new WaitForSeconds(3f);
            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Guard: Ahh... You go yourself!");
            if(ESDoor.JudgeWhetherOpen()==false)
            StartCoroutine(ESDoor.Open());
            ESDoor.IsLocked = true;
            yield return new WaitForSeconds(1.5f);
            ToShow.SetActive(true);
            {
                var lp=ToShow.transform.localPosition;
                lp.x = -2.5f;
                ToShow.transform.localPosition = lp;
            }
            float timed = 0;
            while (timed<1)
            {
                ToShow.transform.Translate(new Vector3(2.5f,0,0) * Time.deltaTime);
                timed += Time.deltaTime;
                yield return null;
            }
            yield break;
        }
    }

}