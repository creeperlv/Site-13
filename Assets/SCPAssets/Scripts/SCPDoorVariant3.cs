using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.DoorV2
{
    public class SCPDoorVariant3 : SCPDoorV2Base
    {
        public Animator CentralAnimator;
        public float DoorOperationDuration = 2f;
        public override IEnumerator Open()
        {
            isPending = true;
            if (DoorSFX!=null&&OpenSFX!=null)
            {
                DoorSFX.clip = OpenSFX;
                DoorSFX.Play();
            }
            CentralAnimator.SetTrigger("Open");
            yield return new WaitForSeconds(DoorOperationDuration);
            CurrentState = STATE_OPEN;
            isPending = false;
        }
        public override IEnumerator Close()
        {
            isPending = true;
            if (DoorSFX != null && CloseSFX != null)
            {
                DoorSFX.clip = CloseSFX;
                DoorSFX.Play();
            }
            CentralAnimator.SetTrigger("Close");
            yield return new WaitForSeconds(DoorOperationDuration);
            CurrentState = STATE_CLOSED;
            isPending = false;
        }
        public override void ForceState(string StateName)
        {
            base.ForceState(StateName);
        }
    }

}
