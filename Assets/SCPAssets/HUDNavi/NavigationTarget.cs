using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HUDNavi
{
    public class NavigationTarget : MonoBehaviour
    {
        [HideInInspector]
        public NavigationPoint MappedHUDPoint = null;
        [HideInInspector]
        public NavigationPoint MappedHUDArrow = null;
        bool isAdded = false;
        public bool Show = true;
        public string label;
        public bool ShowDistance;
        public string TargetNavigationPointType;
        public bool WillShowOffScreenPoint = false;
        // Update is called once per frame
        void Update()
        {
            if (isAdded == false)
            {
                if (NavigationCore.CurrentCore != null)
                {
                    NavigationCore.CurrentCore.Targets.Add(this);
                    isAdded = true;
                }
            }
        }
        private void OnDestroy()
        {
            NavigationCore.CurrentCore.Targets.Remove(this);
            if (MappedHUDArrow != null) GameObject.Destroy(MappedHUDArrow.gameObject);
            if (MappedHUDPoint != null) GameObject.Destroy(MappedHUDPoint.gameObject);
        }
        public void UpdateLabel(string NewLabel)
        {
            label = NewLabel;
            if (MappedHUDPoint != null)
                MappedHUDPoint.Label.text = label;
            if (MappedHUDArrow != null) MappedHUDArrow.Label.text = label;
        }
    }

}