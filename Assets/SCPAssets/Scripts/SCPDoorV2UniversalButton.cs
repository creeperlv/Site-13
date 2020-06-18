using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.DoorV2
{
    public class SCPDoorV2UniversalButton : SCPInteractive
    {
        public DoorStructure DoorStructure;
        public SCPDoorV2Base SCPDoorV2;
        public AudioSource ButtonSoundSource;
        public AudioClip ButtonSFX;
        public override void StartScript()
        {
            if (DoorStructure == DoorStructure.Parallel)
            {
                SCPDoorV2 = transform.parent.GetComponent<SCPDoorV2Base>();
            }
        }
        public override IEnumerator Move()
        {
            isOperating = true;
            SCPDoorV2.Operate();
            if (ButtonSFX != null)
            {
                ButtonSoundSource.clip = ButtonSFX;
                ButtonSoundSource.Play();
            }
            yield return new WaitForSeconds(.5f);
            isOperating = false;
        }
    }
    public enum DoorStructure
    {
        Parallel,Customed
    }
}
