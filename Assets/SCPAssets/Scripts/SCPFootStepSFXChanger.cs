using Site13Kernel.MTF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{

    public class SCPFootStepSFXChanger : SCPStoryNodeBaseCode
    {
        public string StepSetName = "Normal";
        // Start is called before the first frame update
        void Start()
        {

        }
        public override void SideEnter(Collider other)
        {
            var Human = other.GetComponent<HumanCharacterBaseCode>();
            if (Human != null)
            {
                if (StepSetName == "Normal")
                {

                    Human.footSteps = GameInfo.CurrentGame.currentFootStepSFXManager.NormalSteps;
                }
                else
                    Human.footSteps = GameInfo.CurrentGame.currentFootStepSFXManager.StepCollections[StepSetName];
            }
        }
        public override void StartStory()
        {
            if (GameInfo.CurrentGame.FlagsGroup.ContainsKey("FootStep"))
            {

                GameInfo.CurrentGame.FlagsGroup["FootStep"] = StepSetName;
            }
            else
            {
                GameInfo.CurrentGame.FlagsGroup.Add("FootStep", StepSetName);
            }
            if (StepSetName == "Normal")
            {

                GameInfo.CurrentGame.FirstPerson.footStepSounds = GameInfo.CurrentGame.currentFootStepSFXManager.NormalSteps.ToArray();
            }
            else
                GameInfo.CurrentGame.FirstPerson.footStepSounds = GameInfo.CurrentGame.currentFootStepSFXManager.StepCollections[StepSetName].ToArray();
        }
    }

}