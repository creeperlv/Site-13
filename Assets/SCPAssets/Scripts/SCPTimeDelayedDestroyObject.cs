using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCPTimeDelayedDestroyObject : MonoBehaviour
{
    public float Duration = 2f;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Object initialized.");
        StartCoroutine(StartDestroy());
    }

    private IEnumerator StartDestroy()
    {
        yield return new WaitForSeconds(Duration);
        GameObject.Destroy(this.gameObject);
        //Debug.Log("Object destroyed.");
        yield break;
    }

}
