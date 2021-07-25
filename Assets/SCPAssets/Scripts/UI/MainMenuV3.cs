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
            foreach (var item in pages)
            {
                item.Button.onClick.AddListener(() => {
                    HideAllPages();
                    item.Show();
                });
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
        public Button Button;
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