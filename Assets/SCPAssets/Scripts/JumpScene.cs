using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Site13Kernel
{
    public class JumpScene : SCPStoryNodeBaseCode
    {
        public int SceneID = 0;
        public override void StartStory()
        {
            SceneManager.LoadScene(SceneID);
        }
    }

}