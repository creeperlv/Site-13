using Site13Kernel;
using System;
//using SCPAssets.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.PostProcessing;

public class BeforeCBScript : SCPBaseScript
{

    public GameObject ControlledCamera;
    public GameObject RealPlayerCamera;
    public UnityEngine.Rendering.PostProcessing.PostProcessProfile processingProfile;
    public GameObject BeginingDoorButton1;
    public GameObject BeginingDoorButton2;
    public GameObject BeginingDoorButton3;
    public AudioSource FootstepSource;
    public AudioClip Foot1;
    public AudioClip Foot2;
    public GameObject BlinkCanvas;
    public GameObject BlackCover;
    public float Speed = 1f;
    bool needDo = true;
    // Use this for initialization
    public void Start()
    {
        var c = BlackCover.GetComponent<Image>().color;
        c.a = 0;
        BlackCover.GetComponent<Image>().color = c;
        processingProfile.GetSetting<UnityEngine.Rendering.PostProcessing.Vignette>();
        //var vms=processingProfile.profile.GetSetting<UnityEditor.Rendering.PostProcessing.PostProcessEditorAttribute.;
        //vms.intensity = 0.5f;
        //vms.smoothness = 0.5f;
        //processingProfile.vignette.settings = vms;
        if (isStarted==false)
        {
            isStarted = true;
        }
        else
        {
            needDo = false;
            RealPlayerCamera.SetActive(true);
            stage = 9;
            //ControlledCamera.SetActive(false);
        }

    }
    public AudioSource Guard_1;
    float BlinkTime = 1.5f;
    void Blink()
    {
        BlinkTime -= Time.deltaTime;
        if (BlinkTime <= 0)
        {
            {
                //Fade blink
                if (BlinkTime >= -0.25f)
                {
                    try
                    {
                        var c = BlackCover.GetComponent<Image>().color;
                        c.a = (Math.Abs( BlinkTime)*4);
                        BlackCover.GetComponent<Image>().color = c;
                    }
                    catch (System.Exception)
                    {
                    }
                }
                else if (BlinkTime > -1.5f)
                {
                    try
                    {

                        var c = BlackCover.GetComponent<Image>().color;
                        c.a = 1-(Math.Abs(BlinkTime) - 1.25f) * 4;
                        BlackCover.GetComponent<Image>().color = c;
                    }
                    catch (System.Exception)
                    {
                    }
                }
                else
                {

                    var c = BlackCover.GetComponent<Image>().color;
                    c.a = 0;
                    BlackCover.GetComponent<Image>().color = c;
                    BlinkTime = 1.5f;
                }
            }
        }
    }
    int stage = 0;
    float timeCounter = 0;
    bool CamFlag = true;
    void CameraShake(float speed = 4f)
    {
        Vector3 vector3 = new Vector3(0, 0, speed);
        if (CamFlag == true)
        {
            ControlledCamera.transform.Rotate(vector3 * Time.deltaTime);
            if (ControlledCamera.transform.localEulerAngles.z >= 1f + UnityEngine.Random.Range(-1, 1) && ControlledCamera.transform.localEulerAngles.z < 300f)
            {
                CamFlag = false;
                FootstepSource.clip = Foot1;
                FootstepSource.Play();
            }
        }
        else if (CamFlag == false)
        {
            ControlledCamera.transform.Rotate(-vector3 * Time.deltaTime);
            if (ControlledCamera.transform.localEulerAngles.z - 360f <= -1f + UnityEngine.Random.Range(-1, 1) && ControlledCamera.transform.localEulerAngles.z > 300f)
            {
                CamFlag = true;
                FootstepSource.clip = Foot2;
                FootstepSource.Play();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        if (needDo)
        {
            Blink();
            if (stage == 0)
            {
                Vector3 speed = Vector3.forward * Speed;
                ControlledCamera.transform.Translate(speed * Time.deltaTime);
                CameraShake();

                //SubtitleText.GetComponent<SubtitleController>().ShowSubtitle("[DBG]" + ControlledCamera.transform.localEulerAngles.z + ",y" + ControlledCamera.transform.localEulerAngles.y + ",CAM" + CamFlag, 5f);
                if (ControlledCamera.transform.localPosition.z <= 19)
                {
                    {
                        var lp = ControlledCamera.transform.localPosition;
                        lp.z = 19;
                        ControlledCamera.transform.localPosition = lp;
                    }
                    StartCoroutine(BeginingDoorButton1.GetComponent<SCPDoor>().Open());
                    stage = 1;
                }
            }
            else if (stage == 1)
            {
                timeCounter += Time.deltaTime;
                if (timeCounter >= 1.7)
                {
                    stage = 2;
                }
            }
            else if (stage == 2)
            {
                Vector3 speed = Vector3.forward * Speed;
                ControlledCamera.transform.Translate(speed * Time.deltaTime);
                CameraShake();
                if (ControlledCamera.transform.localPosition.z <= 17f && BeginingDoorButton1.GetComponent<SCPDoor>().JudgeWhetherOpen() == true && BeginingDoorButton1.GetComponent<SCPDoor>().isOperating == false)
                {

                    StartCoroutine(BeginingDoorButton1.GetComponent<SCPDoor>().Close());
                }
                if (ControlledCamera.transform.localPosition.z <= 2.7f)
                {
                    {
                        var lp = ControlledCamera.transform.localPosition;
                        lp.z = 2.7f;
                        ControlledCamera.transform.localPosition = lp;
                    }
                    stage = 3;
                }
            }
            else if (stage == 3)
            {

                Vector3 speed = new Vector3(0, -90, 0);
                ControlledCamera.transform.Rotate(speed * Time.deltaTime);
                //GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("[DBG]"+ ControlledCamera.transform.localRotation.eulerAngles.y);
                if (ControlledCamera.transform.localRotation.eulerAngles.y <= 90)
                {
                    var o = ControlledCamera.transform.localRotation;
                    var ang = o.eulerAngles;
                    ang.z = 0;
                    ang.y = 90;
                    ang.x = 0;
                    o.eulerAngles = ang;
                    ControlledCamera.transform.localRotation = o;
                    //StartCoroutine(BeginingDoorButton2.GetComponent<SCPDoor>().Open());
                    stage = 4;
                }
            }
            else if (stage == 4)
            {
                Vector3 speed = Vector3.forward * Speed;
                ControlledCamera.transform.Translate(speed * Time.deltaTime);
                CameraShake();
                if (ControlledCamera.transform.localPosition.x >= 8.5f && BeginingDoorButton2.GetComponent<SCPDoor>().JudgeWhetherOpen() == false && BeginingDoorButton2.GetComponent<SCPDoor>().isOperating == false)
                {
                    StartCoroutine(BeginingDoorButton2.GetComponent<SCPDoor>().Open());
                }
                if (ControlledCamera.transform.localPosition.x >= 16)
                {
                    StartCoroutine(BeginingDoorButton2.GetComponent<SCPDoor>().Close());
                    stage = 5;
                }
            }
            else if (stage == 5)
            {
                Vector3 speed = Vector3.forward * Speed;
                ControlledCamera.transform.Translate(speed * Time.deltaTime);
                CameraShake();
                if (ControlledCamera.transform.localPosition.x >= 30)
                {
                    StartCoroutine(BeginingDoorButton3.GetComponent<SCPDoor>().Open()); timeCounter = 0;
                    stage = 6;
                }
            }
            else if (stage == 6)
            {
                timeCounter += Time.deltaTime;
                if (timeCounter >= 1.7)
                {
                    stage = 7;
                }
            }
            else if (stage == 7)
            {
                Vector3 speed = Vector3.forward * Speed;
                ControlledCamera.transform.Translate(speed * Time.deltaTime);
                CameraShake();
                if (ControlledCamera.transform.localPosition.x >= 37)
                {
                    GameInfo.CurrentGame.notification.ShowNotification("Press W,A,S,D to move, Space to Jump, and E to operate.");
                    Guard_1.Play();
                    try
                    {
                        GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle(Site_13ToolLib.Globalization.Language.Language_Plot["BeforeCB.1"], 5f);
                    }
                    catch (System.Exception)
                    {
                        GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Guard: I think we'd better to stay here.", 5f);
                    }
                    StartCoroutine(BeginingDoorButton3.GetComponent<SCPDoor>().Close());
                    stage = 8;
                }
            }
            else if (stage == 8)
            {
                Vector3 speed = Vector3.forward * Speed;
                ControlledCamera.transform.Translate(speed * Time.deltaTime);
                CameraShake();
                if (ControlledCamera.transform.localPosition.x >= 34)
                {
                    GameInfo.CurrentGame.secondaryNotification.ShowNotification("1 Minute after Containment breach.");
                    ControlledCamera.SetActive(false);
                    RealPlayerCamera.SetActive(true);
                    stage = 9;
                    timeCounter = 0;
                    BlinkCanvas.SetActive(false);
                    //Destroy(this);
                }
            }
        }
        else if (stage == 9)
        {

            ControlledCamera.SetActive(false);
            RealPlayerCamera.SetActive(true);
            needDo = false;
        }
        else
        {
        }

    }
}
