using Site13ExternalKernel.Difficulty;
using Site13Kernel.FPSSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

namespace Site13Kernel.Weapon
{

    public class TraditionalWeapon : MonoBehaviour, IHandItem
    {
        public bool isHoldingByPlayer = false;
        public GameObject PrimaryShell;
        public GameObject BulletCrater;
        public GameObject RecoiledObject;
        public AudioSource AS;
        public AudioSource ReloadSound;
        public GameObject ShellPresenter;
        public string ID;
        public GameObject OriPoint;
        float orix;
        public Animator CentralAnimator;
        float oriy;
        float oriz;
        public int MaxCap = 0;
        [HideInInspector]
        public float timed = 0.5f;
        [HideInInspector]
        public float timed2 = -1;
        public float RecoilAngle = 10f;
        public float RecoilRecoverSpeed = 1f;
        public float ShootRange = 100f;
        public bool isCustomedAmmoShower = false;
        public bool isRecoilEnabled = true;
        public Text AmmoShower;
        public SCPEntity Pocesser;
        public float ReloadingDelay = 2.5f;
        public float BaseDamage = 50;
        public float CriticalDamage = 50;
        public GameObject Spark;
        public Animator Crosshair;
        public float AimingModeFOV = 30;
        public int DefaultMags = 3;
        public bool AmmoShowerPercentage = false;
        public bool AmmoShowerShowCap = true;
        public bool AmmoShowerShowTotal = true;
        public AimingModeSettings AimingMode;
        public FightWeaponSettings FightWeapon = new FightWeaponSettings();
        public List<Text> SecondaryAmmoShower;
        [Serializable]
        public class AimingModeSettings
        {
            public bool isAdvancedAimingModeEnabled;
            public Vector3 AimingMainObjectPosition;
            public Vector3 NormalMainObjectPosition;
            public Vector3 AimingMainObjectScale;
            public Vector3 NormalMainObjectScale;
            public float AimSpeed = 1;
            public GameObject MainObject;
        }

