using Site13Kernel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Stories
{

    public class SCPAmationControllerV01 : MonoBehaviour
    {
        public float TotalPreDelay = 0;
        [System.Serializable]
        public class ObjectToToggle
        {
            public GameObject Object;
            public float Time;
            [HideInInspector]
            public bool Toogled;
        }
        public List<ObjectToToggle> ToActive;
        public List<ObjectToToggle> ToDeactive;
        public List<Subtitle> subtitles;
        // Start is called before the first frame update
        void Start()
        {
            _time -= TotalPreDelay;
            StartCoroutine(SubtitleStory());
        }
        IEnumerator SubtitleStory()
        {
            yield return new WaitForSeconds(TotalPreDelay);
            yield return new WaitForSeconds(0.5f);
            foreach (var item in subtitles)
            {
                try
                {

                    GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle(item.subtitleLangAlias == "" ? item.subtitle : Site_13ToolLib.Globalization.Language.Language_Plot[item.subtitleLangAlias], item.Length);

                }
                catch (System.Exception)
                {
                }
                yield return new WaitForSeconds(item.DelayTime);
            }
        }
        bool isAllDone = false;

        float _time = .0f;
        // Update is called once per frame
        void Update()
        {
            if (isAllDone == false)
            {
                _time += Time.deltaTime;
                isAllDone = true;
                foreach (var item in ToActive)
                {
                    if (_time >= item.Time)
                    {
                        if (item.Toogled == false)
                        {
                            item.Object.gameObject.SetActive(true);
                            item.Toogled = true;
                            isAllDone = false;
                        }
                    }
                    else
                    {
                        isAllDone = false;
                    }
                }
                foreach (var item in ToActive)
                {
                    if (_time >= item.Time)
                    {
                        if (item.Toogled == false)
                        {
                            item.Object.gameObject.SetActive(false);
                            item.Toogled = true;
                            isAllDone = false;
                        }
                    }
                    else
                        isAllDone = false;
                }
                if (isAllDone == true)
                {

                }
            }

        }
    }
    [System.Serializable]
    public class Subtitle
    {
        public string subtitle;
        public string subtitleLangAlias;
        public float Length;
        public float DelayTime;
        [HideInInspector]
        public bool IsShown;
    }
}
