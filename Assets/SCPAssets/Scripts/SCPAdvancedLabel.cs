using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.UI
{
    public class SCPAdvancedLabel : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }
        float parameter0 = 3;
        float parameter1 = 0;
        // Update is called once per frame
        void Update()
        {
            //this.GetComponent<RectTransform>().LookAt(Camera.main.transform);
            this.transform.LookAt(Camera.main.transform);
            var r = this.transform.rotation;
            var ea = r.eulerAngles;
            ea.x = 0;
            ea.y = Camera.main.transform.rotation.eulerAngles.y + 180;
            r.eulerAngles = ea;
            transform.rotation = r;
            var distance = Vector3.Distance(this.transform.position, Camera.main.transform.position);
            var s = (distance/parameter0) + parameter1;
            transform.localScale = new Vector3(s, s, s);
        }
    }

}
