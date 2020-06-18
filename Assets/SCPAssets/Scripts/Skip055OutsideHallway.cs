using Site_13ToolLib.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.SCPs
{

    public class Skip055OutsideHallway : MonoBehaviour
    {
        public List<string> LanguagedSubtitles;
        public List<string> UnlanguagedSubtitles;
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<SCPFirstController>() != null)
            {
                try
                {
                    string O55Status = "";
                    O55Status = GameInfo.CurrentGame.FlagsGroup["SCP055Status"];
                    if (O55Status == "Memoried")
                    {
                        string Subtitle = "";
                        int i = new System.Random().Next(0, LanguagedSubtitles.Count);
                        try
                        {
                            Subtitle = Language.Language_Plot[LanguagedSubtitles[i]];
                        }
                        catch (System.Exception)
                        {
                            Subtitle = UnlanguagedSubtitles[i];
                        }
                        GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle(Subtitle);
                        GameInfo.CurrentGame.FlagsGroup["SCP055Status"] = "Forgotten";
                    }
                }
                catch (System.Exception)
                {
                }
            }
        }
    }

}