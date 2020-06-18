using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Site13Kernel
{
    public class AutoBackManimenu : MonoBehaviour
    {
        public float TimeDelay = 10f;
        void Start()
        {
            StartCoroutine(back());
        }
        IEnumerator back()
        {
            yield return new WaitForSeconds(TimeDelay);
            SceneManager.LoadScene(1);
        }
    }

}