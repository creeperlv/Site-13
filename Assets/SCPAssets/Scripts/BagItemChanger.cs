using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public class BagItemChanger : SCPInteractive
    {
        public BagItemType itemType;
        public string ID;
        public override IEnumerator Move()
        {
            isOperating = true;
            string BagOwner = GameInfo.CurrentGame.FirstPerson.BagNameAlias;
            switch (itemType)
            {
                case BagItemType.Primary:
                    {
                        if (GameInfo.CurrentGame.FlagsGroup.ContainsKey($"BAG.{BagOwner}.1"))
                        {
                            GameInfo.CurrentGame.FlagsGroup[$"BAG.{BagOwner}.1"]= ID;
                        }
                        else
                        GameInfo.CurrentGame.FlagsGroup.Add($"BAG.{BagOwner}.1", ID);
                    }
                    break;
                case BagItemType.Secondary:
                    {
                        if (GameInfo.CurrentGame.FlagsGroup.ContainsKey($"BAG.{BagOwner}.2"))
                        {
                            GameInfo.CurrentGame.FlagsGroup[$"BAG.{BagOwner}.2"] = ID;
                        }
                        else
                            GameInfo.CurrentGame.FlagsGroup.Add($"BAG.{BagOwner}.2", ID);
                    }
                    break;
                case BagItemType.Keycard:
                    {
                        GameInfo.CurrentGame.FlagsGroup.Add($"BAG.{BagOwner}.3", ID);
                    }
                    break;
                case BagItemType.FirstAidKit:
                    break;
                case BagItemType.MobilePhone:
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(0.5f);
            isOperating = false;
        }
    }

}