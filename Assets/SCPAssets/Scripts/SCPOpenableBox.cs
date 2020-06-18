using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{

    public class SCPOpenableBox : SCPInteractive
    {
        public Animator animator;
        public string OpenTrigger = "Open";
        public string CloseTrigger = "Close";
        // Start is called before the first frame update
        void Start()
        {

        }
        [HideInInspector]
        public bool isOpen = false;
        public override IEnumerator Move()
        {
            isOperating = true;
            if (isOpen == false)
            {

                animator.SetTrigger(OpenTrigger);
                isOpen = !isOpen;

            }
            else
            {
                animator.SetTrigger(CloseTrigger); isOpen = !isOpen;
            }
            yield return new WaitForSeconds(1);
            isOperating = false;
        }
        // Update is called once per frame
        void Update()
        {

        }
    }

}