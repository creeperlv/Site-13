using Site_13ToolLib.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public class ShowAreaName : SCPStoryNodeBaseCode
    {
        public string AreaName;
        public string AreaNameCode;

        public override void StartStory()
        {
            isStarted = true;
            //GameInfo.CurrentGame.notification.SendMessage()
            try
            {
                GameInfo.CurrentGame.notification.ShowNotification(Language.Language_Plot[AreaNameCode]);
            }
            catch (System.Exception)
            {
                GameInfo.CurrentGame.notification.ShowNotification(AreaName);
            }
        }
    }

}