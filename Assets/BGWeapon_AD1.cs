using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Weapon.AD1
{
    public class BGWeapon_AD1 : MonoBehaviour
    {
        public AudioSource SFX00;
        public float FireDelta = 2f;
        // Start is called before the first frame update
        void Start()
        {
            time = Random.Range(0.0f, FireDelta);
            GetComponent<Animator>().SetTrigger("Fire");
        }
        float time;
        // Update is called once per frame
        void Update()
        {
            if (time < 0)
            {
                SFX00.Play();
                time = FireDelta;

            }
            else
            {
                time -= Time.deltaTime;
            }

        }
    }

}
