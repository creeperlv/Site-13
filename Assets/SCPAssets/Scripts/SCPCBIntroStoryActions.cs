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

        public bool Action4_OpenDoor1 = false;
        private bool Action4_OpenDoor1_ = false;

        public bool Action5_CloseDoor1 = false;
        private bool Action5_CloseDoor1_ = false;

        public bool Action6_OpenDoor2 = false;
        private bool Action6_OpenDoor2_ = false;

        public bool Action7_CloseDoor2 = false;
        private bool Action7_CloseDoor2_ = false;


        public SCPDoor Door0;
        public SCPDoor Door1;
        public SCPDoor Door2;
        public SCPDoor Door3;
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
            if (Action4_OpenDoor1 == true)
            {
                if (Action4_OpenDoor1_ == false)
                {
                    Action4_OpenDoor1_ = true;
                    StartCoroutine(Door1.Open());
                }
            }
            if (Action5_CloseDoor1 == true)
            {
                if (Action5_CloseDoor1_ == false)
                {
                    Action5_CloseDoor1_ = true;
                    StartCoroutine(Door1.Close());
                }
            }
            if (Action6_OpenDoor2 == true)
            {
                if (Action6_OpenDoor2_ == false)
                {
                    Action6_OpenDoor2_ = true;
                    StartCoroutine(Door2.Open());
                }
            }
            if (Action7_CloseDoor2 == true)
            {
                if (Action7_CloseDoor2_ == false)
                {
                    Action7_CloseDoor2_ = true;
                    StartCoroutine(Door2.Close());
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