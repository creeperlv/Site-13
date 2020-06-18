using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Plugin
{

    public class PluginExecutor : MonoBehaviour
    {
        public string ID;
        public bool Load = true;
        public bool Unload = false;
        // Start is called before the first frame update
        void Start()
        {

            if (Load == true)
            {
                var RealID = ID + ".Load";
                if (PluginPool.functionsCollection.Data.ContainsKey(RealID))
                {

                    var functions = PluginPool.functionsCollection.Data[RealID];
                    foreach (var func in functions)
                    {
                        func.Run();
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}