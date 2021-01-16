using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public class AD1 : SCPEntity
    {
        public GameObject ShootPont;
        public GameObject OriginalPont;
        public GameObject Part1;
        public GameObject Part2;
        public GameObject BulletCrater;
        public GameObject Presenter;
        public AudioSource SFX;
        public AudioSource SFX2;
        public float Speed = 50;
        public float BaseDamage = 20;
        public float CriticalDamage = 20;
        List<SCPEntity> entities = new List<SCPEntity>();
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<SCPEntity>() != null)
            {
                entities.Add(other.GetComponent<SCPEntity>());
            }
        }
        private void OnTriggerExit(Collider other)
        {

            if (other.GetComponent<SCPEntity>() != null)
            {
                entities.Remove(other.GetComponent<SCPEntity>());
            }

        }
        SCPEntity entity;
        void Movement()
        {
            
            float d = 40;
            for (int i = 0; i < entities.Count; i++)
            {
                try
                {
                    if(entities[i].gameObject.activeSelf==true)
                    if (d > Vector3.Distance(OriginalPont.transform.position, entities[i].transform.position))
                    {
                        d = Vector3.Distance(OriginalPont.transform.position, entities[i].transform.position);
                        entity = entities[i];
                    }
                }
                catch (System.Exception)
                {
                }
            }
            if (entity != null)
                try
                {
                    //Calculate move
                    //First Horizontal
                    var a = entity.transform.position - OriginalPont.transform.position;
                    //a.Normalize();
                    var b = ShootPont.transform.position - OriginalPont.transform.position;
                    //b.Normalize();
                    //Debug.Log($"A:x{a.x},{a.y},{a.z}|B:{b.x},{b.y},{b.z}|SP:{ShootPont.transform.position.x},{ShootPont.transform.position.z}");
                    {
                        float dot = Vector3.Dot(-ShootPont.transform.up, a.normalized);
                        float dot1 = Vector3.Dot(ShootPont.transform.right, a.normalized);
                        //Debug.Log($"{dot},{dot1}");
                        //var h = Vector3.SignedAngle(Vector3.up, a , Vector3.forward);
                        //Quaternion rotation00 = Quaternion.Euler(0, h, 0);
                        //Part1.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation00, Time.deltaTime * 50);
                        //Debug.Log("H"+ h);
                        //if(h>3)
                        Part1.transform.Rotate(new Vector3(0, (dot1>0?1:-1) * Speed * Time.deltaTime, 0));
                        Part2.transform.Rotate(new Vector3((dot > 0 ? 1 : -1) * Speed * Time.deltaTime,0, 0));
                        //    var ANG = Part1.transform.localRotation.eulerAngles;
                        //ANG.y += h * Time.deltaTime;
                        //var lr = Part1.transform.localRotation;
                        //lr.eulerAngles = ANG;
                        //Part1.transform.localRotation = lr;
                    }

                    //Second Vertical
                    {

                        //var h = Vector3.SignedAngle(Vector3.forward, a - b, Vector3.forward);
                        ////Debug.Log("H" + h);
                        //Part2.transform.Rotate(new Vector3(Mathf.Sign(h) * 50 * Time.deltaTime, 0, 0));
                    }
                    //Debug.Log($"Rotate: h:{h},");
                }
                catch (System.Exception)
                {
                    //Debug.Log("Error");
                }


        }
        float time = 0.02f;
        bool isFiring = false;
        void Update()
        {
            Movement();
            if (entities.Count != 0)
            {
                if (isFiring == false)
                {
                    GetComponent<Animator>().SetTrigger("Fire"); SFX2.Play();
                    isFiring = true;
                }
                if (time < 0)
                {
                    SFX.Play();
                    time = 0.02f;
                    Ray ray = new Ray(ShootPont.transform.position, ShootPont.transform.forward);
                    RaycastHit hit; // Variable reading information about the collider 
                                                                                                                  // Cast ray from center of the screen towards where the player is looking
                    //SparkCutDown = 0.1f;
                    //if (Physics.Raycast(ray, out hit, ShootRange))
                    if (Physics.Raycast(ray, out hit, 50, ~(1 << 2), QueryTriggerInteraction.Ignore))
                    {
                        bool aa = true;
                        //Debug.Log("Hit - 1");
                        if (hit.collider.gameObject.GetComponent<SCPEntity>() != null)
                        {
                            //Debug.Log("Hit - 2");
                            if (hit.collider.gameObject.GetComponent<SCPEntity>() != this.gameObject.GetComponent<SCPEntity>())
                            {
                                //try
                                //{
                                //    Crosshair.SetTrigger("Hit");
                                //}
                                //catch (System.Exception)
                                //{
                                //}
                                hit.collider.gameObject.GetComponent<SCPEntity>().ChangeHealth(-(BaseDamage + (CriticalDamage * Random.Range(0f, 1f))));
                            }
                            else
                            {
                                aa = false;
                            }
                        }

                        if (aa == true)
                            try
                            {
                                Quaternion quaternion = Quaternion.FromToRotation(Vector3.up, hit.normal);
                                var a = GameObject.Instantiate(BulletCrater, hit.point, quaternion);
                                a.transform.parent = hit.collider.transform;
                            }
                            catch (System.Exception e)
                            {
                                Debug.Log(e);
                            }
                    }
                }
                else
                {
                    time -= Time.deltaTime;
                }
            }
            else
            {
                if (isFiring != false)
                {
                    GetComponent<Animator>().SetTrigger("Stop"); SFX2.Pause();
                    isFiring = false;
                }
                entity = null;
            }
        }
    }


}