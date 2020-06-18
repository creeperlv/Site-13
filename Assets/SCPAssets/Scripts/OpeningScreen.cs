using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.PostProcessing;
namespace Site13Kernel
{
    public class OpeningScreen : MonoBehaviour
    {
        public GameObject OperatingButton;
        public GameObject ControlledCamera;
        public float Speed = 1f;
        // Use this for initialization
        void Start()
        {

        }
        bool CamFlag = true;
        bool CamFlag2 = true;
        void CameraShake(float speed = 8f)
        {
            Vector3 vector3 = new Vector3(0, 0, speed);
            if (CamFlag == true)
            {
                ControlledCamera.transform.Rotate(vector3 * Time.deltaTime);
                if (ControlledCamera.transform.localEulerAngles.z >= 2f && ControlledCamera.transform.localEulerAngles.z < 300f)
                {
                    CamFlag = false;
                }
            }
            else if (CamFlag == false)
            {
                ControlledCamera.transform.Rotate(-vector3 * Time.deltaTime);
                if (ControlledCamera.transform.localEulerAngles.z - 360f <= -2f && ControlledCamera.transform.localEulerAngles.z > 300f)
                {
                    CamFlag = true;
                }
            }
        }
        void CameraVerticalMove(float speed = 1f)
        {
            Vector3 vector3 = Vector3.up * speed;
            if (CamFlag2 == true)
            {
                {
                    var p = ControlledCamera.transform.localPosition;
                    p.y += speed * Time.deltaTime;
                    ControlledCamera.transform.localPosition = p;
                }
                //ControlledCamera.transform.Translate(vector3 * Time.deltaTime);
                if (ControlledCamera.transform.localPosition.y >= 0.1)
                {
                    CamFlag2 = false;
                }
            }
            else if (CamFlag2 == false)
            {
                {
                    var p = ControlledCamera.transform.localPosition;
                    p.y -= speed * Time.deltaTime;
                    ControlledCamera.transform.localPosition = p;
                }
                //ControlledCamera.transform.Translate(-vector3 * Time.deltaTime);
                if (ControlledCamera.transform.localPosition.y <= 0.3)
                {
                    CamFlag2 = true;
                }
            }
        }
        float timeCounter = 0;
        int STAGE = 0;
        // Update is called once per frame
        void Update()
        {
            if (STAGE == 0)
            {
                CameraShake();
                Vector3 speed = new Vector3(0, 0, Speed);
                ControlledCamera.transform.Translate(speed * Time.deltaTime);
                if (ControlledCamera.transform.localPosition.x <= -5)
                {
                    STAGE++;
                }
            }
            else if (STAGE == 1)
            {
                StartCoroutine(OperatingButton.GetComponent<SCPDoor>().Open());
                STAGE++;
            }
            else if (STAGE == 2)
            {
                timeCounter += Time.deltaTime;
                if (timeCounter >= 1.7)
                {
                    STAGE++;
                }
            }
            else if (STAGE == 3)
            {
                CameraShake();

                Vector3 speed = new Vector3(0, 0, Speed);
                ControlledCamera.transform.Translate(speed * Time.deltaTime);
                if (ControlledCamera.transform.localPosition.x <= -13.3)
                {
                    STAGE++;
                    //ControlledCamera.GetComponent<PostProcessingBehaviour>().enabled = false;
                }
            }
            else if (STAGE == 4)
            {
                CameraShake(0.5f);
                CameraVerticalMove(0.02f);
            }
        }
    }

}