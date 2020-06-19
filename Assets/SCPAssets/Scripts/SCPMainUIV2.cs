using Site13Kernel.EFI;
using Site13Kernel.IO;
using Site_13ToolLib.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;
namespace Site13Kernel.UI
{

    public class SCPMainUIV2 : MonoBehaviour
    {

        [UnityEngine.Header("Main Menu V2", order = 0)]
        #region Languagilized Texts
        [UnityEngine.Header("Languagilized Texts", order = 1)]
        public Text TitleText;
        public Text NewGameText;
        public Text LoadGameText;
        public Text SaveNameText;
        public Text ArchievementButtonText;
        public Text ArchievementTitleText;
        public Text SettingsButtonText;
        public Text ExitText;
        public Text AboutText;
        public Text AboutTitle1Text;
        public Text AboutTitle2Text;
        public Text QualityLevelText;
        public Text WarningText;
        public Text FullScreenText;
        #endregion
        #region Languagilized Texts
        [UnityEngine.Header("Languagilized Texts", order = 1)]
        public EFIBase LangEFI;
        #endregion

        #region User Controls
        [UnityEngine.Header("User Controls", order = 1)]
        public Button NewGameOK;
        public InputField SaveName;
        public Button LoadGameTemplate;
        public Button ExitButton;
        public Dropdown Resolutions;
        public Dropdown Languages;
        public Dropdown Qualities;
        public InputField MouseRotationSpeed;
        public GameObject SavesPresenter;
        public GameObject AchievementsPresenter;
        public GameObject AchievementButton;
        public Slider FullscreenSwitch;
        public Animator CentralAnimator;
        #endregion

