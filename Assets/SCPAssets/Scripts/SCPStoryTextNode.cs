using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{

    public class SCPStoryTextNode: SCPStoryNodeBaseCode
    {
        public List<string> Subtitles;
        public List<int> Durations;
        public bool IsLangEnabled = false;
        public override void StartStory()
        {
            isStarted = true;
            StartCoroutine(RealStartStory());
        }
        IEnumerator RealStartStory()
        {
            if (Subtitles != null)
            {
                for (int i = 0; i < Subtitles.Count; i++)
                {
                    if (IsLangEnabled == false)
                        //SubtitleTextOGC.GetComponent<SubtitleController>().ShowSubtitle(Subtitle[i]);
                        GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle(Subtitles[i]);
                    else
                    {
                        try
                        {
                            //SubtitleTextOGC.GetComponent<SubtitleController>().ShowSubtitle(Language.Plot[Subtitle[i]]);
                            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle(Site_13ToolLib.Globalization.Language.Language_Plot[Subtitles[i]]);
                        }
                        catch (Exception)
                        {
                            //SubtitleTextOGC.GetComponent<SubtitleController>().ShowSubtitle("TRANSLATION_ERROR");
                            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("TRANSLATION_ERROR");
                        }
                    }
                    yield return new WaitForSeconds(3f);
                }
            }

        }
    }
}
