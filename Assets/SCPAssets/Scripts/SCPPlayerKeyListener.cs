using HUDNavi;
using Site_13ToolLib.Globalization;
using Site13Kernel.FPSSystem;
using Site13Kernel.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
namespace Site13Kernel
{

    public class SCPPlayerKeyListener : MonoBehaviour
    {
        public GameObject DebugPanel;
        public GameObject HintPanel;
        public GameObject PausePanel;
        public GameObject ItemsTip;
        public GameObject NaviHUD;
        public Text HintText;
        public Button ContinueGame;
        public Button MainMenuButton;
        public Button SaveButtonToRegister;
        public Savable CurrentSaveSystem;
        public PostProcessVolume layer;
        public PostProcessProfile Normal;
        public PostProcessProfile NightVision;
        public bool isNightVisionEnabled;
        List<InteractiveObjectV2> interactiveObjects = new List<InteractiveObjectV2>();
        public void AddInteractiveObject(InteractiveObjectV2 obj)
        {
            if (!interactiveObjects.Contains(obj)) interactiveObjects.Add(obj);
            ShowHint();
        }
        public void RemoveInteractiveObject(InteractiveObjectV2 obj)
        {
            if (interactiveObjects.Contains(obj)) interactiveObjects.Remove(obj);
        }
        public void ShowHint() { ShowHint(0); }
        public void HideHint() { HintPanel.SetActive(false); }
        public void ShowHint(int index)
        {
            HintPanel.SetActive(true);
            var obj = interactiveObjects[index];
            string LangString = Language.GetString(LanguageCategory.UI, "InteractiveObject", "Press [{0}] to {1} {2}");
            string Key0 = "";
            string Key1 = "";
            if (Keyboard.current != null) Key0 = SCPInputSystem.CurrentInputSystem.GetKey("Interact", FPSSystem.DeviceType.Keyboard);
            if (Gamepad.current != null) Key1 = SCPInputSystem.CurrentInputSystem.GetKey("Interact", FPSSystem.DeviceType.Keyboard);
            string FinalKey = "";
            if(Key0 != null)
            {
                FinalKey = Key0;
                if (Key1 != null)
                {
                    FinalKey += "/"+Key1;
                }
            }
            else
            {
                if (Key1 != null)
                {
                    FinalKey = Key1;
                }
            }
            string Operation = "";
            switch (obj.InteractAction)
            {
                case InteractAction.SwitchWeapon:
                    Operation = Language.GetString(LanguageCategory.UI, "Interact.SwitchWeapon", "switch to");
                    break;
                case InteractAction.PickUp:
                    Operation = Language.GetString(LanguageCategory.UI, "Interact.SwitchWeapon", "pick up");
                    break;
                case InteractAction.Use:
                    Operation = Language.GetString(LanguageCategory.UI, "Interact.SwitchWeapon", "use");
                    break;
                case InteractAction.None:
                    Operation = Language.GetString(LanguageCategory.UI, "Interact.SwitchWeapon", "");
                    break;
                case InteractAction.Check:
                    Operation = Language.GetString(LanguageCategory.UI, "Interact.SwitchWeapon", "check");
                    break;
                default:
                    break;
            }
            string FinalHint =string.Format(LangString,FinalKey,Operation,Language.GetString(LanguageCategory.Sign,obj.DisplayNameID,obj.DisplayNameFallback));
            HintText.text = FinalHint;
        }
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
            NavigationCore.CurrentCore.CullNaviPoint("Info");
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
                        NavigationCore.CurrentCore.UncullNaviPoint("Info");
                    }

                }
                else
                {
                    if (ItemsTip.activeInHierarchy == true)
                    {
                        ItemsTip.SetActive(false);
                        NavigationCore.CurrentCore.CullNaviPoint("Info");
                    }
                }
                if (Input.GetKey(KeyCode.F5))
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
