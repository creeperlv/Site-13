using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Site13Kernel.Weapon
{
    public class GenericWeapon : MonoBehaviour
    {
        public GameObject OriPoint;
        public bool isInfiniteAmmo = false;
        public AudioSource AS;
        public AudioSource AS2;
        public GameObject Shell;
        public GameObject Shell2;
        public GameObject ShellP;
        public Text AmmoShower;
        public AnimationClip recoilAni;
        public int AmmoID;
        public int MagazineCapacity;
        public Animation ani;
        public bool isCustomedAmmoShower = false;
        public bool isRecoilEnabled = true;
        public bool isPlayerWeapon = true;
        float orix;
        float oriy;
        float oriz;
        public float RecoilAngle = 10f;
        public float FireDelta = 0.5f;
        public SCPEntity Pocesser;
        // Start is called before the first frame update
        void Start()
        {
            //orix = transform.localPosition.x;
            //oriy = transform.localPosition.y;
            //oriz = transform.localPosition.z;
            orix = transform.localRotation.eulerAngles.x;
            oriy = transform.localRotation.eulerAngles.y;
            oriz = transform.localRotation.eulerAngles.z;
            try
            {

                ani.AddClip(recoilAni, "Recoil");
            }
            catch (System.Exception)
            {
            }
        }

        float timed = 0.5f;
        float timed2 = -1;
        public virtual void SideUpdate()
        {
            try
            {
                if (isCustomedAmmoShower == false)
                    AmmoShower.text = $"{GameInfo.CurrentGame.LeftAmmos[AmmoID]}";
            }
            catch (System.Exception)
            {
            }
            if (isRecoilEnabled == true) RecoilAnime();
        }
        public virtual void RecoilAnime()
        {
            if (timed2 >= 0)
            {
                var ang = transform.localRotation.eulerAngles;
                if (timed2 > 0.1f)
                {
                    ang.x = orix - (0.2f - timed2) * 10* RecoilAngle;
                }
                else
                {
                    ang.x = orix -  timed2 * 10 * RecoilAngle;
                }
                {

                    var lr = transform.localRotation;
                    lr.eulerAngles = ang;
                    transform.localRotation = lr;
                }
                //var lp = transform.localPosition;
                //if (timed2 > 0.1f)
                //{
                //    lp.z = oriz - (0.2f - timed2);
                //    lp.y = oriy + (0.2f - timed2)/2;
                //}
                //else if(timed2>0)
                //{
                //    lp.z = oriz - timed2;
                //    lp.y = oriy + timed2/2;
                //}
                //else
                //{
                //    lp.z = oriz;
                //    lp.y = oriy;
                //    lp.x = orix;
                //}
                //transform.localPosition = lp;
                timed2 -= Time.deltaTime;
                //if (timed2 < 0)
                //{
                //    lp.z = oriz;
                //    lp.y = oriy;
                //    lp.x = orix;
                //}
                //transform.localPosition = lp;
                if (timed2 < 0)
                {
                    ang.x = orix;
                    ang.y = oriy;
                    ang.z = oriz;
                }
                {
                    var lr = transform.localRotation;
                    lr.eulerAngles = ang;
                    transform.localRotation = lr;
                }
            }
            else
            {

            }
        }
        // Update is called once per frame
        void Update()
        {
            if (isPlayerWeapon)
            {
                if (Input.GetButton("Fire1"))
                {
                    if (GameInfo.CurrentGame.isPaused == false)
                    {

                        if (timed >= FireDelta)
                        {
                            if (isInfiniteAmmo == true)
                            {
                                FirePrimary();
                            }
                            else
                            if (GameInfo.CurrentGame.LeftAmmos[AmmoID] != 0)
                            {
                                FirePrimary();
                            }
                            timed = FireDelta - 0.1f;
                        }
                        else if (timed <= 0f)
                        {
                            timed = 1f;
                            //var lp = transform.position;
                            //lp.z = oriz;
                            //transform.position = lp;
                        }
                        else
                        {
                            //var lp = transform.position;
                            //if (timed > 0.25f)
                            //{
                            //    lp.z = oriz - (0.5f - timed);
                            //}
                            //else
                            //{
                            //    lp.z = oriz - timed;
                            //}
                            //transform.position = lp;
                        }
                        timed -= Time.deltaTime;
                    }
                }
                else
                if (Input.GetButton("Fire2"))
                {
                    if (GameInfo.CurrentGame.isPaused == false)
                    {

                        if (timed >= FireDelta)
                        {
                            if (isInfiniteAmmo == true)
                            {
                                FireSecondary();
                            }
                            else
                            if (GameInfo.CurrentGame.LeftAmmos[AmmoID] != 0)
                            {
                                FireSecondary();
                            }
                            timed = FireDelta - 0.1f;
                        }
                        else if (timed <= 0f)
                        {
                            timed = 1f;
                            //var lp = transform.position;
                            //lp.z = oriz;
                            //transform.position = lp;
                        }
                        else
                        {
                            //var lp = transform.position;
                            //if (timed > 0.25f)
                            //{
                            //    lp.z = oriz - (0.5f - timed);
                            //}
                            //else
                            //{
                            //    lp.z = oriz - timed;
                            //}
                            //transform.position = lp;
                        }
                        timed -= Time.deltaTime;
                    }
                }
                else
                {
                    timed = FireDelta + 1f;
                }
                SideUpdate();
            }
        }
        public virtual void FirePrimary()
        {
            timed2 = 0.2f;
            try
            {
                AS.Play();
            }
            catch (System.Exception)
            {
            }
            try
            {
                //ani.Play("Recoil");
            }
            catch (System.Exception)
            {
            }
            //StartCoroutine(Recoil());

            GameObject.Instantiate(Shell, OriPoint.transform.position, OriPoint.transform.rotation, ShellP.transform).GetComponent<SCP_R_13_Shell>().Sender = Pocesser;
            if (isInfiniteAmmo == false)
                GameInfo.CurrentGame.LeftAmmos[AmmoID]--;
        }
        public virtual void FireSecondary()
        {
            if (Shell2 != null)
            {
                try
                {
                    AS2.Play();
                }
                catch (System.Exception)
                {
                }
                try
                {
                    ani.Play("Recoil");
                }
                catch (System.Exception)
                {
                }
                //StartCoroutine(Recoil());
                try
                {
                    AmmoShower.text = $"{GameInfo.CurrentGame.LeftAmmos[AmmoID] % MagazineCapacity}/{MagazineCapacity}\r\n{GameInfo.CurrentGame.LeftAmmos[AmmoID]}";
                }
                catch (System.Exception)
                {
                }
                GameObject.Instantiate(Shell2, OriPoint.transform.position, OriPoint.transform.rotation, ShellP.transform).GetComponent<SCP_R_13_Shell>().Sender = Pocesser;
                if (isInfiniteAmmo == false)
                    GameInfo.CurrentGame.LeftAmmos[AmmoID]--;
            }
        }
        IEnumerator Recoil()
        {
            for (float i = 0; i < 0.25f; i += Time.deltaTime)
            {

                transform.Translate(Vector3.back * Time.deltaTime);
                i += Time.deltaTime;
                yield return null;

            }
            {

                var lp = transform.position;
                lp.z = oriz - 0.25f;
                transform.position = lp;
            }
            //transform.
            for (float i = 0; i < 0.25f; i += Time.deltaTime)
            {
                transform.Translate(Vector3.forward * Time.deltaTime);
                i += Time.deltaTime;
                yield return null;

            }
            {

                var lp = transform.position;
                lp.z = oriz;
                transform.position = lp;
            }
            //GetComponent<Animation>().Play("Recoil");
            yield return new WaitForSeconds(0.5f);
        }
    }
}
