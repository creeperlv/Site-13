using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Site13Kernel.Audio
{

    public class SCPBGMManager : MonoBehaviour
    {
        public List<string> Alias;
        public List<AudioClip> RealBGMs;
        public AudioSource audioSource;
        public AudioSource audioSource1;
        bool State0 = false;//False - audio0,True - Audio1
        bool isWorking = false;
        public void ChangeBGM(string TargetAlia)
        {
            CurrentAlia = TargetAlia;
            var インデックス = Alias.IndexOf(TargetAlia);
            if (State0 == false)
            {
                audioSource1.clip = RealBGMs.ElementAt(インデックス);
                audioSource1.volume = 0;
                audioSource1.Play(); 
                State0 = true;
            }
            else
            if (State0 == true)
            {
                audioSource.clip = RealBGMs.ElementAt(インデックス);
                audioSource.volume = 0;
                audioSource.Play(); 
                State0 = false;
            }
            if (isWorking == false)
            {
                StartCoroutine(BGMTransition());
            }
            else
            {
                isWorking = false;
            }
        }
        public IEnumerator BGMTransition()
        {
            isWorking = true;
            float TimeDelta = 0;
            while (TimeDelta < 1)
            {

                if (State0 == false)
                {

                    if(audioSource.volume<.6f)
                    audioSource.volume += Time.deltaTime*0.6f;
                    if(audioSource1.volume>0f)
                    audioSource1.volume -= Time.deltaTime*0.6f;
                }
                else
                {

                    if(audioSource1.volume<.6f)
                    audioSource1.volume += Time.deltaTime * 0.6f;
                    if(audioSource.volume>0f)
                    audioSource.volume -= Time.deltaTime * 0.6f;
                }


                if (isWorking == false)
                {
                    isWorking = true;
                    TimeDelta = 0;
                }
                yield return null;
            }
            yield return null;

        }
        public string CurrentAlia;
        private void Start()
        {
            GameInfo.CurrentGame.BGMManager = this;
            ChangeBGM(CurrentAlia);
        }
        public void StopBGM()
        {
            if (State0 == false)
                audioSource.Stop();
            else audioSource1.Stop();
        }
        public void PlayBGM()
        {
            if (State0 == false)
                audioSource.Play();
            else audioSource1.Play();
        }
    }

}