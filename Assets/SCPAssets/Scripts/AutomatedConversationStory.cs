using Site_13ToolLib.Globalization;
using Site13Kernel.Stories.Universal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Stories.Universal
{
    public class AutomatedConversationStory : MonoBehaviour
    {
        public List<SingleSubtitle> subtitles;
        void Start()
        {
            StartCoroutine(RealStory());
        }
        IEnumerator RealStory()
        {
            foreach (var item in subtitles)
            {
                try
                {

                    GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle(Language.Language_Plot.ContainsKey(item.LanguageAlia) ? Language.Language_Plot[item.LanguageAlia] : item.fallback, item.Duration);

                }
                catch (System.Exception)
                {
                }
                if (item.audio != null && item._AudioSource != null)
                {
                    try
                    {
                        item._AudioSource.clip = item.audio;
                        item._AudioSource.Play();
                    }
                    catch (System.Exception)
                    {
                    }
                }
                yield return new WaitForSeconds(item.WaitLength);
            }
        }
    }
}