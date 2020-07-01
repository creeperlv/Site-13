using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Site13Kernel.DynamicScene
{
    public class SceneHelper : MonoBehaviour
    {
        [Header("Helper Functions")]
        public bool PrintOutSceneName;
        void Start()
        {
            if (PrintOutSceneName == true)
            {
                Debug.Log("Scene:"+SceneManager.GetActiveScene().name);
            }
        }

    }

}