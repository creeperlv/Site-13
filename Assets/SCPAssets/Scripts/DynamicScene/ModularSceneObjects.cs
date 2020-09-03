using Site13Kernel.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.DynamicScene
{
    public class ModularSceneObjects : MonoBehaviour
    {
        public ModularSaveSystemModule SaveModule;
        public List<IModularSceneComponent> Components;
    }
    public interface IModularSceneComponent
    {
        void Init();
        void OnDispose();
    }
}