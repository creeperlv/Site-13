using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Site_13ToolLib.Globalization;
using UnityEngine.SceneManagement;
using System.Linq;
using Site13Kernel.IO;
using UnityEngine.EventSystems;

namespace Site13Kernel
{

    public class SCPMainUI : MonoBehaviour
    {
        public Button NewGameButton;
        public Button SettingsButton;
        public Button AchievementButton;
        public GameObject AchieveTemplateButton;
        public Button AchievementOKButton;
        public Button SettingsCloseButton;
        public Button NewGameCancelButton;
        public Button NewGameOKButton;
        public Button ExitButton;
        public Button GeneralButton;
        public Button LoadButton;
        public Button LoadCloseButton;
        public Button AboutButton;
        public InputField SaveName;

        public Dropdown QualityComboBox;
        public Dropdown LanguageDropdown;
        public Dropdown ResolutionBox;

        public GameObject DialogPanel;
        public GameObject MainPage;
        public GameObject NewGamePanel;
        public GameObject AchievementsPanel;
        public GameObject AchievementsList;
        public GameObject SettingsPanel;
        public GameObject GeneralPanel;
        public GameObject RRPanel;
        public GameObject AboutPanel;
        public GameObject LoadPanel;
        public GameObject SavesList;
        public GameObject SingleSaveButton;

        public Text TitleText1;
        public Text TitleText2;
        public Text AboutText;

        public Slider FullscreenSwitch;

