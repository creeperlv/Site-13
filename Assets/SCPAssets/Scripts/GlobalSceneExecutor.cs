using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Site13Kernel.DynamicScene
{

    public class GlobalSceneExecutor : MonoBehaviour
    {
        public static GlobalSceneExecutor CurrentExecutor;
        public GameObject Player;
        public GameObject BlackCover;
        public LightingSettings lightingSettings;
        void Start()
        {
            CurrentExecutor = this;
        }
        public void Invoke(Action a)
        {
            a();
        }
        public void SartCoroutine(IEnumerator enumerator)
        {
            StartCoroutine(enumerator);
        }
        public void ChangeSkybox()
        {
        }
        public Scene ForceLoadSubScene(string SceneName)
        {

            LoadSceneParameters loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Additive);
            return SceneManager.LoadScene(SceneName, loadSceneParameters);
        }
        public void UnloadSubScene(string SceneName)
        {
            SceneManager.UnloadSceneAsync(SceneName);
        }
        public void UnloadSubsScene(Scene s)
        {
            SceneManager.UnloadSceneAsync(s);
        }
    }

}