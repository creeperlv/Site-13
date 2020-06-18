using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{

    public class SupplyBox : SCPInteractive
    {
        public Transform Containments;
        public override IEnumerator Move()
        {
            isOperating = true;
            for (int i = 0; i < Containments.childCount; i++)
            {
                Containments.GetChild(i).gameObject.SetActive(false);
            }
            {
                //Take Away Player's valuable things.
                int RandomID = Random.Range(0, (int)6);
                switch (RandomID)
                {
                    case 0:
                        {
                            if (GameInfo.CurrentGame.PlayerHealth.CurrentHealth - 50 <= 0)
                            {
                                GameInfo.CurrentGame.DeathText = "You are dead because wasting both of your livers.";
                            }
                            GameInfo.CurrentGame.PlayerHealth.ChangeHealth(-50);
                        }
                        break;
                    case 2:
                        {
                            if (GameInfo.CurrentGame.LeftAmmos[0] - 20 > 0)
                            {
                                GameInfo.CurrentGame.LeftAmmos[0] -= 20;
                            }
                            else
                            {
                                GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("God, I have no enough bullets.");
                                yield return new WaitForSeconds(0.5f);
                                isOperating = false;
                                yield break;
                            }
                        }
                        break;
                    case 3:
                        {

                            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("I won't give out LR-SP-20! Never!");
                            yield return new WaitForSeconds(0.5f);
                            isOperating = false;
                            yield break;
                        }
                        break;
                    case 1:
                        {
                            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("I will not use my keycard.");
                            yield return new WaitForSeconds(0.5f);
                            isOperating = false;
                            yield break;
                        }
                        break;
                    case 4:
                        {
                            if (GameInfo.CurrentGame.PossessingFAK- 1 > 0)
                            {
                                GameInfo.CurrentGame.PossessingFAK -= 1;
                            }
                            else
                            {
                                GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Oh, I don't have any more First Aid Kit.");
                                yield return new WaitForSeconds(0.5f);
                                isOperating = false;
                                yield break;
                            }
                        }
                        break;
                    case 5:
                        {
                            if (GameInfo.CurrentGame.Bats- 1 > 0)
                            {
                                GameInfo.CurrentGame.Bats -= 1;
                            }
                            else
                            {
                                GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Danm! I have no more batteries!");
                                yield return new WaitForSeconds(0.5f);
                                isOperating = false;
                                yield break;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            Containments.GetChild(Random.Range(0, Containments.childCount)).gameObject.SetActive(true);
            this.GetComponent<AudioSource>().Play();
            transform.parent.parent.GetComponent<Animator>().SetTrigger("Europe");
            yield return new WaitForSeconds(10);
            //transform.parent.parent.GetComponent<Animator>().SetTrigger()
            isOperating = false;
            yield break;
        }
    }

}