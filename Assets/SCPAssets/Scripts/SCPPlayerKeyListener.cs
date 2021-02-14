using Site13Kernel.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
namespace Site13Kernel
{

    public class SCPPlayerKeyListener : MonoBehaviour
    {
        public GameObject DebugPanel;
        public GameObject PausePanel;
        public GameObject ItemsTip;
        public GameObject NaviHUD;
        public Button ContinueGame;
        public Button MainMenuButton;
        public Button SaveButtonToRegister;
        public Savable CurrentSaveSystem;
        public PostProcessVolume layer;
        public PostProcessProfile Normal;
        public PostProcessProfile NightVision;
        public bool isNightVisionEnabled;
        void Start()
        {
            ContinueGame.onClick.AddListener(delegate
            {
                PausePanel.SetActive(false);
                GameInfo.CurrentGame.isPaused = false;
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                GameInfo.CurrentGame.FirstPerson.enabled = true;
                AudioListener.pause = false;
            });
            SaveButtonToRegister.onClick.AddListener(delegate
            {
                try
                {
                    CurrentSaveSystem.Save();
                }

                catch (System.Exception e)
                {
                    Debug.Log(e);
                }
            });
        }
        // Update is called once per frame
        void Update()
        {
            if (GameInfo.CurrentGame.isPaused == false)
            {
                if (Input.GetButton("Bag"))
                {
                    if (ItemsTip.activeInHierarchy == false)
                    {
                        ItemsTip.SetActive(true);
                        NaviHUD.SetActive(false);
                    }

                }
                else
                {
                    if (ItemsTip.activeInHierarchy == true)
                    {
                        ItemsTip.SetActive(false);
                        NaviHUD.SetActive(true);
                    }
                }
                if (Input.GetKey( KeyCode.F5))
                {
                    AudioListener.pause = true;
                    GameInfo.CurrentGame.isPaused = true;
                    GameInfo.CurrentGame.FirstPerson.enabled = false;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    DebugPanel.SetActive(true);
                    Time.timeScale = 0;

                }
                if (Input.GetButton("Cancel"))
                {
                    if (GameInfo.CurrentGame.isPauseAllowed == true)
                    {
                        AudioListener.pause = true;
                        GameInfo.CurrentGame.isPaused = true;
                        GameInfo.CurrentGame.FirstPerson.enabled = false;
                        Cursor.lockState = CursorLockMode.Confined;
                        Cursor.visible = true;
                        PausePanel.SetActive(true);
                        Time.timeScale = 0;
                    }
                    else
                    {
                        GameInfo.CurrentGame.notification.ShowNotification("You cannot pause now.");
                    }
                }
                if (isNightVisionEnabled)
                {
                    if (Input.GetKeyDown(KeyCode.V))
                    {
                        if (layer.profile == Normal)
                        {
                            layer.profile = NightVision;
                        }
                        else
                        {
                            layer.profile = Normal;
                        }
                    }
                }
            }
        }

    }
}
