using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.MTF
{
    public class Delta12Teammate : HumanCharacterBaseCode
    {
        public AudioSource AudioSource;
        void Start()
        {
            Team = Teams.MTF_Delta_12;
            try
            {

                footSteps = GameInfo.CurrentGame.currentFootStepSFXManager.NormalSteps;
            }
            catch (System.Exception)
            {
            }
        }
        [HideInInspector]
        public bool PlayWalkSound = false;
        float timed = 0.0f;
        void Update()
        {
            if (PlayWalkSound)
            {
                if (footSteps == null)
                {
                    footSteps = GameInfo.CurrentGame.currentFootStepSFXManager.NormalSteps;
                }
                else if (footSteps.Count == 0)
                {
                    footSteps = GameInfo.CurrentGame.currentFootStepSFXManager.NormalSteps;
                }
                else
                {

                    timed += Time.deltaTime;
                    if (timed > .6f)
                    {
                        AudioSource.clip = footSteps[Random.Range(0, footSteps.Count)];
                        AudioSource.Play();
                        timed = 0;
                    }
                }
            }
        }
    }
    public class HumanCharacterBaseCode : MonoBehaviour
    {
        [HideInInspector]
        public List<AudioClip> footSteps;
        [HideInInspector]
        public Teams Team=Teams.Generic;
    }
    public enum Teams
    {
        Generic, MTF_Delta_12,Site13_Scientist,Site13_ClassD,MTF_Gamma_24,MTF_Zeta_9,MTF_Apollo_3,MTF_Tau_5,Foundation_Guard,
    }

}