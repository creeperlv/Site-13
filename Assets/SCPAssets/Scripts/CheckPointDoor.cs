using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{

    public class CheckPointDoor : SCPDoor
    {
        public AudioSource SirenSFX;
        public IEnumerator CoreMove()
        {
            transform.parent.GetChild(0).GetComponent<SCPDoor>().isOperating = true;
            transform.parent.GetChild(1).GetComponent<SCPDoor>().isOperating = true;
            DoorOpenAudio.Play();
            StartCoroutine(Open());
            yield return new WaitForSeconds(7f);
            SirenSFX.Play();
            yield return new WaitForSeconds(1.7f);
            DoorCloseAudio.Play();
            StartCoroutine(Close());
            yield return new WaitForSeconds(DurationTime);
            transform.parent.GetChild(0).GetComponent<SCPDoor>().isOperating = false;
            transform.parent.GetChild(1).GetComponent<SCPDoor>().isOperating = false;
            yield break;
        }
        public override IEnumerator Move()
        {
            transform.parent.GetChild(0).GetComponent<SCPDoor>().isOperating = true;
            transform.parent.GetChild(1).GetComponent<SCPDoor>().isOperating = true;
            if (IsLocked == true)
            {
                if (isLanguageSpecific == false)
                    GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle(LockMessage);
                else
                {
                    string msg = $"<color=red>Translation Error: Unable to locate '{LockMessage}'</color>";
                    try
                    {
                        msg = Site_13ToolLib.Globalization.Language.Language_Plot[LockMessage];
                    }
                    catch (System.Exception)
                    {
                    }
                    GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle(msg);
                }
                transform.parent.GetChild(0).GetComponent<SCPDoor>().isOperating = true;
                transform.parent.GetChild(1).GetComponent<SCPDoor>().isOperating = true;
            }
            else
            {

                bool willRun = false;
                switch (RequiredSecurityCredential)
                {
                    case SecurityCredential.None:
                        willRun = true;
                        break;
                    case SecurityCredential.Level3:
                        if (GameInfo.CurrentGame.HandingItem != null)
                            if (GameInfo.CurrentGame.HandingItem.SecurityClearance >= 3)
                            {
                                willRun = true;
                            }
                        break;
                    case SecurityCredential.Level4:
                        if (GameInfo.CurrentGame.HandingItem != null)
                            if (GameInfo.CurrentGame.HandingItem.SecurityClearance >= 4)
                            {
                                willRun = true;
                            }
                        break;
                    case SecurityCredential.Level5:
                        if (GameInfo.CurrentGame.HandingItem != null)
                            if (GameInfo.CurrentGame.HandingItem.SecurityClearance >= 5)
                            {
                                willRun = true;
                            }
                        break;
                    default:
                        break;
                }
                if (willRun == true)
                {
                    StartCoroutine(CoreMove());
                }
                else
                {
                    DoorUnableToOpenAudio.Play();
                    GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("You need a higher security credential.");
                    yield return new WaitForSeconds(1f);
                    transform.parent.GetChild(0).GetComponent<SCPDoor>().isOperating = false;
                    transform.parent.GetChild(1).GetComponent<SCPDoor>().isOperating = false;
                }
            }

        }
    }

}