using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{

    public class FiredFloor : MonoBehaviour
    {
        public float HPPS = 10.0f;
        public string DeathMessage = "Death from burn.";
        bool isEntered = false;
        Collider coll;
        public string IgnoreEnemyGroup;
        void Update()
        {
            if (isEntered == true)
            {
                try
                {
                    if (coll.GetComponent<SCPEntity>().CurrentHealth - HPPS * Time.deltaTime <= 0)
                    {
                        GameInfo.CurrentGame.DeathText = DeathMessage;
                    }
                    if (coll.GetComponent<SCPEnemy>() != null)
                    {
                        var ene = coll.GetComponent<SCPEnemy>();
                        foreach (var item in realGroup)
                        {
                            if (ene.ActionGroup.ToUpper() == item.ToUpper())
                            {
                                return;
                            }
                        }
                    }
                    coll.GetComponent<SCPEntity>().ChangeHealth(-HPPS * Time.deltaTime);
                }
                catch (System.Exception)
                {
                }
            }
        }
        string[] realGroup;
        private void Start()
        {
            try
            {

                realGroup = IgnoreEnemyGroup.Split(',');
            }
            catch (System.Exception)
            {
                realGroup =new string[0];
            }
        }
        void OnTriggerEnter(Collider coll)
        {
            {
                this.coll = coll;
                isEntered = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            isEntered = false;
        }
    }

}