using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Site13Kernel.EFI
{
    public class UEFI_RESET : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(delegate () {
                SceneManager.LoadScene(0);
            });
        }

    }

}