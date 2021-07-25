using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Site13Kernel.UI
{
    public class FirstScreen : MonoBehaviour
    {
        public Image Cover;
        public TMPro.TMP_Text HintText;
        public int MainMenuSceneID;
        Color c1;
        Color c2;
        void Start()
        {
            c1 = Cover.color;
            c2 = HintText.color;
        }
        int State0=0;
        bool State1=false;
        void Update()
        {
            if (State0 < 2)
                if (Input.anyKeyDown)
                {
                    HintText.gameObject.SetActive(false);
                    State0 = 2;
                }
            if (State1)
            {
                c2.a -= Time.deltaTime;
                HintText.color = c2;
                if (c2.a <= 0)
                    State1 = !State1;
            }
            else
            {
                c2.a += Time.deltaTime;
                HintText.color = c2;
                if (c2.a >= 1)
                    State1 = !State1;
            }

            switch (State0)
            {
                case 0:
                    {
                        c1.a -= Time.deltaTime;
                        Cover.color = c1;
                        if (c1.a <= 0)
                        {
                            State0 = 1;
                        }
                    }
                    break;
                case 1:
                    {

                    }
                    break;
                case 2:
                    {

                        c1.a += Time.deltaTime;
                        Cover.color = c1;
                        if (c1.a >= 1)
                        {
                            State0 = 3;
                        }
                    }
                    break;
                case 3:
                    {
                        SceneManager.LoadScene(MainMenuSceneID);
                    }
                    break;
                default:
                    break;
            }
        }
    }

}