using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{

    public class SCPFootStepSFXManager : MonoBehaviour
    {
        public List<AudioClip> NormalSteps;
        public List<stepSFXCollection> Collections;
        public Dictionary<string, List<AudioClip>> StepCollections=new Dictionary<string, List<AudioClip>>();
        [System.Serializable]
        public class stepSFXCollection
        {
            public string Name;
            public List<AudioClip> Steps;
        }
        void Start()
        {
            GameInfo.CurrentGame.currentFootStepSFXManager = this;
            foreach (var item in Collections)
            {
                StepCollections.Add(item.Name, item.Steps);
            }
        }
        
    }

}