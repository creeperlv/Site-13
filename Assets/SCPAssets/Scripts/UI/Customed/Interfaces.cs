using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.UI
{
    public interface IVisualElement
    {
        void SetProperty(string Key,object Value);
        void SetProperty(string Key,string Value);
        Property GetProperty(string Key);
        object GetPropertyValue(string Key);
        void Show();
        void Hide();
    }
    public class Property
    {
        public string Key;
        public object Value;
    }
}