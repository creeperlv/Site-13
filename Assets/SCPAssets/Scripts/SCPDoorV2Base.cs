using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.DoorV2
{
    public class SCPDoorV2Base : SCPBaseScript
    {
        public const string STATE_CLOSED = "CLOSED";
        public const string STATE_OPEN = "OPEN";

        public bool isPending = false;

        #region FX Settings
        public AudioSource DoorSFX;
        public AudioClip OpenSFX;
        public AudioClip CloseSFX;
        public AudioClip ErrorSFX;
        public AudioClip RefuseSFX;
        #endregion

        internal string CurrentState = STATE_CLOSED;
        public virtual string RetireState()
        {
            return CurrentState;
        }
        public virtual void Operate()
        {
            if (isPending == false)
            {
                if (CurrentState == STATE_CLOSED)
                {
                    StartCoroutine(Open());
                }
                else if (CurrentState == STATE_OPEN)
                {
                    StartCoroutine(Close());
                }
            }   
        }
        public virtual void ForceState(string StateName)
        {
            switch (StateName)
            {
                case STATE_CLOSED:
                    StartCoroutine(Close());
                    break;
                case STATE_OPEN:
                    StartCoroutine(Open());
                    break;
                default:
                    break;
            }
            CurrentState = StateName;
        }
        public virtual IEnumerator Open()
        {
            if (isPending == true)
                yield break;
            isPending = true;
            yield return new WaitForSeconds(.5f);

            if (CurrentState != STATE_OPEN)
                CurrentState = STATE_OPEN;
            isPending = false;

        }
        public virtual IEnumerator Close()
        {
            if (isPending == true)
                yield break;
            isPending = true;
            yield return new WaitForSeconds(.5f);

            if (CurrentState != STATE_CLOSED)
                CurrentState = STATE_CLOSED;
            isPending = false;
        }
        public override void StartScript()
        {
            base.StartScript();
        }
    }

}