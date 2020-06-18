using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{

    public class SCPCamera : MonoBehaviour
    {
        public float speed = 5f;
        public float maxAngle = 30f;
        public bool Debug = false;
        Vector3 RealSpeed;
        Vector3 RealSpeed2;
        // Start is called before the first frame update
        void Start()
        {
            RealSpeed = new Vector3(0, speed, 0);
            RealSpeed2 = new Vector3(0, -speed, 0);

        }
        bool FlagA = false;
        // Update is called once per frame
        void Update()
        {
            if(Debug)
            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle((transform.localRotation.eulerAngles.y) + "");
            if (FlagA)
            {
                transform.Rotate(RealSpeed * Time.deltaTime);
                if ((transform.localRotation.eulerAngles.y)<180&(transform.localRotation.eulerAngles.y) >= maxAngle)
                {
                    FlagA = false;
                }
            }
            else
            {
                transform.Rotate(RealSpeed2 * Time.deltaTime);
                if ((transform.localRotation.eulerAngles.y)>180&(transform.localRotation.eulerAngles.y) <= (360- maxAngle))
                {
                    FlagA = true;
                }
            }
        }
    }
}
