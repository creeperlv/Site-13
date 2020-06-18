using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Site13Kernel.UI
{
    public class Notification : MonoBehaviour
    {
        Text text = null;
        public float Speed = 150f;
        // Start is called before the first frame update
        void Start()
        {
            text = transform.Find("Text").GetComponent<Text>();
            var rt = this.GetComponent<RectTransform>();
            GameInfo.CurrentGame.notification = this;
            {
                var ap = rt.anchoredPosition;
                ap.y = 40;
                rt.anchoredPosition = ap;
            }
        }
        float DurationTime = 3.0f;
        float TimePassed = 0.0f;
        public void ShowNotification(string msg, float t = 3)
        {

            text.text = msg;
            TimePassed = 0.0f;
            DurationTime = t;
            if (isRunning == true)
            {

            }
            else StartCoroutine(Show());
        }
        Vector2 speed = Vector2.down ;
        Vector2 speed2 = Vector2.up;
        bool isRunning = false;
        IEnumerator Show()
        {
            isRunning = true;
            //float t1 = 0;
            var rt = this.GetComponent<RectTransform>();
            for (; rt.anchoredPosition.y > -40;)
            {
                var rt2 = this.GetComponent<RectTransform>();
                rt2.Translate(speed*Speed * Time.deltaTime);
                yield return null;
            }
            {
                var ap = rt.anchoredPosition;
                ap.y = -40;
                rt.anchoredPosition = ap;
            }
            while (TimePassed <= DurationTime)
            {
                TimePassed += Time.deltaTime;
                yield return null;
            }
            //t1 = 0;
            for (; rt.anchoredPosition.y < 40;)
            {
                rt.Translate(speed2 * Speed * Time.deltaTime);
                yield return null;
            }
            {
                var ap = rt.anchoredPosition;
                ap.y = 40;
                rt.anchoredPosition = ap;
            }
            isRunning = false;
            yield return null;
        }
        IEnumerator Hide() { return null; }
    }

}
