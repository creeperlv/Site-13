using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Site13Kernel.Stories.BE00
{
    public class SatelliteDestroySequence01 : MonoBehaviour
    {
        public float TimeDelay = 10f;
        public int NaviScene = 17;
        void Start()
        {
            StartCoroutine(Navigate());
        }
        IEnumerator Navigate()
        {
            yield return new WaitForSeconds(TimeDelay);
            SceneManager.LoadScene(NaviScene);
        }
    }


}