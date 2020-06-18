using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Site13Kernel
{

    public class SCPBGMManager : MonoBehaviour
    {
        public List<string> Alias;
        public List<AudioClip> RealBGMs;
        public AudioSource audioSource;
        public void ChangeBGM(string TargetAlia)
        {
            CurrentAlia = TargetAlia;
            var インデックス = Alias.IndexOf(TargetAlia);
            audioSource.clip = RealBGMs.ElementAt(インデックス);
            audioSource.Play();
        }
        public string CurrentAlia;
        private void Start()
        {
            GameInfo.CurrentGame.BGMManager = this;
            ChangeBGM(CurrentAlia);
        }
        public void StopBGM()
        {
            audioSource.Stop();
        }
        public void PlayBGM()
        {
            audioSource.Play();
        }
    }

}