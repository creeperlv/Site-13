using Site_13ToolLib.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Site13Kernel.EFI
{
    public class AchievementEFILoader : EFIBase
    {
        public override void Run()
        {
            Debug.Log("StartLoadAchievements");
            Debug.Log("AchievementText:" + Language.Achievements.Count);
            int Count = Language.Achievements.Count / 2;
            for (int i = 0; i < Count; i++)
            {
                GameInfo.Achievements.Add(false);
            }
            //Load file from './Save/Achievements.bin'
            try
            {

                if (File.Exists("./Saves/Achievements.bin"))
                {

                    var ach = File.ReadAllText("./Saves/Achievements.bin").ToCharArray();
                    for (int i = 0; i < ach.Length; i++)
                    {
                        if (ach[i] == '0')
                        {
                            GameInfo.Achievements[i] = false;
                        }
                        else if (ach[i] == '1')
                        {
                            GameInfo.Achievements[i] = true;
                        }
                        //GameInfo.Achievements[i]=
                    }
                }
            }
            catch (System.Exception)
            {
            }

        }

    }

}