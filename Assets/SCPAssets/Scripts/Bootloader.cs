using Site13ExternalKernel.Difficulty;
using Site13Kernel.EFI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Site13Kernel
{
    public class Bootloader : MonoBehaviour
    {
        public TextAsset buildIndo;
        public List<Image> Logos;
        public List<float> DurationTimes;
        public List<GameObject> GameObjectsThatHoldsEFI;
        void Start()
        {
            try
            {
                var c=buildIndo.text;
                StringReader stringReader = new StringReader(c);
                string s;
                while ((s= stringReader.ReadLine())!=null)
                {
                    if (s.StartsWith("Platform:"))
                    {
                        EnvironmentInfo.BuildPlatform = s.Substring("Platform:".Length);
                    }
                }

            }
            catch (System.Exception)
            {
            }
            try
            {
                SystemSettings.isFullscreen = bool.Parse(Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Read("HKEY_LOCAL_MACHINE/System/Display/Fullscreen"));
                Screen.fullScreen = SystemSettings.isFullscreen;
            }
            catch (System.Exception)
            {
                SystemSettings.isFullscreen = true;
            }

            try
            {
                SystemSettings.W = int.Parse(Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Read("HKEY_LOCAL_MACHINE/System/Display/Width"));
                SystemSettings.H = int.Parse(Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Read("HKEY_LOCAL_MACHINE/System/Display/Height"));
                Screen.SetResolution(SystemSettings.W, SystemSettings.H, SystemSettings.isFullscreen);
            }
            catch (System.Exception)
            {
                var re = Screen.resolutions;
                SystemSettings.W = Screen.resolutions[re.Length - 1].width;
                SystemSettings.H = Screen.resolutions[re.Length - 1].height;
                Screen.SetResolution(SystemSettings.W, SystemSettings.H, SystemSettings.isFullscreen);
            }
            if (!File.Exists("./Difficulties/Level2.diff"))
            {
                DifficultyDefinition.InitStandardDefinitions();
            }
            try
            {
                {
                    string currentDef = (Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Read("HKEY_LOCAL_MACHINE/System/Difficulty/CurrentDefinition"));
                    DifficultyManager.LoadDefinition(currentDef);
                }
            }
            catch (System.Exception)
            {
                DifficultyManager.LoadDefinition();
            }
            //Check OOBEVER
            try
            {
                int currentoobever = int.Parse(Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Read("HKEY_LOCAL_MACHINE/System/Setup/OOBE/OOBEVER"));
                if (currentoobever < 1)
                {
                    SceneManager.LoadScene(4);
                }
            }
            catch (System.Exception)
            {
                SceneManager.LoadScene(4);
            }
            //SceneManager.LoadScene(4);
            StartCoroutine(LogoSequence());
            foreach (var item in GameObjectsThatHoldsEFI)
            {
                item.GetComponent<EFIBase>().Run();
            }
        }

        IEnumerator LogoSequence()
        {
            for (int i = 0; i < Logos.Count; i++)
            {
                var tmp = Logos[i];
                var singleTime = DurationTimes[i] / 2.0f;
                var c = tmp.color;
                for (float ii = 0; ii < singleTime;)
                {
                    c.a = ii / singleTime;
                    //tmp.color = c;
                    ii += Time.deltaTime;
                    yield return null;
                }
                {

                    c.a = 1;
                    tmp.color = c;
                }
                for (float ii = 0; ii < singleTime;)
                {
                    c.a = 1 - (ii / singleTime);
                    //tmp.color = c;
                    ii += Time.deltaTime;
                    yield return null;
                }
                {

                    //c.a = 0;
                    //tmp.color = c;
                }
            }
            bool Skip = false;
            string GPU_NAME = SystemInfo.graphicsDeviceName.ToUpper();
            if(GPU_NAME.StartsWith("NVIDIA"))
            {
                GPU_NAME = GPU_NAME.Substring("NVIDIA".Length).Trim();
                if(GPU_NAME.StartsWith("GT "))
                {
                    SceneManager.LoadScene(13);
                    Skip = true;
                    yield break;
                }else
                if(GPU_NAME.StartsWith("GTX "))
                {
                    if(int.Parse(GPU_NAME.Substring("GTX ".Length).Trim().Substring(0, GPU_NAME.Substring("GTX ".Length).Trim().IndexOf(' ')))<750)
                    SceneManager.LoadScene(13);
                    Skip = true;
                    yield break;
                }
            }
            else if(GPU_NAME.StartsWith("INTEL"))
            {
                SceneManager.LoadScene(13);
                Skip = true;
                yield break;
            }
            if ((double)SystemInfo.systemMemorySize / 1024.0 < 7.5)
            {
                SceneManager.LoadScene(13);
                Skip = true;
                yield break;
            }
            if(Skip==false)
            SceneManager.LoadScene(1);
            yield break;
        }
        // Update is called once per frame
        void Update()
        {
            try
            {
                if (Input.GetKey(KeyCode.Delete))
                {
                    SceneManager.LoadScene(3);
                }
            }
            catch (System.Exception)
            {
            }
        }
    }
}
