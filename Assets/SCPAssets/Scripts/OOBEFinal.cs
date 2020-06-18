using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Site13Kernel
{
    public class OOBEFinal : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            this.GetComponent<Button>().onClick.AddListener(delegate () {
                Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Put("HKEY_LOCAL_MACHINE/System/Setup/OOBE/OOBEVER", "1");
                SceneManager.LoadScene(0);
            });
        }

    }
}
