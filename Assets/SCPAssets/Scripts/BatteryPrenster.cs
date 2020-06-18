using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Site13Kernel
{

    public class BatteryPrenster : MonoBehaviour
    {
        Text shower;
        // Start is called before the first frame update
        void Start()
        {
            shower = this.GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            if (shower.text != GameInfo.CurrentGame.Bats + "")
            {
                shower.text = "" + GameInfo.CurrentGame.Bats;
            }
        }
    }

}