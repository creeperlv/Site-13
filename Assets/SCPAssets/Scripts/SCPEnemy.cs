using Site13Kernel.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Site13Kernel
{
    public class SCPEnemy : SCPEntity
    {
        public GameObject Player;
        public GenericWeapon weapon;
        public float AttackDistance = 10f;
        public float AttackDamageIfWeaponIsNull = -20f;
        public float AttackInterval = 0.4f;
        public Animator DefAnime;
        public string IDLEAnime;
        public string MovingAnime;
        public string AttackAnime;
        [HideInInspector]
        public string CurrentState;
        public bool isAttackArea = false;
        public float AttackAnimeLength = 0;
        public float DetectDistence = 0;
        public string ActionGroup = "DEF";
        public string DeathMessage = "You are killed by unknown.";
        void DetectPlayer()
        {
            if (isAttackArea == false)
            {

                if (Vector3.Distance(Player.transform.position, this.transform.position) < AttackDistance)
                {
                    if (weapon == null)
                    {
                        Debug.Log("Attack!");
                        if (Player.GetComponent<SCPEntity>().CurrentHealth + AttackDamageIfWeaponIsNull <= 0)
                        {
                            GameInfo.CurrentGame.DeathText = DeathMessage;
                        }
                        Player.GetComponent<SCPEntity>().ChangeHealth(AttackDamageIfWeaponIsNull);
                    }
                    else
                    {
                        weapon.FirePrimary();
                    }
                }
            }
            else
            {
                if (Vector3.Distance(Player.transform.position, this.transform.position) < AttackDistance)
                {

                    DefAnime.SetBool(MovingAnime, false);
                    DefAnime.SetBool(AttackAnime, true);

                    //StartCoroutine(WaitTillMove());
                }
                else
                {

                    DefAnime.SetBool(MovingAnime, true);
                    DefAnime.SetBool(AttackAnime, false);
                }
            }
        }
        bool wait = false;
        float timed = 0;
        bool isStop = false;
        void Update()
        {
            if (isAttackArea == false)
            {
                try
                {

                    if (wait == false)
                    {
                        isStop = false;
                        if (Vector3.Distance(this.transform.position, Player.transform.position) >= DetectDistence && DetectDistence != 0)
                        {

                            isStop = true;
                            //DefAnime.SetBool(MovingAnime, false);
                            //DefAnime.SetBool(AttackAnime, false);
                        }
                        else
                        {
                            isStop = false;
                        }
                        try
                        {
                            string status = GameInfo.CurrentGame.FlagsGroup[ActionGroup];
                            if (status == "Disabled")
                            {
                                isStop = true;

                                //DefAnime.SetBool(MovingAnime, false);
                                //DefAnime.SetBool(AttackAnime, false);
                            }
                            else isStop = false;
                        }
                        catch (System.Exception)
                        {
                        }
                        if (isStop == false)
                        {
                            if (Player != null)
                            {
                                timed += Time.deltaTime;
                                if (timed > AttackInterval)
                                {

                                    try
                                    {

                                        GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(Player.transform.position);
                                        GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
                                    }
                                    catch (System.Exception)
                                    {
                                    }
                                    DetectPlayer();
                                    timed = 0;
                                }
                            }
                            else
                            {
                                GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(new Vector3(0, 0, 0));
                            }
                        }
                        else
                        {
                            GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
                        }
                    }
                }
                catch (System.Exception)
                {
                }
            }
            else
            {
                Update2();
            }
        }
        void Update2()
        {
            try
            {

                if (Vector3.Distance(this.transform.position, Player.transform.position) >= DetectDistence && DetectDistence != 0)
                {

                    DefAnime.SetBool(MovingAnime, false);
                    DefAnime.SetBool(AttackAnime, false);
                    GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
                }
                else
                {
                    isStop = false;
                    try
                    {
                        string status = GameInfo.CurrentGame.FlagsGroup[ActionGroup];
                        if (status == "Disabled")
                        {
                            isStop = true;

                            DefAnime.SetBool(MovingAnime, false);
                            DefAnime.SetBool(AttackAnime, false);
                            GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
                        }
                        else isStop = false;
                    }
                    catch (System.Exception)
                    {
                    }
                    if (isStop == false)
                    {
                        if (Vector3.Distance(Player.transform.position, this.transform.position) < AttackDistance)
                        {
                            GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
                            //GetComponent<UnityEngine.AI.NavMeshAgent>().St
                            DefAnime.SetBool(MovingAnime, false);
                            DefAnime.SetBool(AttackAnime, true);
                        }
                        else
                        {
                            timed += Time.deltaTime;
                            if (timed > AttackInterval)
                            {

                                try
                                {

                                    GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(Player.transform.position);
                                }
                                catch (System.Exception)
                                {
                                }
                                timed = 0;
                            }
                            GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
                            DefAnime.SetBool(MovingAnime, true);
                            DefAnime.SetBool(AttackAnime, false);
                        }
                    }
                }
            }
            catch (System.Exception)
            {
            }
        }
    }

}