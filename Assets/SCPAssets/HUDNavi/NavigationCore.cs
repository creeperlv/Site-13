using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace HUDNavi
{
    public class NavigationCore : MonoBehaviour
    {
        public static NavigationCore CurrentCore;
        public List<NavigationTarget> Targets = new List<NavigationTarget>();
        public Camera TargetCamera;
        public List<PresetNaviPoint> OnScreenPresets;
        public List<PresetNaviPoint> OffScreenPresets;
        public GameObject HUDPointsHolder;
        public int DistancePrecision = 0;
        public float DistanceIntensity = 1;
        public string DistanceSI = "M";
        [Serializable]
        public class PresetNaviPoint
        {
            public string ID;
            public NavigationPoint Point;
        }
        void Start()
        {
            CurrentCore = this;
        }
        public static Vector3 MapToCam(Camera camera, Vector3 Target)
        {
            return camera.WorldToViewportPoint(Target);
        }
        public static bool IsMappedObjVisible(Vector3 Target)
        {
            return Target.z > 0 && Target.x > 0 && Target.x < 1 && Target.y > 0 && Target.y < 1;
        }
        public void UpdateDistance(NavigationTarget target, NavigationPoint point)
        {
            if (point.Distance.text == null) return;
            float l = (target.transform.position - TargetCamera.transform.position).magnitude * DistanceIntensity;
            point.Distance.text = $"{l.ToString($"f{DistancePrecision}")} {DistanceSI}";
        }
        void Update()
        {
            foreach (var item in Targets)
            {
                if (item.MappedHUDPoint == null)
                {
                    NavigationPoint Preferred = OnScreenPresets.First().Point;
                    foreach (var Point in OnScreenPresets)
                    {
                        if (Point.ID == item.TargetNavigationPointType)
                        {
                            Preferred = Point.Point;
                        }
                    }
                    item.MappedHUDPoint = GameObject.Instantiate(Preferred, HUDPointsHolder.transform).GetComponent<NavigationPoint>();
                    if (item.MappedHUDPoint.Label != null)
                        item.MappedHUDPoint.Label.text = item.label;
                }
                if (item.MappedHUDArrow == null)
                {
                    NavigationPoint Preferred = OffScreenPresets.First().Point;
                    foreach (var Point in OffScreenPresets)
                    {
                        if (Point.ID == item.TargetNavigationPointType)
                        {
                            Preferred = Point.Point;
                        }
                    }
                    item.MappedHUDArrow = GameObject.Instantiate(Preferred, HUDPointsHolder.transform).GetComponent<NavigationPoint>();
                    if (item.MappedHUDPoint.Label != null)
                        item.MappedHUDArrow.Label.text = item.label;
                }
                if (item.Show == false)
                {
                    if (item.MappedHUDPoint.gameObject.activeSelf == true)
                        item.MappedHUDPoint.gameObject.SetActive(false);
                    if (item.MappedHUDArrow.gameObject.activeSelf == true)
                        item.MappedHUDArrow.gameObject.SetActive(false);
                    continue;
                }
                else
                {
                    var v = MapToCam(TargetCamera, item.transform.position);
                    if (IsMappedObjVisible(v))
                    {
                        if (item.MappedHUDPoint.gameObject.activeSelf == false)
                            item.MappedHUDPoint.gameObject.SetActive(true);
                        if (item.MappedHUDArrow.gameObject.activeSelf == true)
                            item.MappedHUDArrow.gameObject.SetActive(false);
                        var t = HUDPointsHolder.transform as RectTransform;
                        v.Scale(new Vector3(t.rect.width, t.rect.height, 0));
                        item.MappedHUDPoint.transform.position = v;
                        if (item.ShowDistance)
                        {
                            //Deal with label and distance.
                            UpdateDistance(item, item.MappedHUDPoint);
                        }
                        else
                        {
                            if (item.MappedHUDPoint.Distance.gameObject.activeSelf) item.MappedHUDPoint.Distance.gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        if (item.MappedHUDPoint.gameObject.activeSelf == true)
                            item.MappedHUDPoint.gameObject.SetActive(false);
                        if (item.MappedHUDArrow.gameObject.activeSelf == false&&item.WillShowOffScreenPoint==true)
                            item.MappedHUDArrow.gameObject.SetActive(true);

                        {
                            var t = HUDPointsHolder.transform as RectTransform;
                            var HolderW = t.rect.width;
                            var HolderH = t.rect.height;
                            var location = v;
                            if (location.z < 0) location *= -1;

                            var RelateSystem2 = new Vector3(.5f, .5f, 0);//Center of the screen.
                            location -= RelateSystem2;//Convert to relate to center of the screen.
                            float Angle = Mathf.Atan2(location.y, location.x);//Angle of center.
                            {
                                var r = item.MappedHUDArrow.Icon.transform.rotation;
                                r.eulerAngles = new Vector3(0, 0, Angle*Mathf.Rad2Deg);
                                item.MappedHUDArrow.Icon.transform.rotation = r;
                            }
                            Vector3 ActualPosition;
                            if (location.x > 0)
                            {
                                ActualPosition = new Vector3(RelateSystem2.x, location.y, 0);
                            }
                            else
                            {
                                ActualPosition = new Vector3(-RelateSystem2.x, location.y, 0);
                            }
                            if (location.y > RelateSystem2.y)
                            {
                                ActualPosition = new Vector3(location.x, RelateSystem2.y, 0);
                            }
                            else if (location.y < -RelateSystem2.y)
                            {
                                ActualPosition = new Vector3(location.x, -RelateSystem2.y, 0);
                            }
                            ActualPosition += RelateSystem2;
                            ActualPosition.Scale(new Vector3(HolderW, HolderH, 0));
                            ActualPosition.x = Mathf.Clamp(ActualPosition.x, 100, HolderW - 100);
                            ActualPosition.y = Mathf.Clamp(ActualPosition.y, 50, HolderH - 50);
                            item.MappedHUDArrow.transform.position = ActualPosition;

                        }
                        if (item.ShowDistance)
                        {
                            //Deal with label and distance.
                            UpdateDistance(item, item.MappedHUDArrow);
                        }
                        else
                        {
                            if (item.MappedHUDArrow.Distance.gameObject.activeSelf) item.MappedHUDArrow.Distance.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }

}