using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public class SCPRandomAnnounc : MonoBehaviour
    {
        public List<AudioClip> audioClips;
        public AudioSource audioSource;
        void Start()
        {
            StartCoroutine(RandomPlay());
        }
        
        IEnumerator RandomPlay()
        {
            yield return new WaitForSeconds(10);
            int sec = (new System.Random()).Next(30, 60);
            while (true)
            {
                sec = (new System.Random()).Next(30, 60);
                Debug.Log("StartWaiting:"+sec);
                yield return new WaitForSeconds(sec);
                int index = (new System.Random()).Next(audioClips.Count);
                Debug.Log("Play:" + index );
                audioSource.clip = audioClips[index];
                audioSource.Play();
            }
            yield break;
        }
    }

}