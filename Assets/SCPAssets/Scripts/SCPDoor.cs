﻿using CLUNL.Data.Layer0.Buffers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public enum SecurityCredential
    {
        None, Level3, Level4, Level5
    }
    public enum DoorType
    {
        Old_Flat,
        V2_Animated,
    }
    public enum DoorState
    {
        Close, Closing, Open, Opening
    }
    [Serializable]
    public class AnimatedDoorData
    {
        public List<Animator> DoorAnimators;
        public DoorState CurrentState = DoorState.Close;
        public string OpenAnimatorTrigger;
        public float OpenAnimatorTime;
        public string OpenErrorAnimatorTrigger;
        public float OpenErrorAnimatorTime;
        public string CloseAnimatorTrigger;
        public float CloseAnimatorTime;
        public string ClosedTrigger;
        public string OpenTrigger;
    }
    public class SCPDoor : SCPInteractive,IByteBufferable
    {
        [Tooltip("Side 1 of the door.")]
        public GameObject Door1;
        [Tooltip("Side 2 of the door.")]
        public GameObject Door2;
        [Tooltip("How much will one side of the door will move.")]
        public float Movement = 1.5f;
        [Tooltip("How long will the movement last.")]
        public float DurationTime = 3f;
        [Header("Audio Settings", order = 1)]
        [Tooltip("Open sound of the door.")]
        public AudioSource DoorOpenAudio;
        [Tooltip("Close sound of the door.")]
        public AudioSource DoorCloseAudio;
        [Tooltip("Error sound of the door.")]
        public AudioSource DoorErrorAudio;
        [Tooltip("Unable to open sound of the door.")]
        public AudioSource DoorUnableToOpenAudio;
        [Tooltip("Operate sound of the door.")]
        public AudioSource DoorOperateAudio;
        [Header("Movement Settings", order = 6)]
        [Tooltip("Probability of how will the errors happen on the door.")]
        public float ErrorProbability = 0.2f;
        [Tooltip("Is the door locked.")]
        [StayPositive]
        [SerializeField]
        private bool isLocked = false;
        public bool isStayInsteadOfDeactive = false;
        [Tooltip("Do not modify when the door is programmatically locked.")]
        public bool OverrideDefaultLockMat = false;
        [Tooltip("The message will show when the player try to open a locked door.")]
        public string LockMessage = "The door is locked down.";
        [Tooltip("Determine whether the message is related to language.")]
        public bool isLanguageSpecific = false;
        public bool isNewButtonEnabled = false;
        public SecurityCredential RequiredSecurityCredential = SecurityCredential.None;
        Material OriginalMaterial;
        [SerializeField]
        public bool IsLocked
        {
            get => isLocked; set
            {
                isLocked = value;
                try
                {
                    if (OverrideDefaultLockMat == false)
                    {

                        if (value == true)
                        {
                            if (isNewButtonEnabled == false)
                                disp.material = StaticResources.MaterialLoader.DoorButton03;
                            else
                            {
                                try
                                {

                                    disp.material = StaticResources.MaterialLoader.DoorButton03_V2;
                                }
                                catch (System.Exception)
                                {
                                }
                            }
                        }
                        else
                        {
                            disp.material = OriginalMaterial;
                        }
                    }

                }
                catch (System.Exception)
                {
                }
            }
        }
        public DoorType DoorType = DoorType.Old_Flat;
        public AnimatedDoorData AnimationData;
        MeshRenderer disp;
        private void Start()
        {
            try
            {
                if (isNewButtonEnabled == false)
                {
                    disp = transform.Find("Button2").Find("node_id13").Find("node_id6").Find("node_id6 2").GetComponent<MeshRenderer>(); OriginalMaterial = disp.material;
                }
                else
                {
                    disp = transform.Find("Gate Access Machine").GetComponent<MeshRenderer>(); OriginalMaterial = disp.material; OriginalMaterial = disp.material;

                }
            }
            catch (Exception)
            {
            }
        }
        public virtual void ApplyState(bool isOpen)
        {
            if(DoorType== DoorType.Old_Flat)
            {
                if (isOpen == true)
                {
                    Door1.SetActive(false);
                    Door2.SetActive(false);
                }
                else
                {
                    Door1.SetActive(true);
                    Door2.SetActive(true);
                }
            }else if(DoorType== DoorType.V2_Animated)
            {

                if (isOpen == true)
                {
                    foreach (var item in AnimationData.DoorAnimators)
                    {
                        item.SetTrigger(AnimationData.OpenAnimatorTrigger);
                    }
                }
                else
                {
                    foreach (var item in AnimationData.DoorAnimators)
                    {
                        item.SetTrigger(AnimationData.CloseAnimatorTrigger);
                    }
                }
            }
        }
        public virtual bool GetState()
        {
            if (DoorType == DoorType.Old_Flat)
            {
                return Door1.activeInHierarchy;
            }
            else
            {
                switch (AnimationData.CurrentState)
                {
                    case DoorState.Close:
                        return true;
                    case DoorState.Closing:
                        return true;
                    case DoorState.Open:
                        return false;
                    case DoorState.Opening:
                        return false;
                    default:
                        return false;
                }
            }
        }
        public virtual IEnumerator OnOpen01()
        {
            yield break;
        }
        public virtual IEnumerator OnOpen02()
        {
            yield break;
        }
        public virtual IEnumerator OnClose01()
        {
            yield break;
        }
        public virtual IEnumerator OnClose02()
        {
            yield break;
        }
        public IEnumerator Open()
        {
            isOperating = true;
            StartCoroutine(OnOpen01());
            float TimeProgression = 0f;
            //false: Open:Active=false
            //true: Closed->To Open
            Vector3 Door1v;
            Vector3 Door2v;
            DoorOpenAudio.Play();
            Door1.gameObject.SetActive(true);
            Door2.gameObject.SetActive(true);
            Door1v = new Vector3(0, 0, Movement) * this.transform.parent.parent.localScale.y;
            Door2v = new Vector3(0, 0, -Movement) * this.transform.parent.parent.localScale.y;
            while (TimeProgression <= DurationTime)
            {
                TimeProgression += Time.deltaTime;
                Door1.transform.Translate((Door1v / DurationTime) * Time.deltaTime, Space.Self);
                Door2.transform.Translate((Door2v / DurationTime) * Time.deltaTime, Space.Self);

                yield return null;
            }
            if (isStayInsteadOfDeactive == false)
            {
                Door1.gameObject.SetActive(false);
                Door2.gameObject.SetActive(false);
            }
            StartCoroutine(OnOpen02());
            isOperating = false;
            yield break;
        }
        public IEnumerator Close()
        {
            isOperating = true;
            StartCoroutine(OnClose01());
            float TimeProgression = 0f;
            Vector3 Door1v;
            Vector3 Door2v;
            DoorCloseAudio.Play();
            {

                Vector3 t = Door1.transform.localPosition;
                t.x = 0;
                t.z = Movement;
                Door1.transform.localPosition = t;
            }
            {

                var t = Door2.transform.localPosition;
                t.x = 0;
                t.z = -Movement;
                Door2.transform.localPosition = t;
            }
            Door1.gameObject.SetActive(true);
            Door2.gameObject.SetActive(true);
            Door1v = new Vector3(0, 0, Movement) * this.transform.parent.parent.localScale.y;
            Door2v = new Vector3(0, 0, -Movement) * this.transform.parent.parent.localScale.y;

            {
                while (TimeProgression <= DurationTime)
                {
                    TimeProgression += Time.deltaTime;
                    Door1.transform.Translate(-(Door1v / DurationTime) * Time.deltaTime, Space.Self);
                    Door2.transform.Translate(-(Door2v / DurationTime) * Time.deltaTime, Space.Self);
                    yield return null;
                }

                {

                    var t = Door1.transform.localPosition;
                    t.x = 0;
                    t.z = 0;
                    Door1.transform.localPosition = t;
                }
                {

                    var t = Door2.transform.localPosition;
                    t.x = 0;
                    t.z = 0;
                    Door2.transform.localPosition = t;
                }

            }
            try
            {
                StartCoroutine(OnClose02());
            }
            catch (Exception)
            {
            }
            isOperating = false;
            yield break;
        }

        public IEnumerator Error()
        {
            isOperating = true;
            DoorErrorAudio.Play();
            float TimeProgression = 0f;
            Vector3 Door1v = new Vector3();
            Vector3 Door2v = new Vector3();
            Door1v = new Vector3(0, 0, Movement) * this.transform.parent.parent.localScale.y;
            Door2v = new Vector3(0, 0, -Movement) * this.transform.parent.parent.localScale.y;
            while (TimeProgression <= DurationTime * 0.51f)
            {
                TimeProgression += Time.deltaTime;
                Door1.transform.Translate((Door1v / DurationTime) * Time.deltaTime);
                Door2.transform.Translate((Door2v / DurationTime) * Time.deltaTime);
                yield return null;
            }
            while (TimeProgression <= (DurationTime) * 0.6)
            {
                TimeProgression += Time.deltaTime;
                Door1.transform.Translate(((-Door1v * 5f) / DurationTime) * Time.deltaTime);
                Door2.transform.Translate(((-Door2v * 5f) / DurationTime) * Time.deltaTime);
                yield return null;
            }
            {

                var t = Door1.transform.localPosition;
                t.x = 0;
                t.z = 0;
                Door1.transform.localPosition = t;
            }
            {

                var t = Door2.transform.localPosition;
                t.x = 0;
                t.z = 0;
                Door2.transform.localPosition = t;
            }
            yield return new WaitForSeconds(DurationTime * 0.4f);
            isOperating = false;
            yield break;
        }
        public bool JudgeWhetherOpen()
        {
            if (isStayInsteadOfDeactive != true)
            {
                if (Door1.activeInHierarchy == false)
                {
                    return true;
                }
                else
                {
                    if (Door1.activeInHierarchy == true)
                    {
                        if (Door1.transform.localPosition.z == 0)
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (Door1.transform.localPosition.z >= Movement)
                {
                    return true;
                }
                else if (Door1.transform.localPosition.z == 0)
                {
                    return false;
                }
            }
            return true;
        }
        public virtual IEnumerator AnimatedOpen()
        {
            isOperating = true;
            {
                //To Open trigger.
                foreach (var item in AnimationData.DoorAnimators)
                {
                    item.SetTrigger(AnimationData.OpenAnimatorTrigger);
                }
                yield return new WaitForSeconds(AnimationData.OpenAnimatorTime);
                foreach (var item in AnimationData.DoorAnimators)
                {
                    item.SetTrigger(AnimationData.OpenTrigger);
                }
            }
            isOperating = false;
            yield break;
        }
        public virtual IEnumerator AnimatedClose()
        {
            isOperating = true;
            {
                //To Close trigger.
                foreach (var item in AnimationData.DoorAnimators)
                {
                    item.SetTrigger(AnimationData.CloseAnimatorTrigger);
                }
                yield return new WaitForSeconds(AnimationData.CloseAnimatorTime);
                foreach (var item in AnimationData.DoorAnimators)
                {
                    item.SetTrigger(AnimationData.ClosedTrigger);
                }
            }
            isOperating = false;
            yield break;
        }
        public virtual IEnumerator AnimatedError()
        {
            isOperating = true;
            {
                //To Close trigger.
                foreach (var item in AnimationData.DoorAnimators)
                {
                    item.SetTrigger(AnimationData.OpenErrorAnimatorTrigger);
                }
                yield return new WaitForSeconds(AnimationData.OpenErrorAnimatorTime);
            }
            isOperating = false;
            yield break;
        }
        public override IEnumerator Move()
        {
            if (IsLocked == true)
            {
                DoorUnableToOpenAudio.Play();
                if (isLanguageSpecific == false)
                    GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle(LockMessage);
                else
                {
                    string msg = $"<color=red>Translation Error: Unable to locate '{LockMessage}'</color>";
                    try
                    {
                        msg = Site_13ToolLib.Globalization.Language.Language_Plot[LockMessage];
                    }
                    catch (Exception)
                    {
                    }
                    GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle(msg);
                }
                isOperating = true;
                yield return new WaitForSeconds(0.5f);
                isOperating = false;
            }
            else
            {

                bool willRun = false;
                if (DoorOperateAudio != null)
                    DoorOperateAudio.Play();
                switch (RequiredSecurityCredential)
                {
                    case SecurityCredential.None:
                        willRun = true;
                        break;
                    case SecurityCredential.Level3:
                        if (GameInfo.CurrentGame.HandingItem.SecurityClearance >= 3)
                        {
                            willRun = true;
                        }
                        break;
                    case SecurityCredential.Level4:
                        if (GameInfo.CurrentGame.HandingItem.SecurityClearance >= 4)
                        {
                            willRun = true;
                        }
                        break;
                    case SecurityCredential.Level5:
                        if (GameInfo.CurrentGame.HandingItem.SecurityClearance >= 5)
                        {
                            willRun = true;
                        }
                        break;
                    default:
                        break;
                }
                if (willRun == true)
                {
                    if (DoorType == DoorType.Old_Flat)
                    {
                        if (JudgeWhetherOpen() == true)
                        {
                            StartCoroutine(Close());

                        }
                        else
                        {
                            {
                                var probablility = UnityEngine.Random.Range(0f, 1f);
                                if (probablility > ErrorProbability)
                                {
                                    StartCoroutine(Open());
                                }
                                else
                                {
                                    StartCoroutine(Error());
                                }
                            }
                        }
                    }
                    else if (DoorType == DoorType.V2_Animated)
                    {
                        if (JudgeWhetherOpen() == true)
                        {
                            StartCoroutine(AnimatedClose());

                        }
                        else
                        {
                            {
                                var probablility = UnityEngine.Random.Range(0f, 1f);
                                if (probablility > ErrorProbability)
                                {
                                    StartCoroutine(AnimatedOpen());
                                }
                                else
                                {
                                    StartCoroutine(AnimatedError());
                                }
                            }
                        }
                    }
                }
                else
                {
                    isOperating = true;
                    DoorUnableToOpenAudio.Play();
                    GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("You need a higher security credential.");
                    yield return new WaitForSeconds(1f);
                    isOperating = false;
                }
            }
            yield break;
        }

        public void Deserialize(ByteBuffer buffer)
        {
            DataBuffer dataBuffer = DataBuffer.FromByteBuffer(buffer);
            ApplyState(dataBuffer.ReadBool());
            IsLocked = dataBuffer.ReadBool();
        }

        public ByteBuffer Serialize()
        {
            DataBuffer dataBuffer = new DataBuffer();
            dataBuffer.WriteBool(JudgeWhetherOpen());
            dataBuffer.WriteBool(IsLocked);
            return dataBuffer.ObtainByteArray();
        }
    }

}