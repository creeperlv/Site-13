using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Site13Kernel
{
    public class BloodOverlay : MonoBehaviour
    {
        RawImage img;
        // Start is called before the first frame update
        void Start()
        {
            img = GetComponent<RawImage>();
        }

        // Update is called once per frame
        void Update()
        {
            try
            {
                var c = img.color;
                c.a = 1f - (GameInfo.CurrentGame.PlayerHealth.CurrentHealth / GameInfo.CurrentGame.PlayerHealth.MaxHealth);
                img.color = c;
            }
            catch (System.Exception)
            {
            }
        }
    }

}