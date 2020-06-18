using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Site13Kernel
{
    public class LoadingUI : MonoBehaviour
    {
        AsyncOperation operation;

        public Text LoadingText;

        public Slider ProgressBar;

        // Start is called before the first frame update
        void Start()
        {

            try
            {
                GameInfo.CurrentGame.isCurrentArrived = false;
                var loadings = Load();
                int WhichOne = (new System.Random()).Next(loadings.Key.Count - 1);
                LoadingText.text = $"<size=46>{loadings.Key[WhichOne]}</size>\r\n{loadings.Value[WhichOne]}";
            }
            catch (System.Exception e)
            {
                LoadingText.text = $"<size=46>Ops!</size>\r\nError happens!\r\nError message:<color=red>{e.Message}</color>";
                Debug.Log(e.Message);
            }

            //ProgressIndicator=transform.Find("ProgressIndicator").GetComponent<Text>();
            StartCoroutine(Navigate());//玄学¯\_(ツ)_/¯
        }

        KeyValuePair<List<string>, List<string>> Load()
        {
            List<string> names = new List<string>();
            List<string> datas = new List<string>();
            Dictionary<string, string> data = new Dictionary<string, string>();
            var cont = File.ReadAllLines($"./ExtraResources/Languages/{Site_13ToolLib.Globalization.Language.LanguageCode}/Loadings.goc-data");
            var doc = XDocument.Parse(cont[0]);
            var e = doc.Root.Elements();
            foreach (var item in e)
            {
                var a = (string)item.Attribute("Name");
                var b = (string)item.Attribute("Data");
                names.Add(a);
                datas.Add(b.Replace(":R;", "\r\n"));
            }
            return new KeyValuePair<List<string>, List<string>>(names, datas);
        }
        IEnumerator Navigate()
        {
            GameInfo.CurrentGame.CurrentScene = SceneManager.GetSceneByBuildIndex(GameInfo.CurrentGame.NextScene).name;
            operation = SceneManager.LoadSceneAsync(GameInfo.CurrentGame.NextScene);
            operation.allowSceneActivation = false;
            yield return operation;
        }
        float CurrentProgress = 0;
        bool flag = false;//Stop progress from overflow.
                          // Update is called once per frame
        void Update()
        {

            if (CurrentProgress < 0.9)
            {
                CurrentProgress = operation.progress * 100;

            }
            else if (!flag)
            {
                CurrentProgress++;
                if (CurrentProgress > 100)
                {
                    CurrentProgress = 100;
                }
            }
            ProgressBar.value = CurrentProgress;
            //ProgressIndicator.text = CurrentProgress + "%";
            if (CurrentProgress >= 100)
            {
                flag = true;
                operation.allowSceneActivation = true;
            }
        }
    }
}
