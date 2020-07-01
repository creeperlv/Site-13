using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Site13Kernel.DynamicScene
{
    public class SceneDetector : MonoBehaviour
    {
        public int TargetSceneID = -1;
        public string TargetSceneName = "#";
        public bool AutoLoad=false;
        Scene Scene;
        // Start is called before the first frame update
        void Start()
        {
            if (AutoLoad == true)
            {
                LoadTargetScene();
            }
        }
        void LoadTargetScene()
        {

            if (TargetSceneName != "#")
            {
                try
                {
                    SceneManager.LoadScene(TargetSceneName, LoadSceneMode.Additive);
                }
                catch (System.Exception)
                {
                    if (TargetSceneID != -1)
                    {
                        SceneManager.LoadScene(TargetSceneID, LoadSceneMode.Additive);
                    }
                }
            }
        }
        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            
        }
        private void OnTriggerExit(Collider other)
        {
            
        }
    }

}