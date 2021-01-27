using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Site13Kernel.UI
{

    public class EnterMainMenu : MonoBehaviour
    {
        AsyncOperation operation;
        public CanvasGroup cg0;
        public CanvasGroup cg1;
        public int SceneID;
        float CurrentProgress = 0;
        bool flag = false;
        void Start()
        {
            StartCoroutine(Navigate());

        }
        IEnumerator Navigate()
        {
            operation = SceneManager.LoadSceneAsync(SceneID);
            operation.allowSceneActivation = false;
            yield return operation;
        }
        IEnumerator ShowMainMenu()
        {
            operation.allowSceneActivation = true;
            yield return null;
            for (float i = 0; i < 1; i += Time.deltaTime)
            {
                cg0.alpha = 1 - i;
                cg1.alpha = 1 - i;
                yield return null;
            }
            cg0.alpha = 0;
            cg1.alpha = 0;
            SceneManager.UnloadSceneAsync(1);
        }
        void Update()
        {

            if (CurrentProgress < 0.9)
            {
                CurrentProgress = operation.progress * 100;

            }
            else if (!flag)
            {
                CurrentProgress++;
                if (CurrentProgress > 100)
                {
                    CurrentProgress = 100;
                }
            }
            if (CurrentProgress >= 100)
            {
                flag = true;
                StartCoroutine(ShowMainMenu());
            }
        }
    }

}