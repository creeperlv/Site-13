using Site13Kernel;
using Site13Kernel.Plugin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCPDebugInfo : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    private int Frames = 0;
    private float FPS = 0.0f;
    public float fpsMeasuringDelta = 2.0f;
    private float timePassed;
    void Start()
    {
        //timePassed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        text = GetComponent<Text>();
        //Frames += 1;
        timePassed += Time.deltaTime;

        if (timePassed > fpsMeasuringDelta)
        {
            //FPS = Frames / timePassed;

            FPS = 1 / Time.deltaTime;
            text.text = $"<size=18>What Happened to Site-13?</size>\r\nVery Early Preview\r\nUnity:{EnvironmentInfo.UnityVersion}\r\nBuilt on: <color=#2288EE>{EnvironmentInfo.BuildPlatform}</color>\r\nFPS(Frames per second):";
            if (FPS > 60)
            {
                text.text += $"<color=green>{FPS}</color>";
            }
            else if (FPS > 30)
            {
                text.text += $"<color=orange>{FPS}</color>";
            }
            else if (FPS >= 0)
            {
                text.text += $"<color=red>{FPS}</color>";
            }
            else
            {
                text.text += $"<color=white>{FPS}</color>";
            }
            text.text += $"\r\nTarget Frame Rate:{Application.targetFrameRate}\r\nPlatform:{Application.platform}\r\nPlugins:{PluginPool.PluginCount}\r\nGPU:{SystemInfo.graphicsDeviceName}\r\nDevice:{SystemInfo.deviceModel}";

            timePassed = 0.0f;
            Frames = 0;
        }
    }
}
