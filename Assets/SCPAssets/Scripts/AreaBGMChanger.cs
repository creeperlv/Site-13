using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public class AreaBGMChanger : MonoBehaviour
    {
        public string TargetBGMAlia;
        public bool WillRevertToOriginalBGM=true;
        string LastBGMAlia;
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<SCPFirstController>() != null)
            {
                if (GameInfo.CurrentGame.BGMManager.CurrentAlia != TargetBGMAlia)
                {
                    LastBGMAlia = GameInfo.CurrentGame.BGMManager.CurrentAlia.ToString();
                    GameInfo.CurrentGame.BGMManager.ChangeBGM(TargetBGMAlia);
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if(WillRevertToOriginalBGM==true)
            if (other.GetComponent<SCPFirstController>() != null)
                {
                    if (GameInfo.CurrentGame.BGMManager.CurrentAlia == TargetBGMAlia)
                    {
                        GameInfo.CurrentGame.BGMManager.ChangeBGM(LastBGMAlia);
                    }
            }
        }
    }

}