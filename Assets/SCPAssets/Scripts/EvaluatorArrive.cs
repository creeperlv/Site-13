using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public class EvaluatorArrive : SCPStoryNodeBaseCode
    {
        public SCPDoor TargetDoor;
        public override void StartStory()
        {
            isStarted = true;
            if (TargetDoor.JudgeWhetherOpen() == false)
            {
                if (GameInfo.CurrentGame.isCurrentArrived == false)
                {
                    GameInfo.CurrentGame.isCurrentArrived = true;
                    StartCoroutine(TargetDoor.Open());
                }
            }
        }
    }

}