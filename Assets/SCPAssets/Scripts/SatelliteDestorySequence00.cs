using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Site13Kernel.Stories.BE00
{
    public class SatelliteDestorySequence00 : MonoBehaviour
    {
        public Animator ShieldAnimator;
        void Start()
        {
            StartCoroutine(StartStory());
        }
        IEnumerator StartStory()
        {
            yield return new WaitForSeconds(7);
            ShieldAnimator.enabled = true;
            yield return new WaitForSeconds(17);
            SceneManager.LoadScene(16);
            yield break;
        }

    }

}