using Site13Kernel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSFXPlayer : MonoBehaviour
{
    [HideInInspector] public bool isPlayed = false;
    public AudioSource ArriveSFX;
    private void OnTriggerEnter(Collider other)
    {
        if (isPlayed == false)
        {
            if (other.GetComponent<SCPFirstController>() != null)
            {
                isPlayed = true ;
                ArriveSFX.Play();
            }
        }
    }
}
