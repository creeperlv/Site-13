using Site13Kernel.GameLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Site13Kernel.UI
{
    public class MainMenuV3 : ControlledBehavior
    {
        public List<ButtonedPage> pages=new List<ButtonedPage>();
        public override void Init()
        {
            if (!GameInfo.MainUIBGM.isPlaying)
                GameInfo.MainUIBGM.Play();
            foreach (var item in pages)
            {
                foreach (var btn in item.Buttons)
                {
                    btn.onClick.AddListener(() =>
                    {
                        HideAllPages();
                        item.Show();
                    });
                }
            }
        }
        void HideAllPages()
        {
            foreach (var item in pages)
            {
                item.Hide();
            }
        }
    }
    [Serializable]
    public class ButtonedPage
    {
        public List<Button> Buttons;
        public GameObject Page;
        public void Hide()
        {
            Page.SetActive(false);
        }
        public void Show()
        {
            Page.SetActive(true);
        }
    }

}