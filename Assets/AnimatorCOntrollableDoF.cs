using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Site13Kernel.Animations
{
    public class AnimatorCOntrollableDoF : MonoBehaviour
    {

        public PostProcessProfile profile;
        public float Value;
        public bool IsEnabled;
        DepthOfField Depth;
        // Update is called once per frame
        float timed;
        private void Start()
        {

            foreach (var item in
            profile.settings)
            {
                if (item is DepthOfField)
                {
                    Depth = item as DepthOfField;
                }
            }
        }
        void Update()
        {
            if (Depth.enabled.value != IsEnabled) Depth.enabled.value = IsEnabled;
            Depth.focusDistance.value = Value;
            timed += Time.deltaTime;

        }
    }
}