using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{

    public class SCPAnimatorControllableAnimator : MonoBehaviour
    {
        int CurrentTrigger = 0;
        public int TargetTrigger = 0;
        public List<string> Triggers = new List<string>();
        public Animator TargetAnimator;
        // Update is called once per frame
        void Update()
        {
            if (CurrentTrigger != TargetTrigger)
            {
                try
                {
                    TargetAnimator.SetTrigger(Triggers[TargetTrigger]);
                }
                catch (System.Exception)
                {
                }
                CurrentTrigger = TargetTrigger;
            }
        }
    }

}