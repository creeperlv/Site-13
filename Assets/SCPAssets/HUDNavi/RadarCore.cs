using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HUDNavi
{
    public class RadarCore : MonoBehaviour
    {
        public static RadarCore CurrentRadar;
        public GameObject Detector;
        public List<RadarDot> dots = new List<RadarDot>();
        public float RadarRadius = 50;
        public bool isOn = true;
        public float North_Base_Angle;
        public bool ReverseHorizontalRotation = false;
        public GameObject RadarObject;
        [HideInInspector]
        public List<MotionTarget> Targets;

        /// <summary>
        /// The detection distance.
        /// </summary>
        public float Distance = 25;
        void Start()
        {
            CurrentRadar = this;
        }
        public void Add(MotionTarget target)
        {
            Targets.Add(target);
            var d = dots[target.RadarIconType].Dot;
            target.RadarPoint = GameObject.Instantiate(d, RadarObject.transform);
            target.RadarPoint.GetComponent<RadarPoint>().color = dots[target.RadarIconType].Color;
        }

        void Update()
        {
            if (isOn == true)
            {
                var rotatoin = (ReverseHorizontalRotation ? -1 : 1);
                foreach (var item in Targets)
                {
                    if (item.isActiveAndEnabled == false)
                    {
                        if (item.RadarPoint.activeSelf == true) item.RadarPoint.SetActive(false);
                        continue;
                    }
                    float _x = (item.transform.position.x - Detector.transform.position.x) / Distance;
                    float _y = (item.transform.position.z - Detector.transform.position.z) / Distance;
                    bool isMoving = item.isMoving;
                    if (item.isAlwaysDetected == true) isMoving = true;
                    if (isMoving == true)
                    {
                        if (_x * _x + _y * _y > 1)
                        {
                            if (item.ShowBeyoundBoundary == false)
                            {
                                if (item.RadarPoint.activeSelf == true) item.RadarPoint.SetActive(false);
                            }
                            else
                            {
                                if (item.RadarPoint.activeSelf == false) item.RadarPoint.SetActive(true);

                                var _len = Mathf.Sqrt(_x * _x + _y * _y);
                                var ratio = 1 / _len;
                                var rt = item.RadarPoint.transform as RectTransform;
                                rt.anchoredPosition = new Vector2(_x * RadarRadius * ratio, _y * RadarRadius * ratio);
                            }
                        }
                        else
                        {
                            if (item.RadarPoint.activeSelf == false) item.RadarPoint.SetActive(true);
                            var rt = item.RadarPoint.transform as RectTransform;
                            rt.anchoredPosition = new Vector2(_x * RadarRadius, _y * RadarRadius);
                        }
                    }
                    else
                    {
                        if (item.RadarPoint.activeSelf == true) item.RadarPoint.SetActive(false);
                    }

                }
                {
                    RadarObject.transform.localEulerAngles = new Vector3(0, 0, North_Base_Angle + (rotatoin * Detector.transform.localEulerAngles.y));
                }
            }
            else
            {
                foreach (var item in Targets)
                {
                    if (item.RadarPoint.activeSelf == true) item.RadarPoint.SetActive(false);
                }
            }
        }
    }
    [Serializable]
    public class RadarDot
    {
        public GameObject Dot;
        public Color Color;
        public int TypeID;
    }
}