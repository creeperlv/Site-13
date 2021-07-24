using Site_13ToolLib.Globalization;
using Site13Kernel.GameLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Site13Kernel.UI.Localization
{
    public class Localizer : ControlledBehavior
    {
        bool isInited=false;
        public bool SelfInit=false;
        public List<LocalizedText> localizedTexts= new List<LocalizedText>();
        public List<LocalizedTMPText> localizedTMPTexts = new List<LocalizedTMPText>();
        void Start()
        {

            if (SelfInit)
            {
                Init();
            }
        }
        public override void Init()
        {
            if (isInited)
            {
                isInited= true;
            }
        }
        public void ApplyLanguage()
        {
            foreach (var item in localizedTexts)
            {
                item.TargetText.text = Language.GetString(item.LanguageCategory, item.ID, item.Fallback);
            }
            foreach (var item in localizedTMPTexts)
            {
                item.TargetText.text = Language.GetString(item.LanguageCategory, item.ID, item.Fallback);
            }
        }
    }
    [Serializable]
    public class LocalizedText
    {
        public Text TargetText;
        public LanguageCategory LanguageCategory;
        public string ID;
        public string Fallback;
    }
    [Serializable]
    public class LocalizedTMPText
    {
        public TMP_Text TargetText;
        public LanguageCategory LanguageCategory;
        public string ID;
        public string Fallback;
    }

}