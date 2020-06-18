using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.SCPs.Skip173
{

    public class SCP173Door : SCPDoor
    {
        public GameObject SCP173Detector;
        public SCPDoor door1;
        public SCPDoor door2;
        public SCPDoor door3;
        public override IEnumerator OnOpen01()
        {
            SCP173Detector.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            try
            {

                {
                    if (door1.JudgeWhetherOpen())
                    {
                        StartCoroutine(door1.Close());
                    }
                    if (door2.JudgeWhetherOpen())
                    {
                        StartCoroutine(door2.Close());
                    }
                    if (door3.JudgeWhetherOpen())
                    {
                        StartCoroutine(door3.Close());
                    }

                }
            }
            catch (System.Exception)
            {
            }
            yield break;
        }
        public override IEnumerator OnClose01()
        {
            SCP173Detector.SetActive(false);
            yield break;
        }
    }

}