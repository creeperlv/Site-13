using Site_13ToolLib.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Site13Kernel
{
    public class SecurityScreen : MonoBehaviour
    {
        public Text CentralTip;
        public string Flag;
        public string TargetFlag;
        public string Before;
        public string After;
        void Start()
        {
        }
        string ori = "";
        // Update is called once per frame
        void Update()
        {
            try
            {

                if (GameInfo.CurrentGame.FlagsGroup[Flag] == TargetFlag)
                {
                    if (ori == Language.Language_Plot[After])
                    {

                    }
                    else
                    {
                        ori = Language.Language_Plot[After];
                        CentralTip.text = ori;
                    }
                }
            }
            catch (System.Exception)
            {
            }
        }
    }


}