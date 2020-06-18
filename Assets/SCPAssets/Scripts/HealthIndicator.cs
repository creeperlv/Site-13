using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Site13Kernel
{

    public class HealthIndicator : MonoBehaviour
    {
        Slider slider;
        // Start is called before the first frame update
        void Start()
        {

            slider = GetComponent<Slider>();
        }

        // Update is called once per frame
        void Update()
        {
            //slider.value = GameInfo.CurrentGame.PlayerHealth.CurrentHealth / GameInfo.CurrentGame.PlayerHealth.MaxHealth;
            var Health = transform.parent.parent.GetComponent<SCPEntity>();
            slider.value = Health.CurrentHealth / Health.MaxHealth;
        }
    }

}