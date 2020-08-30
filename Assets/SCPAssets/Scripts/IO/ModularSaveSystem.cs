using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.IO
{

    public class ModularSaveSystem : MonoBehaviour, ISave
    {
        public int SaveSystemID;
        public string SavePath { get { return "Undefined."; } }
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

        // Start is called before the first frame update
        void Start()
        {
            GameInfo.CurrentGame.CurrentSceneSaveSystem = this;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}