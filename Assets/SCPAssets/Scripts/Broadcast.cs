using Site13Kernel;
using Site_13ToolLib.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Broadcast : MonoBehaviour
{
    public AudioSource BroadcastSource;
    public AudioSource BroadcastSource2;
    public AudioClip O79InCharge;
    public SCPDoor InESDoor;
    public Text ESTip;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(aaaa());
    }
    IEnumerator aaaa()
    {
        yield return new WaitForSeconds(0.2f);
        try
        {

            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle(Language.Language_Plot["Announc.1"]);
        }
        catch (System.Exception)
        {

            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Announcement: This site is suffering multiple dangerous containments breah!",4f);
        }
        yield return new WaitForSeconds(4.2f);
        try
        {

            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle(Language.Language_Plot["Announc.2"]);
        }
        catch (System.Exception)
        {

            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Announcement: All personnel are required to stay in the nearest evacuation shelter or any other safe area!", 6f);
        }
        yield return new WaitForSeconds(9);
        BroadcastSource.clip = O79InCharge;
        BroadcastSource.Play();
        try
        {

            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle(Language.Language_Plot["Announc.3"]);
        }
        catch (System.Exception)
        {

            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Announcement: According to security protocol, SCP-079 will in charge of this facility!", 4f);
        }
        yield return new WaitForSeconds(3.5f);
        try
        {

            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle(Language.Language_Plot["Announc.4"]);
        }
        catch (System.Exception)
        {

            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Announcement: All personnel are adviced to follow his instructions, he will try his best to keep you alive.",6f);

        }
        yield return new WaitForSeconds(12.5f);
        BroadcastSource2.Play();
        try
        {

            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle(Language.Language_Plot["SCP079.1"]);
        }
        catch (System.Exception)
        {

            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("SCP-079: Please out of this shelter immediately.",7f);
        }
        yield return new WaitForSeconds(8.1f);
        ESTip.text = "!DANGER!";
        StartCoroutine(InESDoor.Open());
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
