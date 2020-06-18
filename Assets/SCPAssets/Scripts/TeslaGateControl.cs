using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{

    public class TeslaGateControl : MonoBehaviour
    {
        bool isOperating = false;
        public bool isDetectPlayer = false;
        public ControlMode controlMode = ControlMode.Detect;
        public GameObject Lighting;
        public GameObject KillArea;
        public AudioSource TeslaSFX;
        public enum ControlMode
        {
            Kill, Detect
        }
        bool isEntityIn = false;
        private void Update()
        {
            if (controlMode == ControlMode.Detect)
            {
                bool willdo = false;

                foreach (var item in entities)
                {
                    try
                    {

                        if (isDetectPlayer == true)
                        {
                            willdo = true;
                        }
                        if (item == null)
                        {

                        }else if (item.gameObject.activeSelf == false)
                        {

                        }else
                        if (item.isPlayer == false)
                        {
                            willdo = true;
                        }
                    }
                    catch (System.Exception)
                    {
                    }
                }
                if (willdo==true)
                {
                    StartStory();
                }
            }else if(controlMode== ControlMode.Kill)
            {
                foreach (var item in entities)
                {
                    if (item.isPlayer == true)
                    {
                        GameInfo.CurrentGame.DeathText = "You are killed by Tesla Gate.";
                    }
                    item.ChangeHealth(-item.MaxHealth);
                }
            }
        }
        List<SCPEntity> entities = new List<SCPEntity>();
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<SCPEntity>() != null)
            {
                entities.Remove(other.GetComponent<SCPEntity>());
                isEntityIn = false;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<SCPEntity>() != null)
            {
                entities.Add(other.GetComponent<SCPEntity>());
                isEntityIn = true;
            }
        }
        public void StartStory()
        {
            if (isOperating == false)
            {
                StartCoroutine(Splash());
            }
        }
        IEnumerator Splash()
        {
            isOperating = true;
            TeslaSFX.Play();
            yield return new WaitForSeconds(2.24f);
            Lighting.SetActive(true);
            KillArea.SetActive(true);
            yield return new WaitForSeconds(1f);
            Lighting.SetActive(false);
            KillArea.SetActive(false);
            yield return new WaitForSeconds(.5f);
            isOperating = false;
            yield break;
        }
    }

}