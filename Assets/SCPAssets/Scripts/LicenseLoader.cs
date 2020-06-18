using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LicenseLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        var a = File.ReadAllText("./ExtraResources/LICENSE");
        this.GetComponent<Text>().text = a;
        var lp = GetComponent<RectTransform>().localPosition;
        lp.y = -this.GetComponent<RectTransform>().sizeDelta.y / 2;
        GetComponent<RectTransform>().localPosition = lp;
    }
}
