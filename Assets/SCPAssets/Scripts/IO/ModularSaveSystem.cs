using CLUNL.Data.Layer0.Buffers;
using Site13Kernel.DynamicScene;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Site13Kernel.IO
{

    public class ModularSaveSystem : MonoBehaviour, ISave
    {
        public string SaveSystemID;
        /// <summary>
        /// Parent path.
        /// </summary>
        public string SavePath { get; private set; }
        public string OriginalSavePath { get; private set; }
        public SCPFirstController MainController;
        public Dictionary<string, ModularSaveSystemModule> Modules = new Dictionary<string, ModularSaveSystemModule>();
        public void Load()
        {
            foreach (var item in Modules)
            {
                item.Value.Load();
            }
        }

        public void Save()
        {
            foreach (var item in Modules)
            {
                item.Value.Save();
            }
        }
        public void LoadGI()
        {
            {
                DataBuffer dataBuffer = DataBuffer.FromByteArray(File.ReadAllBytes(FPSControllerInfoPath));
                MainController.gameObject.SetActive(dataBuffer.ReadBool());
                ByteBuffer byteBuffer = dataBuffer.ObtainByteArray();
                var FPSTransform=Utilities.FromBytes(byteBuffer.GetGroup());
                MainController.transform.position = FPSTransform.Item1;
                MainController.transform.rotation = FPSTransform.Item2;
                MainController.transform.localScale = FPSTransform.Item3;
            }
        }
        public void SaveGI()
        {
            {

                DataBuffer dataBuffer = new DataBuffer();
                dataBuffer.WriteBool(PrimarySceneController.CurrentPrimaryScene.MainCharacter.gameObject.activeSelf);
                ByteBuffer byteBuffer = dataBuffer.ObtainByteArray();
                byteBuffer.AppendGroup(Utilities.FromTransform(PrimarySceneController.CurrentPrimaryScene.MainCharacter.transform));
                File.WriteAllBytes(FPSControllerInfoPath, byteBuffer);
            }
            {

                DataBuffer dataBuffer = new DataBuffer();
                dataBuffer.WriteInt(GameInfo.CurrentGame.FlagsGroup.Count);
                foreach (var item in GameInfo.CurrentGame.FlagsGroup)
                {
                    dataBuffer.WriteString(item.Key);
                    dataBuffer.WriteString(item.Value);
                }
                dataBuffer.WriteInt(GameInfo.CurrentGame.EnemyStatusGroup.Count);
                foreach (var item in GameInfo.CurrentGame.EnemyStatusGroup)
                {
                    dataBuffer.WriteString(item.Key);
                    dataBuffer.WriteString(item.Value);
                }
                File.WriteAllBytes(GeneralInfoPath, dataBuffer.ObtainByteArray());
            }
        }
        public string GeneralInfoPath;
        public string FPSControllerInfoPath;
        void Start()
        {
            if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                OriginalSavePath = $"./Saves/{GameInfo.CurrentGame.SaveName}/MSS/";
            }
            else if (Application.platform == RuntimePlatform.WSAPlayerX64 || Application.platform == RuntimePlatform.WSAPlayerX64 || Application.platform == RuntimePlatform.LinuxPlayer || Application.platform == RuntimePlatform.OSXPlayer)
            {
                OriginalSavePath = Path.Combine(Application.dataPath, "Saves", GameInfo.CurrentGame.SaveName, "MSS");
            }
            else
            {
                OriginalSavePath = $"./Saves/{GameInfo.CurrentGame.SaveName}/Editor/MSS/";
            }
            SavePath = Path.Combine(OriginalSavePath, SaveSystemID);
            if (!Directory.Exists(OriginalSavePath)) Directory.CreateDirectory(OriginalSavePath);
            if (!Directory.Exists(SavePath)) Directory.CreateDirectory(SavePath);
            GameInfo.CurrentGame.CurrentSceneSaveSystem = this;
            GeneralInfoPath = Path.Combine(OriginalSavePath, "GenInfo.bin");
            FPSControllerInfoPath = Path.Combine(SavePath, "FPSController.bin");
            bool isLoadGI = false;
            if (!File.Exists(GeneralInfoPath))
            {
                File.Create(GeneralInfoPath).Close();
            }
            else
            {
                isLoadGI = true;
            }
            if (!File.Exists(FPSControllerInfoPath))
            {
                isLoadGI = false;
                File.Create(FPSControllerInfoPath).Close();
            }
            if (isLoadGI == true) LoadGI();
        }
        bool FirstLoad = false;
        void Update()
        {
        }
    }

}