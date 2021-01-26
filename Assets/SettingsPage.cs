using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Site13Kernel.UI
{

    public class SettingsPage : MonoBehaviour
    {
        public static SettingsPage CurrentSettingsPage;
        public List<TabSection> TabPages;
        public RawImage Background;
        public float BackgroundAnimateTime;
        public CanvasGroup CentralContent;
        public GameObject Dialog;
        float FrameHeight;
        float FrameWidth;
        float Speed;
        int TabIndex = 0;
        void Start()
        {
            CurrentSettingsPage = this;
            Dialog.SetActive(false);
            {
                var rt = CentralContent.gameObject.transform as RectTransform;
                FrameHeight = rt.rect.height;
                FrameWidth = rt.rect.width;
            }
            {
                var rt = Background.rectTransform;
                Debug.Log(Background.rectTransform.position);
                var sd = rt.sizeDelta;
                sd.y = FrameHeight * 2;
                rt.sizeDelta = sd;
                var rect = Background.uvRect;
                rect.height = (FrameHeight * 4) / 960;
                Background.uvRect = rect;
            }
            {

                var rt = Background.rectTransform;
                rt.localPosition = new Vector3(0, -FrameWidth / 2, 0);
            }
            {
                Speed = (FrameHeight * 1.75f) / BackgroundAnimateTime;
            }
            for (int i = 0; i < TabPages.Count; i++)
            {
                var item = TabPages[i];
                var index = i;
                item.button.onClick.AddListener(delegate ()
                {
                    ResetTabs();
                    item.button.Select();
                    TabIndex = index;
                    item.BGImg.gameObject.SetActive(true);
                    var t = item.button.gameObject.transform.Find("Text").GetComponent<Text>();
                    t.color = new Color(1, 1, 1);
                    item.TargetPage.SetActive(true);
                });
            }
            //foreach (var item in TabPages)
            //{
            //    item.button.onClick.AddListener(delegate () {
            //        ResetTabs();

            //        item.BGImg.gameObject.SetActive(true);
            //        var t = item.button.gameObject.transform.Find("Text").GetComponent<Text>();
            //        t.color = new Color(1,1,1);
            //        item.TargetPage.SetActive(true);
            //    });
            //}
        }
        void ResetTabs()
        {
            foreach (var item in TabPages)
            {
                item.BGImg.gameObject.SetActive(false);
                var t = item.button.gameObject.transform.Find("Text").GetComponent<Text>();
                t.color = new Color(170f / 255f, 203f / 255f, 236f / 255f);
                item.TargetPage.SetActive(false);
            }
        }
        float TimeD = 0;
        private void FixedUpdate()
        {
            {
                //Background Image;
                {
                    var rt = Background.rectTransform;
                    rt.localPosition += new Vector3(0, Speed * Time.fixedUnscaledDeltaTime, 0);

                }
                TimeD += Time.fixedUnscaledDeltaTime;
                if (TimeD >= BackgroundAnimateTime)
                {
                    TimeD = 0;
                    var rt = Background.rectTransform;
                    rt.localPosition = new Vector3(0, -FrameWidth / 2, 0);
                }
            }
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton5))
            {
                Debug.Log("RB");
                TabIndex++;
                Debug.Log("TI0:" + TabIndex);
                if (TabIndex >= TabPages.Count)
                {
                    TabIndex = 0;
                }
                Debug.Log("TI1:" + TabIndex);
                TabPages[TabIndex].button.onClick.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton4))
            {
                Debug.Log("LB");
                TabIndex--;
                if (TabIndex < 0)
                {
                    TabIndex = TabPages.Count - 1;
                }
                TabPages[TabIndex].button.onClick.Invoke();
            }
        }
    }
    [Serializable]
    public class TabSection
    {
        public Button button;
        public Image BGImg;
        public GameObject TargetPage;
    }

}