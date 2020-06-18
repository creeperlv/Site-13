using Site13Kernel.MTF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Events
{
    public class IsolationgProgramV0 : SCPStoryNodeBaseCode
    {
        public List<ParticleSystem> particleSystems;
        public AudioSource Airlock;
        bool isPending = false;
        public SCPDoor D1;
        public SCPDoor D2;
        public bool isPostiveDetection = false;
        public bool isDetectHuman = true;
        int Count = 0;
        public int MinHumanPerWave = 6;
        public override void StartStory()
        {
            if (isPostiveDetection)
                if (isPending == false)
                {
                    //Debug.Log("Humans:" + Count);
                    //if (Count >= MinHumanPerWave)
                    //   StartCoroutine(RealOperation());
                }
        }
        bool willD2Open = true;
        public override void SideExit(Collider other)
        {
            isStarted = false;
        }
        public override void SideEnter(Collider other)
        {
            if (isPending == false)
            {
                if (other.GetComponent<HumanCharacterBaseCode>() != null)
                {
                    Count++;
                    if (Count >= MinHumanPerWave)
                        StartCoroutine(RealOperation());

                }
                else if (other.GetComponent<SCPFirstController>() != null)
                {
                    Count++;
                    if (Count >= MinHumanPerWave)
                        StartCoroutine(RealOperation());

                }
            }
        }
        public void StartIsolation()
        {
            StartCoroutine(RealOperation());
        }
        public IEnumerator RealOperation()
        {
            isPending = true;
            isStarted = true;
            D1.IsLocked = true;
            D1.LockMessage = "Please wait until decontamination procedure complete.";
            D2.IsLocked = true;
            D2.LockMessage = "Please wait until decontamination procedure complete.";
            yield return new WaitForSeconds(2);
            if (D1.JudgeWhetherOpen() == true)
            {
                willD2Open = true;
                StartCoroutine(D1.Close());
            }
            if (D2.JudgeWhetherOpen() == true)
            {
                willD2Open = false;
                StartCoroutine(D2.Close());
            }
            D1.IsLocked = true;
            D1.LockMessage = "Please wait until decontamination procedure complete.";
            D2.IsLocked = true;
            D2.LockMessage = "Please wait until decontamination procedure complete.";
            yield return new WaitForSeconds(2);
            foreach (var item in particleSystems)
            {
                item.Play();
            }
            Airlock.Play();
            yield return new WaitForSeconds(6);
            foreach (var item in particleSystems)
            {
                item.Stop();
            }
            D1.IsLocked = false;
            D2.IsLocked = false;
            if (willD2Open == true)
            {

                StartCoroutine(D2.Open());
            }
            else
                StartCoroutine(D1.Open());
            yield return new WaitForSeconds(2);
            Count = 0;
            isPending = false;
        }
    }

}