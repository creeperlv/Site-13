using Site13Kernel.Audio;
using Site13Kernel.EFI;
using Site13Kernel.Experimentals.OoD.V1;
using Site13Kernel.Experimentals.OoD.V2;
using Site13Kernel.GameLogic;
using Site13Kernel.GameLogic.CampaignActions;
using Site13Kernel.IO;
using Site13Kernel.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Site13Kernel
{
    public class GameInfo
    {
        public static GameDefinition CurrentGameDef;
        public static List<CampaignAction> Script;
        public static AudioSource MainUIBGM=null;
        public static GameInfo CurrentGame = new GameInfo("DEBUG");
        public int BaseSeed = int.MinValue+1;
        private static List<bool> achievements = new List<bool>();
        public Blinker PlayerBlink;
        public SCPBGMManager BGMManager;
        public SCPCameraShake cameraShake;
        public SCPFootStepSFXManager currentFootStepSFXManager;
        public SecondaryNotification secondaryNotification;
        public bool isPaused = false;
        public bool isPauseAllowed = true;
        public bool isUltraState= false;
        public bool isAimedEntity = false;
        public bool isHealthSpeedDisabled= false;
        public bool isWatching = false;
        public bool isAiming= false;
        public bool isAimingEnded= false;
        public bool isRunning = false;
        public float DefaultFOV= 60;
        public float UsingFOV= 60;
        public SCPFirstController FirstPerson;
        public bool isCurrentArrived = false;
        public SCPEntity PlayerHealth;
        public string SaveName = "DEBUG";
        public int PlayerScene = 0;
        public float AssistAim = 0.1f;
        public int[] Ammos = { 100 };
        public static float TargetMouseSen = 10f;
        public static float CurrentMouseSen = 10f;
        public bool[] EquippedItems = { true, true, true, true, true, true, false };
        public float[] LeftAmmos = { 100, 0, 100, 1 };
        public string CurrentScene = "SectionBLevel1";
        public int NextScene = 5;
        public SubtitleController PublicSubtitle;
        public Notification notification;
        public AchievementShower achievement;
        public float LoadedHP = 100.0f;
        public ISave CurrentSceneSaveSystem;
        public Dictionary<string, string> EnemyStatusGroup = new Dictionary<string, string>();
        public Dictionary<string, string> FlagsGroup = new Dictionary<string, string>();
        public EnterSourceType EnterSource = EnterSourceType.Tunnel1;
        public SaveControlProtocolMode saveControlProtocolMode = SaveControlProtocolMode.NewGame;
        public int PossessingFAK = 3;
        public int Bats = 0;
        public float SpeedFactor = 1.0f;
        public float[] PlayerLocation = { 0, 0, 0 };
        public float[] PlayerRotation = { 0, 0, 0 };
        public HandableItem HandingItem=null;
        public string DeathText = "Your dead body was found by <color=#A0A0A0>Team Charlie Yukon</color> later and they drag your body to the body pit.";
        public SCPOoDV1Manager CurrentOoDManager;
        public SCPOoDV2Manager CurrentOoDManagerV2;
        public static List<bool> Achievements { get => achievements; set { achievements = value;} }

        public string GetCurrentSceneSaveName()
        {
            return (SaveName + "_" + CurrentScene);
        }

        public void LoadGI_PlayerInfo(GameObject Player)
        {
            SaveControlProtocol.TargetFile = SaveName + "_GI";
            var data = SaveControlProtocol.Load();
            foreach (var item in data)
            {
                var key = item.Key;
                if (key == "PlayerRotation")
                {
                    var l = item.Value.Split(',');
                    {
                        var lr = Player.transform.localRotation;
                        var angle = lr.eulerAngles;
                        angle.x = float.Parse(l[0]);
                        angle.y = float.Parse(l[1]);
                        angle.z = float.Parse(l[2]);
                        lr.eulerAngles = angle;
                        Player.transform.localRotation = lr;
                    }
                }
                else if (key == "PlayerLocation")
                {
                    var l = item.Value.Split(',');
                    {
                        var lp = Player.transform.localPosition;
                        lp.x = float.Parse(l[0]);
                        lp.y = float.Parse(l[1]);
                        lp.z = float.Parse(l[2]);
                        Player.transform.localPosition = lp;
                    }
                }
            }
        }
        public enum EnterSourceType
        {
            None,
            Stair1,
            Stair2,
            Lift,
            Tunnel1
        }
        public enum SaveControlProtocolMode
        {
            NewGame,
            Load,
            Enter,//Enter is different from load, when saveControlProtocolMode is Enter, it won't load player's position,
            //instead, it will set player directly to the lift/stair.
        }
        public void Load()
        {

        }
        public void SaveGeneralInfo()
        {
            SaveControlProtocol.TargetFile = SaveName + "_GI";
            Dictionary<string, string> Data = new Dictionary<string, string>();
            {
                Data.Add("PlayerHP", PlayerHealth.CurrentHealth + "");
                Data.Add("PlayerLocation", $"{PlayerLocation[0]},{PlayerLocation[1]},{PlayerLocation[2]}");
                Data.Add("PlayerRotation", $"{PlayerRotation[0]},{PlayerRotation[1]},{PlayerRotation[2]}");
                Data.Add("CurrentMap", $"{PlayerScene}");
                Data.Add("CurrentFAK", $"{PossessingFAK}");
                Data.Add("CurrentBAT", $"{Bats}");
                Data.Add("LeftAmmo", $"{LeftAmmos[0]},{LeftAmmos[1]},{LeftAmmos[2]}");
                {
                    var str = "";
                    foreach (var item in EquippedItems)
                    {
                        str += item + ",";
                    }
                    str = str.Substring(0, str.Length - 1);
                    Data.Add("EquippedItems",str);
                }
                foreach (var item in EnemyStatusGroup)
                {
                    Data.Add($"ESG.{item.Key}", $"{item.Value}");
                }
                foreach (var item in FlagsGroup)
                {
                    Data.Add($"FLAG.{item.Key}", $"{item.Value}");
                }
            }
            SaveControlProtocol.Save(Data);
        }

        public void LoadGI()
        {
            SaveControlProtocol.TargetFile = SaveName + "_GI";
            var data = SaveControlProtocol.Load();
            foreach (var item in data)
            {
                var key = item.Key;
                if (key.StartsWith("ESG."))
                {
                    var ESGName = key.Substring("ESG.".Length);
                    try
                    {
                        EnemyStatusGroup.Add(ESGName, item.Value);
                    }
                    catch (System.Exception)
                    {
                        try
                        {
                            EnemyStatusGroup[ESGName] = item.Value;
                        }
                        catch (System.Exception)
                        {
                        }
                    }
                }
                else if (key.StartsWith("FLAG."))
                {
                    var FLGName = key.Substring("FLAG.".Length);
                    try
                    {
                        FlagsGroup.Add(FLGName, item.Value);
                    }
                    catch (System.Exception)
                    {
                        try
                        {
                            FlagsGroup[FLGName] = item.Value;
                        }
                        catch (System.Exception)
                        {
                        }
                    }
                }
                else if (key == "PlayerHP")
                {
                    LoadedHP = float.Parse(item.Value);
                }
                else if (key == "CurrentFAK")
                {
                    PossessingFAK = int.Parse(item.Value);
                }
                else if (key == "CurrentBAT")
                {
                    Bats = int.Parse(item.Value);
                }
                else if (key == "LeftAmmo")
                {
                    var l = item.Value.Split(',');
                    LeftAmmos[0] = float.Parse(l[0]);
                    LeftAmmos[1] = float.Parse(l[1]);
                    LeftAmmos[2] = float.Parse(l[2]);
                }
                else if (key == "CurrentMap")
                {
                    PlayerScene = int.Parse(item.Value);
                }
                else if (key == "EquippedItems")
                {
                    var bools = item.Value.Split(',');
                    for (int i = 0; i < bools.Length; i++)
                    {
                        if (bools[i] == "true")
                        {
                            EquippedItems[i] = true;
                        }else
                        if (bools[i] == "false")
                        {
                            EquippedItems[i] = false;
                        }
                    }
                }
            }
        }
        public void Save()
        {

        }
        public GameInfo(string SN)
        {
            SaveName = SN;
        }
        public static void SaveAchievements()
        {
            Debug.Log("Save Achievement Data");
            string generatedFileContent = "";
            for (int i = 0; i < Achievements.Count; i++)
            {
                if (Achievements[i] == false)
                {
                    generatedFileContent += '0';
                }
                else
                {
                    generatedFileContent += '1';
                }
            }
            //if (!File.Exists("./Saves/Achievements.bin"))
            //{
            //    File.Create("./Saves/Achievements.bin");
            //}
            File.WriteAllText("./Saves/Achievements.bin", generatedFileContent);
        }
    }
    public class EnvironmentInfo
    {
        public static string UnityVersion = Application.unityVersion;
        public static string BuildPlatform = "Windows 10 Build UNKNOWN";
    }
    public class StaticResources
    {
        public static ExternalMaterialLoader MaterialLoader;
    }
    public class SystemSettings
    {
        public static int W;
        public static int H;
        public static int QualityLevel = 5;
        public static bool isFullscreen;
    }
}