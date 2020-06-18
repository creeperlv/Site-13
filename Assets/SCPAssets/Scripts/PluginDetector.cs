using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Plugin
{
    public class PluginDetector : MonoBehaviour
    {
        public string ID;
        public bool Enter;
        public bool Exit;
        public bool Stay;
        private bool isEntered = false;
        private void Update()
        {
            if (isEntered == true)
            {
                var RealID = ID + ".Stay";
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
        private void OnTriggerEnter(Collider other)
        {

            Debug.Log("Collision entered.");
            if (other.gameObject.GetComponent<SCPFirstController>() != null)
            {
                isEntered = true;
                Debug.Log("Player entered.");
                if (Enter)
                {
                    var RealID = ID + ".OnEntered";
                    if (PluginPool.functionsCollection.Data.ContainsKey(RealID))
                    {
                        var functions = PluginPool.functionsCollection.Data[RealID];
                        foreach (var func in functions)
                        {
                            Debug.Log("Hit:" + func.GetType().Name);
                            Debug.Log("Try to run:" + func.PluginID);
                            func.Run();
                        }
                    }
                }
                //Entered
            }
        }
        private void OnTriggerExit(Collider other)
        {

            if (other.gameObject.GetComponent<SCPFirstController>() != null)
            {
                isEntered = false;
                //Entered
                if (Exit)
                {
                    var RealID = ID + ".OnExited";
                    if (PluginPool.functionsCollection.Data.ContainsKey(RealID))
                    {

                        var functions = PluginPool.functionsCollection.Data[RealID];
                        foreach (var func in functions)
                        {
                            Debug.Log("Try to run:" + func.PluginID);
                            func.Run();
                        }
                    }
                }
            }
        }
    }


}