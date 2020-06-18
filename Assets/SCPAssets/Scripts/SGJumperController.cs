using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public class SGJumperController : SCPInteractive
    {
        public GameObject Tip;
        public override IEnumerator Move()
        {
            isOperating = true;
            Tip.SetActive(true);
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(1);
            Tip.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            isOperating = false;
        }
    }
}