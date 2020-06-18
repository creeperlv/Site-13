using Site_13ToolLib.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Site13Kernel.Globalization
{

    public class SCPLangScreen : MonoBehaviour
    {
        public List<string> TextHolders;
        public List<string> Alias;
        public bool WillShowErrorText = false;
        void Start()
        {
            foreach (var item in TextHolders)
            {
                try
                {

                    var t = transform.Find(item).GetComponent<Text>();
                    int i = TextHolders.IndexOf(item);
                    try
                    {
                        t.text = Language.Language_Plot[Alias[i]];
                    }
                    catch (System.Exception)
                    {
                        if (WillShowErrorText == true)
                        {
                            t.text = $"<color=red>Error: Code 404 - Cannot find <color=#2080E0>{Alias[i]}</color></color>";
                        }
                    }
                }
                catch (System.Exception)
                {
                }
            }
        }
        public string GetTextAlia(string TextName)
        {
            for (int i = 0; i < TextHolders.Count; i++)
            {
                if (TextHolders[i] == TextName)
                {
                    return Alias[i];
                }
            }
            return "";
        }
        public void SetText(string txtID, string LangAlias)
        {

            try
            {
                var t = transform.Find(txtID).GetComponent<Text>();
                for (int i = 0; i < TextHolders.Count; i++)
                {
                    if (TextHolders[i] == txtID)
                    {
                        Alias[i] = LangAlias;
                    }
                }
                if (LangAlias == "{Sepcial.Space}")
                {
                    t.text = "";
                    return;
                }
                else
                    t.text = Language.Language_Plot[LangAlias];
            }
            catch (System.Exception)
            {
                if (WillShowErrorText == true)
                {
                    try
                    {

                    }
                    catch (System.Exception)
                    {
                        var t = transform.Find(txtID).GetComponent<Text>();
                        t.text = $"<color=red>Error: Code 404 - Cannot find <color=#2080E0>{LangAlias}</color></color>";

                    }
                }
            }
        }

    }

}