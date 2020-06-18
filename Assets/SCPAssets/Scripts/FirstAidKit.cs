using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Site13Kernel
{

    public class FirstAidKit : MonoBehaviour
    {
        public Text Indicator;
        // Start is called before the first frame update
        void Start()
        {

        }
        float timed = 0.5f;
        // Update is called once per frame
        void Update()
        {
            Indicator.text = "Possessing:" + GameInfo.CurrentGame.PossessingFAK;
            if (Input.GetButton("Fire1"))
            {
                if (GameInfo.CurrentGame.isPaused == false)
                {

                    if (timed >= 0.5f)
                    {

                        if (GameInfo.CurrentGame.PossessingFAK > 0)
                        {
                            if (GameInfo.CurrentGame.PlayerHealth.CurrentHealth == GameInfo.CurrentGame.PlayerHealth.MaxHealth)
                            {
                                //Stop wasting First Aid Kit.
                            }
                            else
                            {
                                GameInfo.CurrentGame.PlayerHealth.ChangeHealth(50);
                                GameInfo.CurrentGame.PossessingFAK--;
                            }
                        }
                    }
                    else if (timed <= 0f)
                    {
                        timed = 0.6f;
                    }
                    timed -= Time.deltaTime;
                }
            }
            else
            {
                timed = 0.5f;
            }
        }
    }

}