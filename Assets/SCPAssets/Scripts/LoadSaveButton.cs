using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Site13Kernel.UI
{

    public class LoadSaveButton : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var gameName = gameObject.transform.Find("Text").gameObject.GetComponent<Text>().text;
            GetComponent<Button>().onClick.AddListener(delegate {
                try
                {
                    GameInfo.CurrentGame = new GameInfo(gameName);
                    GameInfo.CurrentGame.saveControlProtocolMode = GameInfo.SaveControlProtocolMode.Load;
                    GameInfo.CurrentGame.LoadGI();
                    GameInfo.CurrentGame.NextScene = GameInfo.CurrentGame.PlayerScene;
                    SceneManager.LoadScene(2);
                }
                catch (System.Exception)
                {

                }
            });
        }
    }

}