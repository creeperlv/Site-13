using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{

    public class SCPDoorAutomation : MonoBehaviour
    {
        public SCPDoor doorController;
        public int AutomationLevel = 0;
        public bool isCheckPoint=false;
        // Start is called before the first frame update
        void Start()
        {
        }
        // Update is called once per frame
        private void OnTriggerEnter(Collider other)
        {
            var a = other.GetComponent<SCPEntity>();
            if (a == null)
            {
            }
            else
            {
                if (a.DoorAutomationLevel >= AutomationLevel)
                {
                    if (doorController.JudgeWhetherOpen() == false)
                        if (doorController.isOperating == false)
                        {
                            if (isCheckPoint == false)
                            {

                                StartCoroutine(doorController.Open());
                            }
                            else
                            {
                                StartCoroutine((doorController as CheckPointDoor).CoreMove());
                            }
                        }
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            var a = other.GetComponent<SCPEntity>();
            if (a == null)
            {

            }
            else
            {
                if (a.DoorAutomationLevel >= AutomationLevel)
                {
                    if (doorController.JudgeWhetherOpen() == true)
                        if (doorController.isOperating == false)
                        {
                            if (isCheckPoint == false)
                            {
                                StartCoroutine(doorController.Close());
                            }
                        }
                }
            }
        }
    }

}