using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Stories
{
    /// <summary>
    /// Simulates walk sound in SCPFirstController in little cost, also, can be easily controlled by Animator. :D
    /// </summary>
    public class SCPWalkSound : MonoBehaviour
    {
        [Header("Interval for each foot step sound")]
        public float Speed;
        public List<AudioClip> clips;
        public float Volume;
        public AudioSource FootStepSpeaker;
        int FootStepIndex = 0;
        float Timer = 0.0f;
        void Update()
        {
            if (Speed > 0)
            {
                Timer += Time.deltaTime;
                if (Timer > Speed)
                {
                    Timer = 0;
                    FootStepSpeaker.clip = clips[FootStepIndex];
                    FootStepSpeaker.Play();
                    FootStepIndex++;
                    if (FootStepIndex >= clips.Count)
                    {
                        FootStepIndex = 0;
                    }
                }
            }
        }
    }

}