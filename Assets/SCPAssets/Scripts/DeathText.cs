using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Site13Kernel
{
    public class DeathText : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }
        private void Awake()
        {
            GetComponent<Text>().text = GameInfo.CurrentGame.DeathText;
        }
    }
}
