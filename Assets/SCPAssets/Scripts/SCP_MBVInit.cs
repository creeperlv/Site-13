using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP_MBVInit : MonoBehaviour
{
    public  Animator MBV_Expand;
    void Start()
    {
        StartCoroutine(Mov());
    }
    IEnumerator Mov()
    {
        yield return new WaitForSeconds(6.5f);
        MBV_Expand.SetTrigger("Expand");
    }
}
