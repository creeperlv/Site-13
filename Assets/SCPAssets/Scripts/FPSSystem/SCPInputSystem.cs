using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Site13Kernel.FPSSystem
{

    public class SCPInputSystem : MonoBehaviour
    {
        public static SCPInputSystem CurrentInputSystem;
        public List<InputControl> TargetControls;
        Dictionary<string, List<InputControl>> KeyMap = new Dictionary<string, List<InputControl>>();
        void Start()
        {
            CurrentInputSystem = this;
            for (int i = 0; i < TargetControls.Count; i++)
            {
                var item = TargetControls[i];
                item.ID = i;
                var KName = item.InputName;
                if (item.HasNegative)
                {

                    LoadKey(KName + "_POSITIVE", item);
                    LoadKey(KName + "_NEGATIVE", item);
                }
                else
                {
                    LoadKey(KName, item);
                }
            }
        }
        public float GetAxis(string InputName)
        {
            var Control = KeyMap[InputName];
            foreach (var item in Control)
            {
                if (item.IsAxis == false)
                {

                    if (Input.GetKey(item.PositiveKey)) return 1f;
                    else if (Input.GetKey(item.NegativeKey)) return 1f;
                }
                else
                {
                    return Input.GetAxis(item.AxisString)*item.Intensity;
                }
            }
            return 0;
        }
        public bool GetButtonDown(string InputName)
        {
            var Control = KeyMap[InputName];
            foreach (var item in Control)
            {
                if (Input.GetKeyDown(item.PositiveKey))
                {
                    return true;
                }
            }
            return false;
        }
        public bool GetButton(string InputName)
        {
            var Control = KeyMap[InputName];
            foreach (var item in Control)
            {
                if (Input.GetKey(item.PositiveKey))
                {
                    return true;
                }
            }
            return false;
        }
        void LoadKey(string InputName, InputControl Templete)
        {
            if (Templete.IsAxis == false)
            {

                {
                    var keyCode = Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Read("HKEY_LOCAL_MACHINE/HARDWARE/CONTROL/" + Templete.ID + "/Key");
                    KeyCode k;
                    Enum.TryParse(keyCode, out k);
                    Templete.PositiveKey = k;
                }
                if (Templete.HasNegative)
                {

                    {
                        var keyCode = Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Read("HKEY_LOCAL_MACHINE/HARDWARE/CONTROL/" + Templete.ID + "/Key_Negative");
                        KeyCode k;
                        Enum.TryParse(keyCode, out k);
                        Templete.NegativeKey = k;
                    }
                }
            }
        }
        public void SetKey(string InputName, DeviceType deviceType, KeyCode key, bool isNegative)
        {

        }

    }
    [Serializable]
    public class InputControl
    {
        public string InputName;
        public string AxisString;
        public DeviceType DeviceType;
        public bool HasNegative;
        public bool IsAxis;
        public KeyCode PositiveKey;
        public KeyCode NegativeKey;
        public float Intensity=1f;
        public int ID;
    }
    public enum DeviceType
    {
        Keyboard, GamePad
    }
}