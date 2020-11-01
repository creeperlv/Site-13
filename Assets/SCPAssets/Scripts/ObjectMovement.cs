using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public class ObjectMovement : MonoBehaviour
    {
        public Transform TargetPosition;
        public float AssumedSpeed = 1;
        public GameObject Brust;
        // Start is called before the first frame update
        void Start()
        {

        }
        private Vector3 Speed = Vector3.zero;
        private float AssumedTime = 5;
        private float timed = 0;
        public void GenerateSpeed()
        {
            var d = Math.Abs(Vector3.Distance(TargetPosition.position, transform.position)) / AssumedSpeed;
            Speed = (TargetPosition.position - transform.position) / d;
            AssumedTime = d;
            timed = 0;
            Debug.Log($"Fired: D:{d},S:{Speed}");
        }
        // Update is called once per frame
        void Update()
        {
            if (timed >= AssumedTime)
            {
                var BrustEffect=GameObject.Instantiate(Brust,this.transform.parent);
                BrustEffect.transform.position = this.transform.position;
                Destroy(this.gameObject);
            }
            else
            {
                if (timed <= AssumedTime / 4f)
                {
                    transform.Translate(Speed * Time.deltaTime / 2);

                }
                else if (timed <= AssumedTime / 4f * 3)
                {
                    transform.Translate(Speed * Time.deltaTime*1.5f);

                }
                else
                {
                    transform.Translate(Speed * Time.deltaTime / 2);

                }
            }
            timed += Time.deltaTime;
        }
    }
}
