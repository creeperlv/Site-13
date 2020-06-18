using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Stories
{

    public class InteractHumanTalking : SCPInteractive
    {
        public AudioClip clip;
        public AudioSource source;
        public override IEnumerator Move()
        {
            isOperating = true;
            source.clip = clip;
            source.Play();
            yield return new WaitForSeconds(clip.length);
            isOperating = false;
        }
    }

}