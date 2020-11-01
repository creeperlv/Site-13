using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{

    public class SCPCamWalkShakeSimluate : MonoBehaviour
    {
        [Header("0 means stops shake.")]
        public float DeltaTimeIntensity = 0;
        public float BaseY;
        public float Intensity;
        float Cycle;

        void Start()
        {

        }

        void Update()
        {
            if (DeltaTimeIntensity == 0) return;
            Cycle += Time.deltaTime * DeltaTimeIntensity;
            var lp = transform.localPosition;
            lp.y = BaseY + Mathf.Sin(Cycle) * Intensity;
            transform.localPosition = lp;
        }
    }

}