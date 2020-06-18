using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public class HyperionLiftController : SCPInteractive
    {
        public Transform d01;
        public Transform d02;
        public Transform d03;
        public Transform Lift;
        public bool isOutsideButton;
        public AreaSFXPlayer Arr;
        public AudioSource Door;
        public AudioSource Mov;
        public int Position = 0;//0 or 1
        private void Start()
        {
            //-22.549
            //0
        }
        IEnumerator GoUp()
        {
            while (Lift.localPosition.y <= 1)
            {
                Lift.Translate(Vector3.up * 2 * Time.deltaTime);
                yield return null;
            }
            {
                var lp = Lift.localPosition;
                lp.y = 1;
                Lift.localPosition = lp;
            }
        }
        IEnumerator GoDown()
        {
            Arr.isPlayed = false;
            while (Lift.localPosition.y >= -22.549)
            {
                Lift.Translate(Vector3.down * 2 * Time.deltaTime);
                yield return null;
            }
            {
                var lp = Lift.localPosition;
                lp.y = -22.549f;
                Lift.localPosition = lp;
            }
        }
        IEnumerator OpenDoor()
        {
            //D03: -1.55
            Door.Play();
            while (d03.localPosition.x >= -1.558)
            {
                d03.Translate(Vector3.left * 1.5f * Time.deltaTime);
                d01.Translate(Vector3.up * 1.82f * Time.deltaTime);
                d02.Translate(Vector3.down * 2f * Time.deltaTime);
                yield return null;
            }
            {
                var lp = d03.localPosition;
                lp.x = -1.558f;
                d03.localPosition = lp;
            }
            {
                var lp = d01.localPosition;
                lp.y = 1.8f;
                d01.localPosition = lp;
            }
            {
                var lp = d02.localPosition;
                lp.x = -1.9f;
                d02.localPosition = lp;
            }
            yield break;
        }
        IEnumerator CloseDoor()
        {
            Door.Play();
            //D03: -1.55
            while (d03.localPosition.x <= 0)
            {
                d03.Translate(Vector3.right * 1.5f * Time.deltaTime);
                d01.Translate(Vector3.down * 1.82f * Time.deltaTime);
                d02.Translate(Vector3.up * 2f * Time.deltaTime);
                yield return null;
            }
            {
                var lp = d03.localPosition;
                lp.x = 0f;
                d03.localPosition = lp;
            }
            {
                var lp = d01.localPosition;
                lp.y = 0f;
                d01.localPosition = lp;
            }
            {
                var lp = d02.localPosition;
                lp.x = 0f;
                d02.localPosition = lp;
            }
            yield break;
        }
        public override IEnumerator Move()
        {
            isOperating = true;
            if (isOutsideButton == true)
            {
                if (d03.localPosition.x == 0)
                    StartCoroutine(OpenDoor());
                else
                {
                    StartCoroutine(CloseDoor());
                }
                yield return new WaitForSeconds(1.5f);
                isOperating = false;
                yield break;
            }
            else
            {
                if(d03.localPosition.x!=0)
                StartCoroutine(CloseDoor());
                yield return new WaitForSeconds(1.5f);
                Mov.Play();
                yield return new WaitForSeconds(2f);
                if (Lift.localPosition.y <= -22.54)
                {
                    StartCoroutine(GoUp());
                }
                else if (Lift.localPosition.y == 1)
                {
                    StartCoroutine(GoDown());

                }
                yield return new WaitForSeconds(12f);
                StartCoroutine(OpenDoor());
                yield return new WaitForSeconds(4f);
                StartCoroutine(CloseDoor());
                yield return new WaitForSeconds(1f);
                isOperating = false;
            }
        }

    }
}