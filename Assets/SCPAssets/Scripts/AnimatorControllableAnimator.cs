using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{

    public class AnimatorControllableAnimator : MonoBehaviour
    {
        public int TargetTrigger;
        public List<string> TriggerNames;
        int LastTrigger = 0;
        public Animator ControlledAnimator;
        void Start()
        {

        }
        void Update()
        {
            if (LastTrigger != TargetTrigger)
            {
                ControlledAnimator.SetTrigger(TriggerNames[TargetTrigger]);
                LastTrigger = TargetTrigger;
            }
        }
    }

}