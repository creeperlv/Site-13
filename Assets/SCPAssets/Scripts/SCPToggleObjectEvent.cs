using Site13Kernel.Convertors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public class SCPToggleObjectEvent : SCPStoryNodeBaseCode
    {
        public GameObject[] GameObjects;
        public List<Flag> Flags;
        [System.Serializable]
        public class Flag
        {
            public string ID;
            public BaseConverter Converter;
        }
        public override void StartStory()
        {
            if (isStarted == false)
            {

                foreach (var item in GameObjects)
                {
                    item.gameObject.SetActive(!item.activeSelf);
                }
                foreach (var item in Flags)
                {
                    switch (item.Converter.Converter)
                    {
                        case Converters.Bool:
                            {
                                BoolConverter RealConverter = new BoolConverter();
                                RealConverter.Parameters = item.Converter.Parameters;
                                RealConverter.Apply(item.ID);
                            }
                            break;
                        case Converters.String:
                            {
                                StringConverter RealConverter = new StringConverter();
                                RealConverter.Parameters = item.Converter.Parameters;
                                RealConverter.Apply(item.ID);
                            }
                            break;
                        case Converters.Int:
                            {

                                {
                                    IntConverter RealConverter = new IntConverter();
                                    RealConverter.Parameters = item.Converter.Parameters;
                                    RealConverter.Apply(item.ID);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            isStarted = true;
        }
    }
}
namespace Site13Kernel.Convertors
{
    [System.Serializable]
    public class BaseConverter
    {
        public Converters Converter;
        public List<string> Parameters;
        public virtual void Apply(string id) { }
    }
    public enum Converters
    {
        Bool,
        String,Int
    }
    [System.Serializable]
    public class BoolConverter:BaseConverter
    {
        public override void Apply(string id)
        {
            bool value = true;
            if (id.ToUpper() == "Reverse".ToUpper())
            {
                if (GameInfo.CurrentGame.FlagsGroup.ContainsKey(id))
                {
                    value = !bool.Parse(GameInfo.CurrentGame.FlagsGroup[id]);
                }
                else value = false;
            }
            else
            {
                bool.TryParse(Parameters[0], out value);
            }

            if (!GameInfo.CurrentGame.FlagsGroup.ContainsKey(id)) GameInfo.CurrentGame.FlagsGroup.Add(id,value.ToString());
            GameInfo.CurrentGame.FlagsGroup[id] = value.ToString();
        }
    }
    [System.Serializable]
    public class StringConverter:BaseConverter
    {
        public override void Apply(string id)
        {
            if (!GameInfo.CurrentGame.FlagsGroup.ContainsKey(id)) GameInfo.CurrentGame.FlagsGroup.Add(id,Parameters[0]);
            GameInfo.CurrentGame.FlagsGroup[id] = Parameters[0];
        }
    }
    [System.Serializable]
    public class IntConverter:BaseConverter
    {
        public override void Apply(string id)
        {
            int value = 0;
            if (GameInfo.CurrentGame.FlagsGroup.ContainsKey(id)) int.TryParse(GameInfo.CurrentGame.FlagsGroup[id], out value);
            switch (Parameters[0].ToUpper())
            {
                case "++":
                    value++;
                    break;
                case "--":
                    value--;
                    break;
                case "STEPADD":
                    try
                    {
                        value += int.Parse(Parameters[1]);
                    }
                    catch{}
                    break;
                case "STEPSUB":
                    try
                    {
                        value -= int.Parse(Parameters[1]);
                    }
                    catch{}
                    break;
                case "STEPPRO":
                    try
                    {
                        value *= int.Parse(Parameters[1]);
                    }
                    catch{}
                    break;
                default:
                    int.TryParse(Parameters[0], out value);
                    break;
            }

            if (!GameInfo.CurrentGame.FlagsGroup.ContainsKey(id)) GameInfo.CurrentGame.FlagsGroup.Add(id,value.ToString());
            GameInfo.CurrentGame.FlagsGroup[id] = value.ToString();
            base.Apply(id);
        }
    }
}