using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Site13Kernel.DynamicScene
{

    public class SceneLightManager : MonoBehaviour
    {
        public static SceneLightManager CurrentManager;
        public static Dictionary<string, bool> LightStates = new Dictionary<string, bool>();
        internal bool LightState;
        public bool DefaultCurrentSceneState;
        public List<ModularSceneLightControlEntity> ControlledEntities = new List<ModularSceneLightControlEntity>();
        string current;
        private void Start()
        {
            CurrentManager = this;
            current = SceneManager.GetActiveScene().name;
            if (!LightStates.ContainsKey(current))
                LightStates.Add(current, DefaultCurrentSceneState);
            LightState = LightStates[current];
        }
        public void RegisterEntity(ModularSceneLightControlEntity entity)
        {
            if (!ControlledEntities.Contains(entity))
                ControlledEntities.Add(entity);

            if (entity.LightGroup == current)
                entity.gameObject.SetActive(LightStates[current]);
        }
        public void UnregisterEntity(ModularSceneLightControlEntity entity)
        {
            if (ControlledEntities.Contains(entity))
                ControlledEntities.Remove(entity);
        }
        public void SetLightState(bool value)
        {
            LightStates[current] = value;
            for (int i = 0; i < ControlledEntities.Count; i++)
            {
                if (ControlledEntities[i] != null)
                {
                    if (ControlledEntities[i].LightGroup == current)
                        ControlledEntities[i].gameObject.SetActive(value);
                }
                else ControlledEntities.RemoveAt(i);
            }
        }
        public bool GetCurrentLightState() => DefaultCurrentSceneState;
    }

}