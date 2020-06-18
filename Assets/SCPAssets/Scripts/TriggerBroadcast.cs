using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public class TriggerBroadcast : SCPStoryNodeBaseCode
    {
        public AudioSource TargetBroadcast;
        
        // Start is called before the first frame update
        void Start()
        {

        }
        public override void StartStory()
        {
            isStarted = true;
            TargetBroadcast.Play();
        }
    }

}