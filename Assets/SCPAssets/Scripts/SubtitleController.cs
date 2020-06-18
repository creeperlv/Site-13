using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Site13Kernel
{
    public class SubtitleController : MonoBehaviour
    {
        Text text = null;

        // Use this for initialization
        void Start()
        {
            GameInfo.CurrentGame.PublicSubtitle = this;
            text = transform.GetComponent<Text>();
            text.enabled = false;
        }
        public void ShowSubtitle(string subtitle, float time = 3.0f)
        {
            text.text = subtitle;
            TimePassed = 0.0f;
            DurationTime = time;
            if (text.enabled != true)
            {

                text.enabled = true;
                StartCoroutine(RealShowSubtitle());
            }
        }
        float DurationTime = 3.0f;
        float TimePassed = 0.0f;

        IEnumerator RealShowSubtitle()
        {
            while (TimePassed <= DurationTime)
            {

                if (TimePassed < .25f)
                {
                    text.color = new Color(1, 1, 1, TimePassed * 4);
                }
                if (TimePassed > DurationTime - .25f)
                {
                    text.color = new Color(1, 1, 1, (DurationTime - TimePassed) * 4);
                }
                TimePassed += Time.deltaTime;
                yield return null;
            }
            text.enabled = false;
        }
        // Update is called once per frame
        //void Update () {

        //}
    }
}
