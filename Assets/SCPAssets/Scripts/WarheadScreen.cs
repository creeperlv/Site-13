using Site13Kernel;
using Site_13ToolLib.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarheadScreen : MonoBehaviour
{
    Text CentralTip;
    // Start is called before the first frame update
    void Start()
    {
        CentralTip = transform.Find("CentralTip").GetComponent<Text>();
    }
    string ori = "";
    // Update is called once per frame
    void Update()
    {
        try
        {

            if (GameInfo.CurrentGame.FlagsGroup["Warhead"] == "Disabled")
            {
                if (ori == Language.Language_Plot["Warhead.Disabled"])
                {

                }
                else
                {
                    ori = Language.Language_Plot["Warhead.Disabled"];
                    CentralTip.text = Language.Language_Plot["Warhead.Disabled"];
                }
            }   
        }
        catch (System.Exception)
        {
        }
    }
}
