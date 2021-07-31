using Site13Kernel.GameLogic;
using Site13Kernel.UI.Customed;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Site13Kernel.UI
{
    public class MainMenuV3 : ControlledBehavior
    {
        public Transform CampaignHolder;
        public GameObject CampaignButton;
        public List<ButtonedPage> pages=new List<ButtonedPage>();
        public override void Init()
        {
            {
                for (int i = CampaignHolder.childCount - 1; i >= 0; i--)
                {
                    Destroy(CampaignHolder.GetChild(i).gameObject);
                }
                CampaignButtonGroup group=new CampaignButtonGroup();
                if (GameInfo.CurrentGameDef != null)
                    if (GameInfo.CurrentGameDef.MissionCollections != null)
                        if (GameInfo.CurrentGameDef.MissionCollections.Count > 0)
                            foreach (var item in GameInfo.CurrentGameDef.MissionCollections[0].MissionDefinitions)
                            {
                                var b=Instantiate(CampaignButton, CampaignHolder);
                                var cb=b.GetComponent<CampaignButton>();
                                cb.Init(group, item);
                            }
            }
            if (GameInfo.MainUIBGM != null)
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