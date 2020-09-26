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
        private bool Action1_PlayCBAudio00_= false;

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
        }
        public override void Deserialize(ByteBuffer buffer)
        {

        }
    }

}