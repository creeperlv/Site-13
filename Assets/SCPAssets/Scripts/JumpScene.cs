using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Site13Kernel
{
    public class JumpScene : SCPStoryNodeBaseCode
    {
        public int SceneID = 0;
        public bool PreLoad = false;
        AsyncOperation SceneLoad;
        public override void StartScript()
        {
            if (PreLoad == true)
            {

                Debug.Log("Preload Scene");
                StartCoroutine(LoadAsync());
            }
        }
        IEnumerator LoadAsync()
        {
            yield return new WaitForSeconds(1f);
            SceneLoad = SceneManager.LoadSceneAsync(SceneID);
            SceneLoad.allowSceneActivation = false;
            SceneLoad.completed += SceneLoad_completed;
            yield return null;
        }

        private void SceneLoad_completed(AsyncOperation obj)
        {
            obj.allowSceneActivation = false;
        }

        public override void StartStory()
        {
            if (PreLoad == false)
            {
                SceneManager.LoadScene(SceneID);
            }
            else
            {
                SceneLoad.allowSceneActivation = true;
            }
            //SceneLoad.allowSceneActivation = true;
            //SceneManager.LoadScene(SceneID);
        }
    }

}