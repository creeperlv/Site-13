using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
namespace Site13Kernel.UI.Settings
{

    public class KeyBindItem : MonoBehaviour
    {
        public string InputName;
        public bool isControlNegative;
        public bool isAxis;
        public BindType BindType;
        public Button Btn;
        public Slider slider;
        public InputField input;
        // Start is called before the first frame update
        void Start()
        {
            try
            {
                Btn.onClick.AddListener(delegate ()
                {

                });
            }
            catch (System.Exception)
            {
            }
            if (BindType == BindType.Slider)
            {
                slider.onValueChanged.AddListener(delegate (float v)
                {
                    if (input.text != v + "")
                    {
                        input.text = v + "";
                    }
                });
                input.onEndEdit.AddListener(delegate (string t)
                {
                    if (t != slider.value + "")
                    {
                        try
                        {
                            slider.value = float.Parse(t);
                        }
                        catch (System.Exception)
                        {
                        }
                    }
                });
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
    public enum BindType
    {
        SingleButton, MultipleButton, Slider
    }
}
