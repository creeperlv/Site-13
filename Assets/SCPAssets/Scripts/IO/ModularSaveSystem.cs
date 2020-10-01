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
        void Start()
        {
            if(Application.platform == RuntimePlatform.WindowsPlayer)
            {
                SavePath = ".";
            }
            else if (Application.platform == RuntimePlatform.WSAPlayerX64 || Application.platform == RuntimePlatform.WSAPlayerX64 || Application.platform == RuntimePlatform.LinuxPlayer || Application.platform == RuntimePlatform.OSXPlayer)
            {
                SavePath = Application.dataPath;
            }
            SavePath = Path.Combine(SavePath, SaveSystemID);
            GameInfo.CurrentGame.CurrentSceneSaveSystem = this;
        }

    }

}