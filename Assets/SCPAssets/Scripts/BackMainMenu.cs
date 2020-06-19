using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackMainMenu : MonoBehaviour
{
    public bool willShowMouse = false;
    // Start is called before the first frame update
    void Start()
    {
        if (willShowMouse)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        GetComponent<Button>().onClick.AddListener(delegate () {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 1;
            AudioListener.pause = false;
            SceneManager.LoadScene(1);
        });   
    }
}
