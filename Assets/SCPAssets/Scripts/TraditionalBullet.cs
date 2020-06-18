using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Weapon
{

    public class TraditionalBullet : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }
        public float speed = 10;
        public float Damage = 50;
        public float CriticalDamage = 50;
        public GameObject Brust;
        [HideInInspector]
        public bool isFromPlayer = true;
        [HideInInspector]
        public SCPEntity Sender;
        // Update is called once per frame
        float TimeD = 0;
        void Update()
        {
            transform.Translate(Vector3.forward*speed * Time.deltaTime);

            TimeD += Time.deltaTime;
            //if (TimeD > 0.3)
            //{
            //    transform.GetChild(0).gameObject.SetActive(true);
            //}
            if (TimeD > 30f)
            {
                Destroy(this);
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            bool willNotCrash = false;
            try
            {
                bool willEffect = true;
                if (collision.collider.isTrigger == true)
                {
                    willNotCrash = true;
                    willEffect = false;
                }
                if (Sender == collision.gameObject.GetComponent<SCPEntity>())
                {
                    willEffect = false;
                    willNotCrash = true;
                }
                if (willEffect == true)
                {
                    try
                    {

                        collision.gameObject.GetComponent<SCPEntity>().ChangeHealth(-(Damage+(CriticalDamage*Random.Range(0f,1f))));
                    }
                    catch (System.Exception)
                    {

                    }
                }
            }
            catch (System.Exception)
            {
            }
            if (willNotCrash != true)
            {

                GameObject.Instantiate(Brust, this.transform.position, this.transform.rotation, transform.parent);
                this.gameObject.SetActive(false);
                GameObject.Destroy(this.gameObject);
            }
        }
        void OnTriggerEnter(Collider other)
        {
            //try
            //{
            //    bool willEffect = true;
            //    if (isFromPlayer == true)
            //    {
            //        if (other.GetComponent<FirstPersonAIO>() != null)
            //        {
            //            willEffect = false;
            //        }
            //    }
            //    if (willEffect == true)
            //    {
            //        try
            //        {

            //            other.GetComponent<SCPEntity>().ChangeHealth(-Damage);
            //        }
            //        catch (System.Exception)
            //        {

            //        }
            //    }
            //}
            //catch (System.Exception)
            //{
            //}
            //this.gameObject.SetActive(false);
            //GameObject.Destroy(this.gameObject);
        }
    }

}