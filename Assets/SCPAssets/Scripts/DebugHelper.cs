using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Diagnostics
{

    public class DebugHelper : MonoBehaviour
    {
        public List<EFI.EFIBase> EssentialEFIs;
        // Start is called before the first frame update
        void Start()
        {
            foreach (var item in EssentialEFIs)
            {
                item.Run();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}