        public InputField NewGameSaveNameField;
        // Start is called before the first frame update
        void Start()
        {
            SettingsDeserialization();
            ExitButton.onClick.AddListener(delegate ()
            {
                Application.Quit();
            });
            NewGameButton.onClick.AddListener(delegate ()
            {
                ShowDialogPanel();
                NewGamePanel.SetActive(true);
            });
            SettingsButton.onClick.AddListener(delegate ()
            {
                ShowDialogPanel();
                SettingsPanel.SetActive(true);
            });
            AchievementButton.onClick.AddListener(delegate ()
            {
                ShowDialogPanel();
                AchievementsPanel.SetActive(true);
            });
            SettingsCloseButton.onClick.AddListener(delegate ()
            {
                HideDialogPanel();
            });
            LoadCloseButton.onClick.AddListener(delegate ()
            {
                HideDialogPanel();
            });
            GeneralButton.onClick.AddListener(delegate ()
            {
                HideAllSettings();
                GeneralPanel.SetActive(true);
            });
            AboutButton.onClick.AddListener(delegate ()
            {
                HideAllSettings();
                AboutPanel.SetActive(true);
            });
            NewGameCancelButton.onClick.AddListener(delegate ()
            {
                HideDialogPanel();
            });
            AchievementOKButton.onClick.AddListener(delegate ()
            {
                HideDialogPanel();
            });
            NewGameOKButton.onClick.AddListener(delegate ()
            {
                if (NewGameSaveNameField.text == "")
                {

                }
                else
                {
                    GameInfo.CurrentGame = new GameInfo(NewGameSaveNameField.text);
                    SceneManager.LoadScene(2);
                }
            });
            try
            {
                SetLanguage();
            }
            catch (System.Exception)
            {
            }
            try
            {

                var dp = QualityComboBox;
                {
                    dp.value = SystemSettings.QualityLevel;
                    dp.onValueChanged.AddListener(delegate (int e)
                    {
                        SystemSettings.QualityLevel = e;
                        QualitySettings.SetQualityLevel(e);
                        SerializeSettingsAndSave();
                    });
                }
            }
            catch (System.Exception)
            {
            }
            try
            {

                LoadButton.onClick.AddListener(delegate ()
                {
                    ShowDialogPanel();
                    LoadPanel.SetActive(true);
                    var saves = SaveControlProtocol.GetList();
                    var orilp = SingleSaveButton.transform.localPosition;
                    for (int i = 0; i < saves.Count; i++)
                    {
                        var newBtn = Instantiate(SingleSaveButton, SavesList.transform);
                        newBtn.SetActive(true);
                        orilp.y -= 35;
                        newBtn.transform.Find("Text").GetComponent<Text>().text = saves[i];
                        newBtn.transform.localPosition = orilp;
                    }
                });
            }
            catch (System.Exception)
            {
            }
            try
            {

                var dp = ResolutionBox;
                List<string> vs = new List<string>();
                {
                    dp.ClearOptions();
                    //Get Resolutions
                    var rs = Screen.resolutions;
                    foreach (var item in rs)
                    {
                        bool isUnique = true;
                        var cr = $"{item.width}×{item.height}";
                        foreach (var item2 in vs)
                        {
                            if (item2 == cr)
                            {
                                isUnique = false;
                            }
                        }
                        if (isUnique)
                            vs.Add(cr);
                    }
                    dp.AddOptions(vs);
                }
                {
                    var i = vs.IndexOf(SystemSettings.W + "×" + SystemSettings.H);
                    dp.value = i;
                    dp.onValueChanged.AddListener(delegate (int e)
                    {
                        var str = dp.options[e].text;
                        var Resolutions = str.Split('×');
                        SystemSettings.W = int.Parse(Resolutions[0]);
                        SystemSettings.H = int.Parse(Resolutions[1]);
                        Screen.SetResolution(SystemSettings.W, SystemSettings.H, Screen.fullScreen);
                        SerializeSettingsAndSave();
                    });
                }
            }
            catch (System.Exception)
            {
            }
            try
            {

                {
                    if (SystemSettings.isFullscreen)
                        FullscreenSwitch.value = 1;
                    FullscreenSwitch.onValueChanged.AddListener(delegate (float i)
                    {
                        if (i == 0)
                        {
                            SystemSettings.isFullscreen = false;
                            Screen.fullScreen = false;
                        }
                        else
                        {
                            SystemSettings.isFullscreen = true;
                            Screen.fullScreen = true;
                        }
                        SerializeSettingsAndSave();
                    });
                }
            }
            catch (System.Exception)
            {
            }
            try
            {

                Dictionary<string, string> list = new Dictionary<string, string>();
                ArrayList keys = new ArrayList();
                ArrayList values = new ArrayList();

                {

                    list = Site_13ToolLib.Data.GOCDataV1.Load("./ExtraResources/Languages/LanguageList.goc-data", false);
                    {
                        List<Dropdown.OptionData> lst = new List<Dropdown.OptionData>();
                        foreach (var item in list)
                        {
                            keys.Add(item.Key);
                            values.Add(item.Value);
                            Dropdown.OptionData optionData = new Dropdown.OptionData(item.Value);
                            lst.Add(optionData);
                        }
                        LanguageDropdown.AddOptions(lst);
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    if (list.ElementAt(i).Key == Language.GetLanguageInUse())
                    {
                        LanguageDropdown.value = i;
                        break;
                    }
                }

                LanguageDropdown.onValueChanged.AddListener(delegate (int i)
                {
                    RRPanel.SetActive(true);
                    try
                    {
                        Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Put("HKEY_LOCAL_MACHINE/System/Localization/Language", (string)keys[i]);
                    }
                    catch (System.Exception)
                    {
                    }
                });
            }
            catch (System.Exception)
            {
            }
            try
            {
                var r = AchievementsList.GetComponent<RectTransform>().sizeDelta;
                r.y = (Language.Achievements.Count / 2) * 80;
                AchievementsList.GetComponent<RectTransform>().sizeDelta = r;
                //AchievementsList.GetComponent<RectTransform>().rect = r;    
                for (int i = 0; i < Language.Achievements.Count / 2; i++)
                {
                    var t = Instantiate(AchieveTemplateButton, AchievementsList.transform).transform;
                    var lp = t.localPosition; lp.y = -40 - i * 80;
                    t.localPosition = lp; var text =
                     t.Find("Text").GetComponent<Text>();
                    try
                    {
                        if (GameInfo.Achievements[i] == false)
                        {
                            text.color = new Color(0.5f, 0.5f, 0.5f);
                        }
                    }
                    catch (System.Exception)
                    {
                        text.color = new Color(0.5f, 0.5f, 0.5f);
                    }
                    text.text = $"<size=24>{Language.Achievements[$"Achievement.{i + 1}.Title"]}</size>\r\n{Language.Achievements[$"Achievement.{i + 1}.Content"]}";
                }
                AchieveTemplateButton.gameObject.SetActive(false);
            }
            catch (System.Exception)
            {
            }

        }
        void SetLanguage()
        {
            {
                NewGameButton.transform.GetChild(0).GetComponent<Text>().text = Language.Language_UI["NewGame"];
            }
            {
                SettingsButton.transform.GetChild(0).GetComponent<Text>().text = Language.Language_UI["Settings"];
            }
            {
                ExitButton.transform.GetChild(0).GetComponent<Text>().text = Language.Language_UI["Exit"];
            }
            {
                TitleText1.GetComponent<Text>().text = Language.Language_UI["Title"];
            }
            {
                TitleText2.GetComponent<Text>().text = Language.Language_UI["Title"];
            }
            {
                AboutText.text = Language.Language_UI["AboutText"].Replace(":VERSION;", SCPVer.SCPVer.GetGameVersionString());
            }

        }
        void HideAllSettings()
        {
            GeneralPanel.SetActive(false);
            AboutPanel.SetActive(false);
        }
        void HideDialogPanel()
        {
            MainPage.SetActive(true);
            DialogPanel.SetActive(false);
        }
        void ShowDialogPanel()
        {
            MainPage.SetActive(false);
            AchievementsPanel.SetActive(false);
            DialogPanel.SetActive(true);
            NewGamePanel.SetActive(false);
            SettingsPanel.SetActive(false);
            LoadPanel.SetActive(false);
        }
        // Update is called once per frame
        void Update()
        {

        }
        void SerializeSettingsAndSave()
        {

            //Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Put("HKEY_LOCAL_MACHINE/HARDWARE/MOUSE/MouseRotationSpeed", "" + GameInfo.CurrentGameInfo.MouseRotationSpeed);
            Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Put("HKEY_LOCAL_MACHINE/System/Display/Width", "" + SystemSettings.W);
            Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Put("HKEY_LOCAL_MACHINE/System/Display/Height", "" + SystemSettings.H);
            Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Put("HKEY_LOCAL_MACHINE/System/Display/Fullscreen", "" + SystemSettings.isFullscreen);
            Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Put("HKEY_LOCAL_MACHINE/System/Graphics/QualityLevel", "" + SystemSettings.QualityLevel);
        }
        void SettingsDeserialization()
        {
            Debug.Log("Start Loading Settings...");
            Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Reload();
            try
            {
                //GameInfo.CurrentGameInfo.MouseRotationSpeed = float.Parse(Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Read("HKEY_LOCAL_MACHINE/HARDWARE/MOUSE/MouseRotationSpeed"));
            }
            catch (System.Exception)
            {

            }
            try
            {
                SystemSettings.isFullscreen = bool.Parse(Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Read("HKEY_LOCAL_MACHINE/System/Display/Fullscreen"));
            }
            catch (System.Exception)
            {
                //SystemSettings.isFullscreen = true;
                //Screen.fullScreen = true;
            }
            try
            {
                //QualitySettings.SetQualityLevel()
                SystemSettings.QualityLevel = int.Parse(Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Read("HKEY_LOCAL_MACHINE/System/Graphics/QualityLevel"));
            }
            catch (System.Exception)
            {
                SystemSettings.QualityLevel = 5;
                QualitySettings.SetQualityLevel(5);
            }
            try
            {
                SystemSettings.W = int.Parse(Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Read("HKEY_LOCAL_MACHINE/System/Display/Width"));
                SystemSettings.H = int.Parse(Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Read("HKEY_LOCAL_MACHINE/System/Display/Height"));
                Screen.SetResolution(SystemSettings.W, SystemSettings.H, SystemSettings.isFullscreen);
            }
            catch (System.Exception)
            {
                var re = Screen.resolutions;
                SystemSettings.W = Screen.resolutions[re.Length - 1].width;
                SystemSettings.H = Screen.resolutions[re.Length - 1].height;
                Screen.SetResolution(SystemSettings.W, SystemSettings.H, SystemSettings.isFullscreen);
            }
        }
    }
}
