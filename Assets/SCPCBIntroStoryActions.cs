using CLUNL.Data.Layer0.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Site13Kernel.IO;
namespace Site13Kernel.Stories
{
    public class SCPCBIntroStoryActions : BytableBehavior
    {
        public bool Action0_ShowSubtitle00 = false;
        private bool Action0_ShowSubtitle00_ = false;
        public bool Action1_PlayCBAudio00 = false;
        private bool Action1_PlayCBAudio00_ = false;
        public bool Action2_OpenDoor0 = false;
        private bool Action2_OpenDoor0_ = false;
        public bool Action3_CloseDoor0 = false;
        private bool Action3_CloseDoor0_ = false;

        public SCPDoor Door0;
        public AudioSource CBAudio00;

        void Update()
        {
            if (Action0_ShowSubtitle00 == true)
            {
                if (Action0_ShowSubtitle00_ == false)
                {
                    Action0_ShowSubtitle00_ = true;
                    GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("[Replacement.0]");
                }
            }
            if (Action1_PlayCBAudio00 == true)
            {
                if (Action1_PlayCBAudio00_ == false)
                {
                    Action1_PlayCBAudio00_ = true;
                    CBAudio00.Play();
                }
            }
            if (Action2_OpenDoor0 == true)
            {
                if (Action2_OpenDoor0_ == false)
                {
                    Action2_OpenDoor0_ = true;
                    StartCoroutine(Door0.Open());
                }
            }
            if (Action3_CloseDoor0 == true)
            {
                if (Action3_CloseDoor0_ == false)
                {
                    Action3_CloseDoor0_ = true;
                    StartCoroutine(Door0.Close());
                }
            }
        }
        public override void Deserialize(ByteBuffer buffer)
        {

        }

        public override ByteBuffer Serialize()
        {
            return null;
        }
    }

}