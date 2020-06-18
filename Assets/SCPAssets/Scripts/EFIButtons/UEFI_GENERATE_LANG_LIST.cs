using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UEFI_GENERATE_LANG_LIST : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate () {
            Site_13ToolLib.Globalization.Language language = new Site_13ToolLib.Globalization.Language();
            Site_13ToolLib.ToolSet.SetExperimentalFlag(true);
            language.GenerateLanguageList();
        });
    }
    
}
