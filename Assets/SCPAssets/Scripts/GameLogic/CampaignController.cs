using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.GameLogic
{
    public class CampaignController : MonoBehaviour
    {
        public List<BehaviorController> behaviorControllers= new List<BehaviorController>();
        void Start()
        {

        }

        void Update()
        {

        }
    }
    
}
namespace Site13Kernel.GameLogic.CampaignActions
{
    [Serializable]
    public class CampaignAction { }
    [Serializable]
    public class LoadScene
    {
        public string Name;
    }
    [Serializable]
    public class WaitUntilLastSceneDone
    {

    }
}