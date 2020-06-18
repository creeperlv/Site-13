using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Site13Kernel
{

    public class StairButton : SCPDoor
    {
        public int TargetSence;
        public GameInfo.EnterSourceType targetType = GameInfo.EnterSourceType.Stair1;
        public override IEnumerator Move()
        {
            GameInfo.CurrentGame.EnterSource = targetType;
            GameInfo.CurrentGame.saveControlProtocolMode = GameInfo.SaveControlProtocolMode.Enter;
            GameInfo.CurrentGame.NextScene = TargetSence;
            yield return new WaitForSeconds(0.1f);
            SceneManager.LoadScene(2);
            yield break;
        }
    }

}