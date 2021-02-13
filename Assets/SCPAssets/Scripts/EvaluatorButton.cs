using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Site13Kernel
{
    public class EvaluatorButton : SCPDoor
    {
        public SCPDoor TargetDoor;
        float DeltaY = 0;
        float DeltaX = 0;
        float DeltaZ = 0;
        //        public GameObject Player;
        public AudioSource MOV;
        public bool isInSiteEvaluator = false;
        public int TargetScene;
        [Tooltip("0 for Up, 1 fot down, 2 for error.")]
        public int Direction = 0;
        public Text CentralHintText;
        [Multiline]
        public string HintText = "";
        public Image UpIcon;
        public Image DownIcon;
        public Image HighlightIcon;
        public Image ErrorIcon;
        public string StateOverFlag;
        [Tooltip("Toogle entities to prevent some problems")]
        public GameObject EntityGroup;
        public bool isInsideDoor = false;
        public IEnumerator ShowHighLight()
        {
            HighlightIcon.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            HighlightIcon.gameObject.SetActive(false);

        }
        public override IEnumerator Move()
        {
            try
            {

                transform.parent.GetChild(0).GetComponent<SCPDoor>().isOperating = true;
                transform.parent.GetChild(1).GetComponent<SCPDoor>().isOperating = true;
            }
            catch (System.Exception)
            {

            }
            if (isInSiteEvaluator == true)
            {
                if (JudgeWhetherOpen() == true)
                {
                    StartCoroutine(Close());
                    yield return new WaitForSeconds(DurationTime);
                    MOV.Play();
                    var detector = transform.parent.Find("EvaluatorEntityDetector").GetComponent<SCPEvaluatorDetector>();
                    DeltaX = detector.DeltaX;
                    DeltaY = detector.DeltaY;
                    DeltaZ = detector.DeltaZ;
                    List<int> ids = new List<int>();
                    List<GameObject> ToTransfer = new List<GameObject>();
                    //Find All Entities;
                    foreach (var item in detector.TargetEntities)
                    {
                        if (ids.IndexOf(item.GetInstanceID()) >= 0)
                        {

                        }
                        else
                        {
                            ids.Add(item.GetInstanceID());
                            ToTransfer.Add(item.gameObject);
                        }
                    }
                    yield return new WaitForSeconds(10f);
                    foreach (var item in ToTransfer)
                    {
                        Debug.Log("Transfer:" + item.name);
                        var lp = item.transform.position;
                        lp.x += DeltaX;
                        lp.y += DeltaY;
                        lp.z += DeltaZ;
                        item.transform.position = lp;

                    }
                    StartCoroutine(TargetDoor.Open());
                    Debug.Log(">>Tried to open target door.");
                }
                else
                {
                    if (isInsideDoor)
                    {
                        if (JudgeWhetherOpen() == false)
                            StartCoroutine(Open());

                        try
                        {

                            transform.parent.GetChild(0).GetComponent<SCPDoor>().isOperating = false;
                            transform.parent.GetChild(1).GetComponent<SCPDoor>().isOperating = false;
                        }
                        catch (System.Exception)
                        {
                        }
                        yield break;
                    }
                    else
                        StartCoroutine(TargetDoor.Close());
                    yield return new WaitForSeconds(DurationTime);
                    MOV.Play();
                    var detector = transform.parent.Find("EvaluatorEntityDetector").GetComponent<SCPEvaluatorDetector>();
                    DeltaX = detector.DeltaX;
                    DeltaY = detector.DeltaY;
                    DeltaZ = detector.DeltaZ;
                    List<int> ids = new List<int>();
                    List<GameObject> ToTransfer = new List<GameObject>();
                    //Find All Entities;
                    foreach (var item in detector.TargetEntities)
                    {
                        if (ids.IndexOf(item.GetInstanceID()) >= 0)
                        {

                        }
                        else
                        {
                            Debug.Log("Transfer:" + item.name);
                            ids.Add(item.GetInstanceID());
                            ToTransfer.Add(item.gameObject);
                        }
                    }
                    yield return new WaitForSeconds(10f);
                    foreach (var item in ToTransfer)
                    {
                        var lp = item.transform.position;
                        lp.x += DeltaX;
                        lp.y += DeltaY;
                        lp.z += DeltaZ;
                        item.transform.position = lp;

                    }
                    if (isInsideDoor)
                    {
                        if (TargetDoor.JudgeWhetherOpen() == false)
                            StartCoroutine(TargetDoor.Open());

                    }
                    else
                    {
                            StartCoroutine(Open());
                    }
                    Debug.Log(">>Tried to open target door.");
                }
            }
            else
            {

                isOperating = true;
                UpIcon.gameObject.SetActive(false);
                DownIcon.gameObject.SetActive(false);
                ErrorIcon.gameObject.SetActive(false);
                bool flag = false;
                if (Direction == 2)
                {
                    flag = true;
                }
                if (StateOverFlag != "")
                {
                    try
                    {

                        if (GameInfo.CurrentGame.FlagsGroup[StateOverFlag] != "Enabled")
                        {
                        }
                    }
                    catch (System.Exception)
                    {
                        flag = true;
                    }
                }
                if (flag == true)
                {
                    StartCoroutine(ShowHighLight());
                    CentralHintText.text = HintText;
                    DoorUnableToOpenAudio.Play();
                    ErrorIcon.gameObject.SetActive(true);
                }
                else
                {
                    {
                        var button =
                    this.transform.parent.GetComponentsInChildren<EvaluatorButton>();
                        foreach (var item in button)
                        {

                            item.isOperating = true;
                        }
                    }
                    {
                        HighlightIcon.gameObject.SetActive(true);
                        if (Direction == 0)
                        {
                            UpIcon.gameObject.SetActive(true);
                        }
                        else if (Direction == 1)
                        {
                            DownIcon.gameObject.SetActive(true);
                        }
                        CentralHintText.gameObject.SetActive(false);
                        StartCoroutine(Close());
                        yield return new WaitForSeconds(1f);
                        if (EntityGroup != null)
                            EntityGroup.SetActive(false);
                        MOV.Play();
                        bool isInside = false;
                        var detector = transform.parent.parent.parent.Find("EvaluatorEntityDetector").GetComponent<SCPEvaluatorDetector>();
                        foreach (var item in detector.TargetEntities)
                        {
                            if (item.GetComponent<SCPFirstController>() != null)
                            {
                                isInside = true;
                            }
                        }
                        if (isInside == true)
                        {
                            GameInfo.CurrentGame.EnterSource = GameInfo.EnterSourceType.Lift;
                            GameInfo.CurrentGame.saveControlProtocolMode = GameInfo.SaveControlProtocolMode.Enter;
                        }
                        yield return new WaitForSeconds(9f);
                        isOperating = false;
                        if (isInside == true)
                        {
                            GameInfo.CurrentGame.isCurrentArrived = false;
                            GameInfo.CurrentGame.CurrentSceneSaveSystem.Save();
                            GameInfo.CurrentGame.EnterSource = GameInfo.EnterSourceType.Lift;
                            GameInfo.CurrentGame.saveControlProtocolMode = GameInfo.SaveControlProtocolMode.Enter;
                            GameInfo.CurrentGame.NextScene = TargetScene;
                            SceneManager.LoadScene(2);
                        }
                    }
                }
            }
            /**
            isOperating = false;
            DoorOpenAudio.Play();
            StartCoroutine(Open());
            yield return new WaitForSeconds(5f);
            DoorCloseAudio.Play();
            StartCoroutine(Close());
            yield return new WaitForSeconds(DurationTime);
    **/
            try
            {

                transform.parent.GetChild(0).GetComponent<SCPDoor>().isOperating = false;
                transform.parent.GetChild(1).GetComponent<SCPDoor>().isOperating = false;
            }
            catch (System.Exception)
            {
            }
            yield break;
        }
    }
}
