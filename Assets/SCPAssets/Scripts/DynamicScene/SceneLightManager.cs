using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Site13Kernel
{

    public class SceneLightManager : MonoBehaviour
    {
        public static Dictionary<string, bool> LightStates = new Dictionary<string, bool>();
        internal bool LightState;
        public bool DefaultCurrentSceneState;
        private void Start()
        {
            var current = SceneManager.GetActiveScene().name;
            if (!LightStates.ContainsKey(current))
                LightStates.Add(current, DefaultCurrentSceneState);
            LightState = LightStates[current];
        }
        public bool GetCurrentLightState() => DefaultCurrentSceneState;
    }

}