using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Weapon.SiantChristopher
{
    public class SiantChristopherFireSystem : MonoBehaviour
    {
        public GameObject Brust;
        public float FireDelta = 0.075f;
        public Transform TargetPosition;
        // Start is called before the first frame update
        void Start()
        {
            time = Random.Range(0.0f, FireDelta);
        }
        float time = 0;
        // Update is called once per frame
        void Update()
        {

            if (time < 0)
            {

                time = FireDelta;
                var brust=
                GameObject.Instantiate(Brust, this.transform);
                brust.transform.position = this.transform.position;
                var movementObj = brust.GetComponent<ObjectMovement>();
                movementObj.TargetPosition = TargetPosition;
                movementObj.GenerateSpeed();

            }
            else
            {
                time -= Time.deltaTime;
            }
        }
    }

}
