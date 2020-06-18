using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.SCPs.Skip990
{
    public class SCP990Leave : SCPStoryNodeBaseCode
    {
        public GameObject ExitStory;
        public override void StartStory()
        {
            ExitStory.SetActive(true);
            GameInfo.CurrentGame.FirstPerson.enabled = false;
        }
    }

}