        [Serializable]
        public class FightWeaponSettings
        {
            public bool IsFightEnabled = false;
            public float Duration;
            public string FightTrigger;
        }
        bool isFightOnProcess;
        private void OnDisable()
        {
            ReloadTimePass = 0.0f;
        }
        private void OnEnable()
        {
            CentralAnimator.Play("Holding");
        }
        [HideInInspector]
        public string BagAlias;
        Vector3 AimingDelta = new Vector3();
        void Start()
        {
            if (AimingMode.isAdvancedAimingModeEnabled)
            {
                AimingDelta = AimingMode.AimingMainObjectPosition - AimingMode.NormalMainObjectPosition;
                AimingDelta = AimingDelta / AimingMode.AimSpeed;
            }
            orix = RecoiledObject.transform.localRotation.eulerAngles.x;
            oriy = RecoiledObject.transform.localRotation.eulerAngles.y;
            oriz = RecoiledObject.transform.localRotation.eulerAngles.z;
            if (isHoldingByPlayer)
            {
                BagAlias = Pocesser.GetComponent<SCPFirstController>().BagNameAlias;
            }
            {
                //Reading difficulty data.
                DefaultMags = int.Parse(DifficultyManager.CurrentDefinition.Get("MAG:" + ID, 0, DefaultMags + ""));
                BaseDamage = int.Parse(DifficultyManager.CurrentDefinition.Get("DAM:" + ID + "_Base", 0, BaseDamage + ""));
                CriticalDamage = int.Parse(DifficultyManager.CurrentDefinition.Get("DAM:" + ID + "_Critical", 0, CriticalDamage + ""));
            }
            if (GameInfo.CurrentGame.FlagsGroup.ContainsKey(BagAlias + ":" + ID))
            {
            }
            else
            {
                GameInfo.CurrentGame.FlagsGroup.Add(BagAlias + ":" + ID, MaxCap + "");
                GameInfo.CurrentGame.FlagsGroup.Add(BagAlias + ":" + ID + "_Total", MaxCap * DefaultMags + "");
            }
        }
        public virtual void RecoilAnime()
        {
            if (timed2 >= 0)
            {
                var ang = RecoiledObject.transform.localRotation.eulerAngles;
                if (timed2 > 0.1f)
                {
                    ang.x = orix - (0.2f - timed2) * 10 * (GameInfo.CurrentGame.isAiming ? RecoilAngle / 3 : RecoilAngle);
                }
                else
                {
                    ang.x = orix - timed2 * 10 * (GameInfo.CurrentGame.isAiming ? RecoilAngle / 3 : RecoilAngle);
                }
                {

                    var lr = RecoiledObject.transform.localRotation;
                    lr.eulerAngles = ang;
                    RecoiledObject.transform.localRotation = lr;
                }
                timed2 -= Time.deltaTime;
                if (timed2 < 0)
                {
                    ang.x = orix;
                    ang.y = oriy;
                    ang.z = oriz;
                }
                {
                    var lr = RecoiledObject.transform.localRotation;
                    lr.eulerAngles = ang;
                    RecoiledObject.transform.localRotation = lr;
                }
            }
            else
            {

            }
        }
        public virtual void SideUpdate()
        {
            if (Spark != null)
                if (SparkCutDown > 0)
                {
                    if (Spark.activeSelf == false)
                    {
                        Spark.SetActive(true);
                    }
                    SparkCutDown -= Time.deltaTime;
                }
                else
                {

                    if (Spark.activeSelf != false)
                    {
                        Spark.SetActive(false);
                    }
                }
            try
            {
                if (isCustomedAmmoShower == false)
                {
                    if (!AmmoShowerPercentage)
                    {
                        string ToShow = GameInfo.CurrentGame.FlagsGroup[BagAlias + ":" + ID];
                        if (AmmoShowerShowCap == true)
                            ToShow += $"/{MaxCap}";
                        if (AmmoShowerShowTotal == true)
                            ToShow += $"\r\n{GameInfo.CurrentGame.FlagsGroup[BagAlias + ":" + ID + "_Total"]}";
                        AmmoShower.text = ToShow;
                        foreach (var item in SecondaryAmmoShower)
                        {
                            item.text = ToShow;
                        }
                    }
                    else
                    {
                        string ToShow = $"{("" + ((float)int.Parse(GameInfo.CurrentGame.FlagsGroup[BagAlias + ":" + ID])) / ((float)MaxCap) * 100).Split('.')[0]}%";
                        AmmoShower.text = ToShow;
                        foreach (var item in SecondaryAmmoShower)
                        {
                            item.text = ToShow;
                        }
                    }
                }
            }
            catch (System.Exception)
            {
            }
            if (isRecoilEnabled == true)
                RecoilAnime();
            if (SecondaryRecoilRate != 0)
            {
                SecondaryRecoilRate -= Time.deltaTime * RecoilRecoverSpeed;
                if (SecondaryRecoilRate <= 0)
                {
                    SecondaryRecoilRate = 0;
                }
            }
        }
        public float FireDelta = 0.1f;
        [HideInInspector]
        public float SecondaryRecoilRate = 0.0f;
        [HideInInspector]
        public float SparkCutDown = 0.0f;
        public float DoubleSideRandom(float Limit) => UnityEngine.Random.Range(-Limit, Limit);
        public virtual void SinglePrimaryOperate()
        {

            timed2 = 0.2f;
            try
            {
                AS.Play();
            }
            catch (Exception)
            {
            }
            //try
            //{
            //    //ani.Play("Recoil");
            //}
            //catch (System.Exception)
            //{
            //}
            {
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0F));
                GameInfo.CurrentGame.FirstPerson.ShakeHeadRandomly(RecoilAngle / 6, -RecoilAngle / 6, RecoilAngle / 6, RecoilAngle / 10, RecoilRecoverSpeed);
                RaycastHit hit; // Variable reading information about the collider 
                                // Cast ray from center of the screen towards where the player is looking
                SparkCutDown = 0.1f;
                if (Physics.Raycast(ray, out hit, ShootRange, 1 << 0 | 1 << 13, QueryTriggerInteraction.Ignore))
                {
                    bool aa = true;
                    if (hit.collider.gameObject.GetComponent<SCPEntity>() != null)
                    {
                        if (hit.collider.gameObject.GetComponent<SCPEntity>() != Pocesser.gameObject.GetComponent<SCPEntity>())
                        {
                            try
                            {
                                Crosshair.SetTrigger("Hit");
                            }
                            catch (Exception)
                            {
                            }
                            var entity = hit.collider.gameObject.GetComponent<SCPEntity>();
                            try
                            {

                                try
                                {
                                    Quaternion quaternion = Quaternion.FromToRotation(Vector3.up, hit.normal);
                                    var a = GameObject.Instantiate(entity.DamageEffect, hit.point, quaternion, hit.transform.parent);
                                }
                                catch (Exception e)
                                {
                                    Debug.Log(e);
                                }
                            }
                            catch (Exception)
                            {
                            }
                            entity.ChangeHealth(-(BaseDamage + (CriticalDamage * UnityEngine.Random.Range(0f, 1f))));
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
                        catch (Exception e)
                        {
                            Debug.Log(e);
                        }
                }
            }
            SecondaryRecoilRate += 0.1f;
            if (SecondaryRecoilRate > 1)
            {
                SecondaryRecoilRate = 1;
            }
            //a.layer = 8;
            //a.transform.GetChild(0).gameObject.layer = 8;
            if (MaxCap != 0)
            {
                GameInfo.CurrentGame.FlagsGroup[BagAlias + ":" + ID] = "" + (int.Parse(GameInfo.CurrentGame.FlagsGroup[BagAlias + ":" + ID]) - 1);
            }
        }
        public virtual void SingleSecondaryOperate()
        {
            {
                GameInfo.CurrentGame.isAiming = true;
                GameInfo.CurrentMouseSen = GameInfo.TargetMouseSen / 10;
                if (Camera.main.fieldOfView > AimingModeFOV)
                {
                    Camera.main.fieldOfView -= Time.deltaTime * (100f / 40f) * (60f - AimingModeFOV);
                }
                else
                    Camera.main.fieldOfView = AimingModeFOV;
                {
                    if (AimingMode.MainObject.transform.localPosition.x <= AimingMode.AimingMainObjectPosition.x)
                    {
                        AimingMode.MainObject.transform.localPosition = AimingMode.AimingMainObjectPosition;
                    }
                    else
                    {
                        AimingMode.MainObject.transform.localScale = AimingMode.AimingMainObjectScale;
                        AimingMode.MainObject.transform.Translate(AimingDelta * Time.deltaTime);
                    }
                }
            }
        }
        IEnumerator RealFight()
        {
            isHolding = true;
            CentralAnimator.SetTrigger(FightWeapon.FightTrigger);
            yield return new WaitForSeconds(FightWeapon.Duration);
            isHolding = false;
            yield return null;
        }
        float ReloadTimePass = 0.0f;
        // Update is called once per frame
        void Update()
        {
            SideUpdate();
            if (isHoldingByPlayer == true)
            {

                if (Input.GetButton("Fire1"))
                {
                    Primary();
                }
                else
                {
                    timed = FireDelta + 1f;
                }
                //if ()
                {

                    if (Input.GetButton("Fire2") && ReloadTimePass == 0.0f && GameInfo.CurrentGame.isRunning == false)
                    {
                        if (GameInfo.CurrentGame.isPaused == false)
                        {
                            GameInfo.CurrentGame.isAimingEnded = false;
                            SingleSecondaryOperate();
                        }
                    }
                    else
                    {
                        if (GameInfo.CurrentGame.isAiming == true && GameInfo.CurrentGame.isRunning == true)
                        {
                            GameInfo.CurrentGame.isAimingEnded = false;
                            if (GameInfo.CurrentMouseSen != GameInfo.TargetMouseSen)
                                GameInfo.CurrentMouseSen = GameInfo.TargetMouseSen;
                            if (Camera.main.fieldOfView < GameInfo.CurrentGame.UsingFOV)
                            {
                                Camera.main.fieldOfView += Time.deltaTime * 100;
                            }
                            else
                            {

                                Camera.main.fieldOfView = GameInfo.CurrentGame.UsingFOV;
                            }
                        }
                        if (GameInfo.CurrentGame.isAiming == false && GameInfo.CurrentGame.isRunning == false && GameInfo.CurrentGame.isAimingEnded == false)
                        {

                            if (
                            GameInfo.CurrentMouseSen != GameInfo.TargetMouseSen)
                                GameInfo.CurrentMouseSen = GameInfo.TargetMouseSen;

                            if (Camera.main.fieldOfView < GameInfo.CurrentGame.UsingFOV)
                            {
                                Camera.main.fieldOfView += Time.deltaTime * 100;
                            }
                            else
                            {

                                Camera.main.fieldOfView = GameInfo.CurrentGame.UsingFOV;
                                GameInfo.CurrentGame.isAimingEnded = true;
                            }
                            // GameInfo.CurrentGame.isAimingEnded = false;
                        }
                        {
                            if (AimingMode.MainObject.transform.localPosition.x >= AimingMode.NormalMainObjectPosition.x)
                            {
                                AimingMode.MainObject.transform.localPosition = AimingMode.NormalMainObjectPosition;
                            }
                            else
                            {
                                AimingMode.MainObject.transform.localScale = AimingMode.NormalMainObjectScale;
                                AimingMode.MainObject.transform.Translate(-AimingDelta * Time.deltaTime);
                            }
                        }
                        GameInfo.CurrentGame.isAiming = false;
                    }
                }
                //else
                //{

                //}
                if (Input.GetButton("Reload"))
                {
                    if (GameInfo.CurrentGame.isPaused == false)
                    {
                        if (ReloadTimePass <= 0.0f)
                        {
                            if (int.Parse(GameInfo.CurrentGame.FlagsGroup[BagAlias + ":" + ID + "_Total"]) > 0)
                            {
                                CentralAnimator.SetTrigger("Reload");
                                ReloadSound.Play();
                                ReloadTimePass = 0.1f;
                            }
                        }
                    }
                }
                else
                {
                    if (
                       ReloadTimePass >= 0.05f)
                    {
                        ReloadTimePass += Time.deltaTime;
                        if (ReloadTimePass >= (ReloadingDelay + 0.1f) / 2)
                        {
                            ReloadTimePass = 0.0f;
                            {
                                int Weapon_Total = int.Parse(GameInfo.CurrentGame.FlagsGroup[BagAlias + ":" + ID + "_Total"]);
                                int Weapon_Current = int.Parse(GameInfo.CurrentGame.FlagsGroup[BagAlias + ":" + ID]);
                                //SetMag;
                                if (Weapon_Total > MaxCap)
                                {
                                    GameInfo.CurrentGame.FlagsGroup[BagAlias + ":" + ID + "_Total"] =
                                        (Weapon_Total - MaxCap + Weapon_Current).ToString();
                                    GameInfo.CurrentGame.FlagsGroup[BagAlias + ":" + ID] = MaxCap.ToString();
                                }
                                else if (Weapon_Total > 0)
                                {
                                    if (Weapon_Total+Weapon_Current >= MaxCap)
                                    {
                                        GameInfo.CurrentGame.FlagsGroup[BagAlias + ":" + ID + "_Total"] =
                                            (Weapon_Total + Weapon_Current - MaxCap).ToString();
                                        GameInfo.CurrentGame.FlagsGroup[BagAlias + ":" + ID] = MaxCap.ToString();
                                    }
                                    else
                                    {
                                        GameInfo.CurrentGame.FlagsGroup[BagAlias + ":" + ID] =
                                            (Weapon_Total + Weapon_Current).ToString();
                                        GameInfo.CurrentGame.FlagsGroup[BagAlias + ":" + ID + "_Total"] = "0";

                                    }
                                }
                            }
                        }
                    }
                }

            }
        }


