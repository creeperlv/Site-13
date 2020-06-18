using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Stories
{
    public class SpaceBendedRoom : SCPStoryNodeBaseCode
    {
        public GameObject Solider;
        public override void StartStory()
        {
            isStarted = true;
            StartCoroutine(RealStart());
        }

        private IEnumerator RealStart()
        {
            Animator animation = Solider.GetComponent<Animator>();
            animation.enabled = true;   
            animation.Play("Jump1");
            float TimeP = 0;
            while (TimeP<1)
            {
                Solider.transform.Translate(Vector3.forward*1.5f * Time.deltaTime);
                TimeP += Time.deltaTime;
                yield return null;
            }
            animation.StopPlayback();
            TimeP = 0;
            while (TimeP<2)
            {
                Solider.transform.Translate(Vector3.down*3 * Time.deltaTime);
                TimeP += Time.deltaTime;
                yield return null;
            }

            yield break;
        }
    }
}
