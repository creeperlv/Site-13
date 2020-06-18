using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Experimentals.OoD.V2
{
    public class SCPOoDV2 : MonoBehaviour
    {
        // Start is called before the first frame update
        public float ViewDistance = 50f;
        [HideInInspector]
        public List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
        void Start()
        {
            CollectRenders();
        }
        public void CollectRenders()
        {
            CollectRecursively(this.transform);
        }
        public OoDObjectState OoDObjectState
        {
            get; private set;
        } = OoDObjectState.Rendered;
        private void CollectRecursively(Transform transform)
        {
            var mr = transform.GetComponents<MeshRenderer>();
            foreach (var item in mr)
            {
                if(item.enabled==true)
                meshRenderers.Add(item);
            }
            for (int i = 0; i < transform.childCount; i++)
            {
                CollectRecursively(transform.GetChild(i));
            }
        }
        public void ToggleRenderer()
        {
            switch (OoDObjectState)
            {
                case OoDObjectState.Rendered:
                    {
                        foreach (var item in meshRenderers)
                        {
                            item.enabled = false;
                        }
                    }
                    OoDObjectState = OoDObjectState.DisRendered;
                    break;
                case OoDObjectState.DisRendered:
                    {
                        foreach (var item in meshRenderers)
                        {
                            item.enabled = true;
                        }
                    }
                    OoDObjectState = OoDObjectState.Rendered;
                    break;
                default:
                    break;
            }
        }
    }
    public enum OoDObjectState
    {
        Rendered, DisRendered
    }
}