        public void Primary()
        {
            if (GameInfo.CurrentGame.isPaused == false && ReloadTimePass <= 0.0f)
            {
                if (timed >= FireDelta)
                {
                    if (MaxCap == 0)
                    {
                        SinglePrimaryOperate();
                    }
                    else
                    if (int.Parse(GameInfo.CurrentGame.FlagsGroup[BagAlias + ":" + ID]) != 0)
                    {
                        SinglePrimaryOperate();
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
                timed -= Time.deltaTime;
            }
        }

        public void Secondary()
        {
            //
        }

        public void Reload()
        {
            throw new NotImplementedException();
        }

        public void Init(string data)
        {
        }

        public string GetData()
        {
            return null;
        }

        public void Fight()
        {
            if(FightWeapon.IsFightEnabled==true)
            StartCoroutine(RealFight());
        }

        public bool IsPrimaryCompleted()
        {
            throw new NotImplementedException();
        }

        public bool IsSecondaryCompleted()
        {
            throw new NotImplementedException();
        }

        public bool IsFightCompleted()
        => isFightOnProcess;


        public bool IsReloadCompleted()
        {
            throw new NotImplementedException();
        }

        public bool IsFPSSystemV3Enabled() => true;
        bool isHolding = false;
        public bool IsOnOperation() => isHolding;

        public void UnPrimary()
        {
            timed = FireDelta + 1f;
        }

        public void UnSecondary()
        {
        }
    }

}