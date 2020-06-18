using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{

    public class SCPResBox : SCPInteractive
    {
        int Index = 0;
        public GameObject AugustAmmo;
        public GameObject P90Ammo;
        public GameObject FirstAidKit;
        // Start is called before the first frame update
        void Start()
        {
            int id=this.GetInstanceID();
            System.Random r = new System.Random(id);
            Index=r.Next(0, 100);
            AugustAmmo.SetActive(false);
            P90Ammo.SetActive(false);
            FirstAidKit.SetActive(false);
            if (Index < 30)
            {
                Index = 0;
            }else if (Index < 50)
            {
                Index = 1;
            }else if (Index < 70)
            {
                Index = 2;
            }else
            {
                Index = 3;
            }
            switch (Index)
            {
                case 0:
                    break;
                case 1:
                    AugustAmmo.SetActive(true);
                    break;
                case 2:
                    P90Ammo.SetActive(true);
                    break;
                case 3:
                    FirstAidKit.SetActive(true);
                    break;
                default:
                    break;
            }
        }
        public override IEnumerator Move()
        {
            switch (Index)
            {
                case 1:
                    {
                        string ID = "AUGUST";
                        if (GameInfo.CurrentGame.FlagsGroup.ContainsKey(ID))
                        {
                            GameInfo.CurrentGame.FlagsGroup[ID + "_Total"] = ""+((int.Parse(GameInfo.CurrentGame.FlagsGroup[ID + "_Total"])) + 30);
                        }
                        else
                        {
                            GameInfo.CurrentGame.FlagsGroup.Add(ID, 30 + "");
                            GameInfo.CurrentGame.FlagsGroup.Add(ID + "_Total", 30 * 4+ "");
                        }
                    }
                    break;
                case 2:
                    {
                        string ID = "P90";
                        if (GameInfo.CurrentGame.FlagsGroup.ContainsKey(ID))
                        {
                            GameInfo.CurrentGame.FlagsGroup[ID + "_Total"] = "" + ((int.Parse(GameInfo.CurrentGame.FlagsGroup[ID + "_Total"])) + 60);
                        }
                        else
                        {
                            GameInfo.CurrentGame.FlagsGroup.Add(ID, 60 + "");
                            GameInfo.CurrentGame.FlagsGroup.Add(ID + "_Total", 60* 4+ "");
                        }
                    }
                    break;
                case 3:
                    GameInfo.CurrentGame.PossessingFAK += 1;
                    break;
                default:
                    break;
            }
            this.gameObject.SetActive(false);
            return base.Move();
        }
    }

}