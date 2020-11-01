using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Site13Kernel.DynamicScene
{

    public class PrimarySceneController : MonoBehaviour
    {
        public static PrimarySceneController CurrentPrimaryScene;
        public SCPFirstController MainCharacter;
        public PostProcessVolume UsingEffectVolume;
        public PostProcessProfile UsingEffectProfile;
        public void Start()
        {
            CurrentPrimaryScene = this;
        }
    }

}