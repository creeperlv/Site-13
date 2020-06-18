using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{

    public class ToggleAudio : SCPStoryNodeBaseCode
    {
        public AudioSource AS;
        public override void StartStory()
        {
            isStarted = true;
            AS.Play();
        }
    }

}