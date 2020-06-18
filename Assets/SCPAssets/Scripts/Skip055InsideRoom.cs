using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.SCPs
{

    public class Skip055InsideRoom : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<SCPFirstController>() != null)
            {
                try
                {
                    GameInfo.CurrentGame.FlagsGroup["SCP055Status"] = "Memoried";
                }
                catch (System.Exception)
                {
                    GameInfo.CurrentGame.FlagsGroup.Add("SCP055Status", "Memoried");
                }
            }
        }
    }

}