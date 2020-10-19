using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Site13Kernel
{
    public class JumpScene : SCPStoryNodeBaseCode
    {
        public int SceneID = 0;
        public bool PreLoad = false;
        AsyncOperation SceneLoad;
        public Image BlackCover;
        public float Speed = 0;
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
            yield return new WaitForSeconds(0.5f);
            SceneLoad = SceneManager.LoadSceneAsync(SceneID);
            SceneLoad.allowSceneActivation = false;
            SceneLoad.completed += SceneLoad_completed;
            yield return null;
        }

        private void SceneLoad_completed(AsyncOperation obj)
        {
            obj.allowSceneActivation = false;
        }
        IEnumerator Jump()
        {
            if (GameInfo.CurrentGame.FirstPerson != null)
            {
                GameInfo.CurrentGame.FirstPerson.enabled = false;
            }
            if (Speed == 0)
            {
            }
            else
            {
                if (BlackCover != null)
                {

                    Color c = BlackCover.color;
                    c.a = 0;
                    BlackCover.color = c;
                    while (c.a < 0.99)
                    {
                        c.a += Time.deltaTime * Speed;
                        BlackCover.color = c;
                        yield return null;
                    }
                    c.a = 1;
                    BlackCover.color = c;
                }
            }

            if (PreLoad == false)
            {
                SceneManager.LoadScene(SceneID);
            }
            else
            {
                SceneLoad.allowSceneActivation = true;
            }
        }
        public override void StartStory()
        {
            StartCoroutine(Jump());
            //SceneLoad.allowSceneActivation = true;
            //SceneManager.LoadScene(SceneID);
        }
    }

}