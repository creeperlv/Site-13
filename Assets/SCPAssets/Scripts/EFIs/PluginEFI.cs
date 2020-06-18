using Site13Kernel.Plugin;
using Site_13_Plug_in_Lib;
using Site_13ToolLib.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Site13Kernel.EFI
{
    public class PluginEFI : EFIBase
    {
        public override void Run()
        {
            {

                {
                    //Clear old pool
                    PluginPool.OnBootloader = new List<ISite13Plugin>();
                    PluginPool.PluginCount = 0;
                    PluginPool.functionsCollection = new Site_13_Plug_in_Lib.PluginSystem.FunctionsCollection();
                    PluginPool.Plugins = new List<PluginInfo>();
                }
                APIController.AddMethod("ChangeHealth", (SCPParameter parameter) =>
                {
                    GameInfo.CurrentGame.PlayerHealth.ChangeHealth((float)parameter[0]);
                    return null;
                });
                APIController.AddMethod("SHOWSUB", (SCPParameter parameter) =>
                {
                    GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle((string)parameter[0], (float)parameter[1]);
                    return null;
                });
                APIController.AddMethod("ACHIEVE", (SCPParameter parameter) =>
                {
                    var AchievementID = (int)parameter[0];
                    if (GameInfo.Achievements.Count == 0)
                    {
                        GameInfo.CurrentGame.achievement.ShowAchievement(AchievementID);
                    }
                    else
                    if (AchievementID != -1)
                        if (GameInfo.Achievements[AchievementID - 1] == false)
                        {
                            GameInfo.Achievements[AchievementID - 1] = true;
                            GameInfo.SaveAchievements();
                            GameInfo.CurrentGame.achievement.ShowAchievement(AchievementID);
                        }
                    return null;
                });
                APIController.AddMethod("GETDELTATIME", (SCPParameter parameter) =>
                {
                    return Time.deltaTime;
                });
                APIController.AddMethod("MOD-MAT-TXTURE", (SCPParameter parameter) =>
                {
                    ExternalMaterialLoader.ModifyMaterialMainTexture((Site_13_Plug_in_Lib.Game.GamingEnvironment.Materials.EditableMaterials)parameter[0], (string)parameter[1], (int)parameter[2], (int)parameter[3]);
                    return null;
                });
                APIController.AddMethod("MOD-MAT-HEIGHT", (SCPParameter parameter) =>
                {
                    ExternalMaterialLoader.ModifyMaterialHeight((Site_13_Plug_in_Lib.Game.GamingEnvironment.Materials.EditableMaterials)parameter[0], (float)parameter[1]);
                    return null;
                });
            }

            DirectoryInfo pluginFolder = new DirectoryInfo("./Plugins/");
            if (!pluginFolder.Exists)
            {
                pluginFolder.Create();
            }
            var Folders = pluginFolder.EnumerateDirectories();
            foreach (var item in Folders)
            {
                var Fs=item.EnumerateFiles();
                bool isDisabled=false;
                string Enterance="";
                string Name="";
                PluginInfo pluginInfo = new PluginInfo();
                foreach (var item2 in Fs)
                {
                    if (item2.Name == "DISABLE")
                    {
                        isDisabled = true;
                        pluginInfo.isDisabled = true;
                    }
                    if (item2.Name == "Manifest")
                    {
                        var mani=GOCDataV1.Load(item2.FullName, false);
                        Enterance = mani["Entrance"];
                        Name = mani["Name"];
                        pluginInfo.PluginEntrance = Enterance;
                        pluginInfo.PluginName = Name;
                    }
                }
                if (isDisabled == false)
                {
                    var pi = System.Reflection.Assembly.LoadFile(item.FullName+"/"+Enterance);
                    foreach (var t in pi.GetTypes())
                    {
                        if (t.GetInterface("IPluginInformation") != null)
                        {
                            var pii = (IPluginInformation)Activator.CreateInstance(t);
                            Debug.Log(pii.GetType().Assembly);
                            PluginPool.PluginCount++;
                            var mi = pii.GetPluginInformation();
                            Debug.Log("Try add:" + mi.PluginID);
                            pluginInfo.PluginID = mi.PluginID;
                            foreach (var func in mi.OnBootloader)
                            {
                                func.PluginID = mi.PluginID;
                                PluginPool.OnBootloader.Add(func);
                            }
                            foreach (var functions in mi.functionsCollection.Data)
                            {
                                foreach (var function in functions.Value)
                                {
                                    function.PluginID = mi.PluginID;
                                    Debug.Log("Try add " + function.GetType().Name + " to " + functions.Key);
                                    PluginPool.functionsCollection.SetFunction(functions.Key, function);
                                }
                            }
                            foreach (var func in mi.OnBootloader)
                            {
                                func.PluginID = mi.PluginID;
                                PluginPool.OnBootloader.Add(func);
                            }
                        }
                    }

                }
            }
            {
                //BootLoaderExecution
                foreach (var item in PluginPool.OnBootloader)
                {
                    item.Run();
                }
            }
            //var Files = pluginFolder.EnumerateFiles("*.dll");
            //foreach (var item in Files)
            //{
            //    //try
            //    //{
            //    //var pi = System.Reflection.Assembly.LoadFile(item.FullName);
            //    //foreach (var t in pi.GetTypes())
            //    //{
            //    //    if (t.GetInterface("IPluginInformation") != null)
            //    //    {
            //    //        var pii = (IPluginInformation)Activator.CreateInstance(t);
            //    //        Debug.Log(pii.GetType().Assembly);
            //    //        PluginPool.PluginCount++;
            //    //        var mi = pii.GetPluginInformation();
            //    //        Debug.Log("Try add:" + mi.PluginID);
            //    //        foreach (var func in mi.OnBootloader)
            //    //        {
            //    //            func.PluginID = mi.PluginID;
            //    //            PluginPool.OnBootloader.Add(func);
            //    //        }
            //    //        foreach (var functions in mi.functionsCollection.Data)
            //    //        {
            //    //            foreach (var function in functions.Value)
            //    //            {
            //    //                function.PluginID = mi.PluginID;
            //    //                Debug.Log("Try add "+function.GetType().Name+" to "+ functions.Key);
            //    //                PluginPool.functionsCollection.SetFunction(functions.Key, function);
            //    //            }
            //    //        }
            //    //    }
            //    //}
            //    //}
            //    //catch (Exception e)
            //    //{
            //    //}
            //}
        }
    }

}