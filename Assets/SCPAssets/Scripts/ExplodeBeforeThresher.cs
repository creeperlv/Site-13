using Site13Kernel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBeforeThresher : MonoBehaviour
{
    public AudioSource ExplodeSound;
    private void OnTriggerEnter(Collider other)
    {
        try
        {

            SCPEntity a = other.GetComponent<SCPEntity>();
            if (a.isPlayer == true)
            {
                GameInfo.CurrentGame.DeathText = "You are killed by oil explosion, your death body became <color=red>Elijah</color>'s food , then MTFs were unable to handle with too many powerful abnormal entities, the reality was reset at last.";
                a.Die();

                ExplodeSound.Play();
            }
        }
        catch (System.Exception)
        {
        }

    }
}
