using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Site13Kernel.DynamicScene
{
    public class ModularSceneLightControlEntity : MonoBehaviour
    {
        public string LightGroup;
        void Start()
        {
            StartCoroutine(Register());
        }
        IEnumerator Register()
        {
            yield return null;
            yield return null;
            //Skip 2 frame to await SceneLightManager to initialize;
            SceneLightManager.CurrentManager.RegisterEntity(this);
        }
        private void OnDestroy()
        {
            SceneLightManager.CurrentManager.UnregisterEntity(this);
            
        }
    }

}