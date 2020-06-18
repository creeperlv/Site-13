using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Site13Kernel
{

    public class SecondaryNotification : MonoBehaviour
    {
        Text text = null;
        // Start is called before the first frame update
        void Start()
        {
            GameInfo.CurrentGame.secondaryNotification = this;
            text = GetComponent<Text>();
        }

        public void ShowNotification(string msg)
        {
            text.text = msg;
            Debug.Log("SN:"+msg);
            GetComponent<Animator>().SetTrigger("Show");
        }
    }

}