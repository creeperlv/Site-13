using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public class SCPLargeLiftControl02 : SCPInteractive
    {
        public GameObject Platform;
        public AnimationClip ShowUp;
        public AudioClip MovingSFX;
        public float LastTime = 4.5f;
        public override IEnumerator Move()
        {
            isOperating = true;

            var ani = Platform.GetComponent<Animation>();
            var AS = Platform.transform.Find("Audio Source").GetComponent<AudioSource>();
            AS.clip = MovingSFX;
            AS.Play();
            //ani.clip = ShowUp;
            ani.AddClip(ShowUp, "GoUp");
            ani.Play("GoUp");
            yield return new WaitForSeconds(LastTime);
            ani.Stop();
            yield break;
        }
    }
}