using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Audio
{

    public class AudioSegment : MonoBehaviour
    {
        public AudioClip ManagedClip;
        public List<SegementDefinition> definitions;
    }
    [Serializable]
    public class SegementDefinition
    {
        public float L;
        public float R;
        public bool isLoop;
        public float LFade;
        public float RFade;
    }
}