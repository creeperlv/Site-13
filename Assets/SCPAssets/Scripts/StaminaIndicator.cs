using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Site13Kernel
{

    public class StaminaIndicator : MonoBehaviour
    {
        Slider slider;
        // Start is called before the first frame update
        void Start()
        {
            slider = GetComponent<Slider>();
        }
        float CurrentStamina;
        // Update is called once per frame
        void Update()
        {
            try
            {

                if (CurrentStamina == GameInfo.CurrentGame.FirstPerson.CurrentStamina / (GameInfo.CurrentGame.FirstPerson.MaxStamina ))
                {

                }
                else
                {
                    CurrentStamina = GameInfo.CurrentGame.FirstPerson.CurrentStamina / (GameInfo.CurrentGame.FirstPerson.MaxStamina );
                    slider.value = CurrentStamina;
                }
            }
            catch (System.Exception)
            {
            }
        }
    }

}