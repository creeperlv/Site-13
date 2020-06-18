using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeviceWarn : MonoBehaviour
{
    public TMPro.TMP_Text TIP;
    public Button Exit;
    public Button Continue;
    // Start is called before the first frame update
    void Start()
    {
        Exit.onClick.AddListener(delegate () {
            Application.Quit();
        });
        Continue.onClick.AddListener(delegate () {
            SceneManager.LoadScene(1);
        });
        TIP.text = TIP.text.Replace("(GPU_MODEL)",SystemInfo.graphicsDeviceName).Replace("(MEM_SIZE)",(double)SystemInfo.systemMemorySize/1024.0+" GB");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