        void SetLanguage()
        {
            {
                WarningText.text = Language.Language_UI["WarningText"];
            }
            {
                NewGameText.text = Language.Language_UI["NewGame"];
            }
            {
                LoadGameText.text = Language.Language_UI["Load"];
            }
            {
                SaveNameText.text = Language.Language_UI["SaveName"];
            }
            {
                SettingsButtonText.GetComponent<Text>().text = Language.Language_UI["Settings"];
            }
            {
                ExitText.text = Language.Language_UI["Exit"];
            }
            {
                TitleText.text = Language.Language_UI["Title"];
            }
            {
                AboutText.text = Language.Language_UI["AboutText"].Replace(":VERSION;", SCPVer.SCPVer.GetGameVersionString());
            }
            {
                FullScreenText.text = Language.Language_UI["Fullscreen"];
            }
            {
                AboutTitle1Text.text = Language.Language_UI["About"];
                AboutTitle2Text.text = Language.Language_UI["About"];
            }
            {
                QualityLevelText.text = Language.Language_UI["QualityLevel"];
            }

        }
        IEnumerator ExitProgress()
        {
            yield return new WaitForSeconds(1.55f);
            Application.Quit();
        }
        void Start()
        {
            ExitButton.onClick.AddListener(delegate ()
            {
                CentralAnimator.SetTrigger("Exit");
                StartCoroutine(ExitProgress());
            });
            LangEFI.Run();
            SetLanguage();
            SettingsDeserialization();
            NewGameOK.onClick.AddListener(delegate ()
            {
                if (SaveName.text == "")
                {

                }
                else
                {
                    GameInfo.CurrentGame = new GameInfo(SaveName.text);
                    GameInfo.CurrentGame.NextScene = 19;
                    SceneManager.LoadScene(2);
                }
            });
            //////////////
            //Language D//
            //////////////
            try
            {

                Dictionary<string, string> list = new Dictionary<string, string>();
                ArrayList keys = new ArrayList();
                ArrayList values = new ArrayList();

                {

                    list = Site_13ToolLib.Data.GOCDataV1.Load("./ExtraResources/Languages/LanguageList.goc-data", false);
                    {
                        List<Dropdown.OptionData> lst = new List<Dropdown.OptionData>();
                        Languages.ClearOptions();
                        foreach (var item in list)
                        {
                            keys.Add(item.Key);
                            values.Add(item.Value);
                            Dropdown.OptionData optionData = new Dropdown.OptionData(item.Value);
                            lst.Add(optionData);
                        }
                        Languages.AddOptions(lst);
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    if (list.ElementAt(i).Key == Language.GetLanguageInUse())
                    {
                        Languages.value = i;
                        break;
                    }
                }

                Languages.onValueChanged.AddListener(delegate (int i)
                {
                    try
                    {
                        Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Put("HKEY_LOCAL_MACHINE/System/Localization/Language", (string)keys[i]);
                        LangEFI.Run();
                        SetLanguage();
                    }
                    catch (System.Exception)
                    {
                    }
                });
            }
            catch (System.Exception)
            {
            }
            //////////////
            //FullScreen//
            //////////////
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
            //////////////
            //Resolution//
            //////////////
            try
            {

                var dp = Resolutions;
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
            /////////////////////////////////
            //Achievements Related Settigns//
            /////////////////////////////////

            try
            {
                var r = AchievementsPresenter.GetComponent<RectTransform>().sizeDelta;
                r.y = (Language.Achievements.Count / 2) * 80;
                AchievementsPresenter.GetComponent<RectTransform>().sizeDelta = r;
                //AchievementsList.GetComponent<RectTransform>().rect = r;    
                for (int i = 0; i < Language.Achievements.Count / 2; i++)
                {
                    var t = Instantiate(AchievementButton, AchievementsPresenter.transform).transform;
                    t.gameObject.SetActive(true);
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
                //AchievementsPresenter.gameObject.SetActive(false);
            }
            catch (System.Exception)
            {
            }

            //////////////////////////////////////
            //Mouse Rotation Speed Configuration//
            //////////////////////////////////////
            MouseRotationSpeed.onEndEdit.AddListener(delegate (string s)
            {
                try
                {
                    GameInfo.TargetMouseSen = float.Parse(s);
                    GameInfo.CurrentMouseSen = float.Parse(s);
                    SerializeSettingsAndSave();
                }
                catch (System.Exception)
                {
                }
            });
            ////////////////////
            //Quality Switcher//
            ////////////////////
            try
            {

                var dp = Qualities;
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
            ///////////////
            //Save Loader//
            ///////////////
            try
            {

                var saves = SaveControlProtocol.GetList();
                var orilp = LoadGameTemplate.transform.localPosition;
                for (int i = 0; i < saves.Count; i++)
                {
                    var newBtn = Instantiate(LoadGameTemplate, SavesPresenter.transform);
                    newBtn.gameObject.SetActive(true);
                    orilp.y -= 40;
                    newBtn.transform.Find("Text").GetComponent<Text>().text = saves[i];
                    newBtn.transform.localPosition = orilp;
                }
                var s = SavesPresenter.GetComponent<RectTransform>().sizeDelta;
                    s.y = saves.Count * 40; SavesPresenter.GetComponent<RectTransform>().sizeDelta=s;
            }
            catch (System.Exception)
            {
            }
            ///////////////
            //Save Loader//
            ///////////////
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
        }

        void SerializeSettingsAndSave()
        {

            //Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Put("HKEY_LOCAL_MACHINE/HARDWARE/MOUSE/MouseRotationSpeed", "" + GameInfo.CurrentGameInfo.MouseRotationSpeed);
            Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Put("HKEY_LOCAL_MACHINE/System/Display/Width", "" + SystemSettings.W);
            Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Put("HKEY_LOCAL_MACHINE/System/Display/Height", "" + SystemSettings.H);
            Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Put("HKEY_LOCAL_MACHINE/System/Display/Fullscreen", "" + SystemSettings.isFullscreen);
            Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Put("HKEY_LOCAL_MACHINE/System/Graphics/QualityLevel", "" + SystemSettings.QualityLevel);
            Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Put("HKEY_LOCAL_MACHINE/HARDWARE/MOUSE/MouseRotationSpeed", "" + GameInfo.TargetMouseSen);
        }
        void SettingsDeserialization()
        {
            Debug.Log("Start Loading Settings...");
            Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Reload();
            try
            {
                GameInfo.TargetMouseSen = float.Parse(Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Read("HKEY_LOCAL_MACHINE/HARDWARE/MOUSE/MouseRotationSpeed"));
                MouseRotationSpeed.text = "" + GameInfo.TargetMouseSen;
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