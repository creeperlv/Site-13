using CLUNL.Data.Layer0.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Site13Kernel.IO;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using System.Linq;

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

        public bool Action8_OpenDoor3 = false;
        private bool Action8_OpenDoor3_ = false;

        public bool Action9_CloseDoor3 = false;
        private bool Action9_CloseDoor3_ = false;

        public bool Action10_OpenDoor3 = false;
        private bool Action10_OpenDoor3_ = false;

        public bool Action11_079InCharge = false;
        private bool Action11_079InCharge_ = false;

        public bool Action12_OutShelter = false;
        private bool Action12_OutShelter_ = false;


        public bool Action13_Explosion = false;
        private bool Action13_Explosion_ = false;

        public bool Constant_Action0_Blink = false;
        private bool Blink_0_0 = false;
        private float Blink_0_1 = 0;
        private float Blink_0_2 = 1;

        public bool DoF_Controll = false;

        public SCPDoor Door0;
        public SCPDoor Door1;
        public SCPDoor Door2;
        public SCPDoor Door3;
        public AudioSource CBAudio00;
        public AudioClip _079InCharge;
        public AudioSource OutShelter;
        public Image BlackCover;
        public AudioSource Explosion;
        public PostProcessProfile TargetProfile;
        public float DoF_Length = 10;
        DepthOfField Depth;
        private void Start()
        {
            foreach (var item in
            TargetProfile.settings)
            {
                if(item is DepthOfField)
                {
                    Depth = item as DepthOfField;
                }
            }
        }
        void Update()
        {
            if (DoF_Controll == true)
            {
                if (Depth.enabled.value == false) Depth.enabled.value= true;
                Depth.focusDistance.value= DoF_Length;
            }else
                if (Depth.enabled.value == true) Depth.enabled.value= false;
            if (Constant_Action0_Blink == true)
            {
                if (Blink_0_0 == false)
                {
                    var c = BlackCover.color;
                    c.a -= Time.deltaTime*1.5f;
                    Blink_0_2 -= Time.deltaTime * 1.5f;
                    BlackCover.color = c;
                    if (Blink_0_2 <= 0)
                    {
                        Blink_0_2 = 0;
                        Blink_0_0 = true;
                    }
                }
                else
                {
                    if (Blink_0_1 < 1)
                    {
                        Blink_0_1 += Time.deltaTime*1.5f;
                    }
                    else
                    {
                        var c = BlackCover.color;
                        c.a += Time.deltaTime * 1.5f;
                        Blink_0_2 += Time.deltaTime * 1.5f;
                        BlackCover.color = c;
                        if (Blink_0_2 >= 1)
                        {
                            Blink_0_0 = false;
                            Blink_0_1 = 0;
                            Blink_0_2 = 1;
                        }
                    }
                }
            }
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
            if (Action8_OpenDoor3==true)
            {
                if (Action8_OpenDoor3_ == false)
                {
                    Action8_OpenDoor3_ = true;
                    StartCoroutine(Door3.Open());
                }
            }
            if (Action9_CloseDoor3 == true)
            {
                if (Action9_CloseDoor3_ == false)
                {
                    Action9_CloseDoor3_ = true;
                    StartCoroutine(Door3.Close());
                }
            }
            if (Action10_OpenDoor3 == true)
            {
                if (Action10_OpenDoor3_ == false)
                {
                    Action10_OpenDoor3_ = true;
                    StartCoroutine(Door3.Open());
                }
            }
            if (Action11_079InCharge == true)
            {
                if (Action11_079InCharge_ == false)
                {
                    Action11_079InCharge_ = true;
                    CBAudio00.clip = _079InCharge;
                    CBAudio00.Play();
                }
            }
            if (Action12_OutShelter == true)
            {
                if (Action12_OutShelter_ == false)
                {
                    Action12_OutShelter_ = true;
                    OutShelter.Play();
                }
            }
            if (Action13_Explosion == true)
            {
                if (Action13_Explosion_ == false)
                {
                    Action13_Explosion_ = true;
                    Explosion.Play();
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