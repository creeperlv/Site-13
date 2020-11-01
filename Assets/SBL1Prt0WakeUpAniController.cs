using Site13Kernel.DynamicScene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Site13Kernel.Stories
{
    public class SBL1Prt0WakeUpAniController : MonoBehaviour
    {
        public PostProcessProfile TargetProfile;
        public float DoF_Length = 10;
        DepthOfField Depth;
        // Start is called before the first frame update
        void Start()
        {
            foreach (var item in
            TargetProfile.settings)
            {
                if (item is DepthOfField)
                {
                    Depth = item as DepthOfField;
                }
            }

        }
        float timed;
        void Update()
        {
            if (Depth.enabled.value == false) Depth.enabled.value = true;
            Depth.focusDistance.value= DoF_Length;
            timed += Time.deltaTime;
            if (timed >= 9.5f)
            {
                PrimarySceneController.CurrentPrimaryScene.MainCharacter.gameObject.SetActive(true);
            }
        }
    }

}