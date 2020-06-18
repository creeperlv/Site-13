using Site_13ToolLib.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Site13Kernel
{
    public class AchievementShower : MonoBehaviour
    {
        Text text = null;
        public float Speed = 150f;
        // Start is called before the first frame update
        void Start()
        {

            text = transform.Find("Text").GetComponent<Text>();
            var rt = this.GetComponent<RectTransform>();
            GameInfo.CurrentGame.achievement = this;
            {
                var ap = rt.anchoredPosition;
                ap.y = 85;
                rt.anchoredPosition = ap;
            }
        }

        float DurationTime = 2.0f;
        float TimePassed = 0.0f;
        public void ShowAchievement(int id, float t = 3)
        {
            try
            {
                text.text = $"<size=24>{Language.Achievements[$"Achievement.{id}.Title"]}</size>\r\n{Language.Achievements[$"Achievement.{id}.Content"]}";
            }
            catch (System.Exception)
            {
                text.text = $"<size=24>Achievement: {id}</size>\r\nPlaceholder: Achievement-{id}";
            }
            TimePassed = 0.0f;
            DurationTime = t;
            if (isRunning == true)
            {

            }
            else StartCoroutine(Show());
        }
        Vector2 speed = Vector2.down*2;
        Vector2 speed2 = Vector2.right*4;
        bool isRunning = false;
        IEnumerator Show()
        {
            isRunning = true;
            //float t1 = 0;
            var rt = this.GetComponent<RectTransform>();
            for (; rt.anchoredPosition.y > -85;)
            {
                var rt2 = this.GetComponent<RectTransform>();
                rt2.Translate(speed * Speed * Time.deltaTime);
                yield return null;
            }
            {
                var ap = rt.anchoredPosition;
                ap.y = -85;
                rt.anchoredPosition = ap;
            }
            while (TimePassed <= DurationTime)
            {
                TimePassed += Time.deltaTime;
                yield return null;
            }
            //t1 = 0;
            for (; rt.anchoredPosition.x < 225;)
            {
                rt.Translate(speed2 * Speed * Time.deltaTime);
                yield return null;
            }
            {
                var ap = rt.anchoredPosition;
                ap.x = 225;
                rt.anchoredPosition = ap;
            }
            {
                var ap = rt.anchoredPosition;
                ap.y = 85;
                ap.x = -225;
                rt.anchoredPosition = ap;
            }
            isRunning = false;
            yield return null;
        }
        IEnumerator Hide() { return null; }
    }

}