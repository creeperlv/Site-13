using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Site13Kernel
{

    public class ReadingPanelExitButton : MonoBehaviour
    {
        SCPFirstController aio;
        // Start is called before the first frame update
        void Start()
        {
            aio = transform.parent.parent.parent.gameObject.GetComponent<SCPFirstController>();
            GetComponent<Button>().onClick.AddListener(delegate ()
            {
                aio.enabled = true;
                transform.parent.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            });
        }
    }
}