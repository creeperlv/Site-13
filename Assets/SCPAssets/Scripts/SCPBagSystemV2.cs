using Site13Kernel.FPSSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    //Version 2.1:
    //  Allow multiple owners.
    //Version 2.0:
    //  Weapons can be replaced.
    public class SCPBagSystemV2 : MonoBehaviour
    {
        int currentItem = 0;
        public int DefaultItem = 0;
        public string BAGOwner = "JOHN";
        public string DefPri = "";
        public string DefSec = "S-CO-O-18";
        public string DefKeyCard = "L-3-K";
        public string DefPhone = "LUMIA950";
        public float WatchModeFov = 25;
        public CanvasGroup ObserveModeOverlay;
        [Serializable]
        public class BagItem
        {
            public BagItemType type;
            public string ID;
            public GameObject Item;
        }
        public List<BagItem> bagItems = new List<BagItem>();
        bool WatchMode00 = false;
        private void Start()
        {
            if (!GameInfo.CurrentGame.FlagsGroup.ContainsKey($"BAG.{BAGOwner}.1"))
            {
                GameInfo.CurrentGame.FlagsGroup.Add($"BAG.{BAGOwner}.1", DefPri);
            }
            if (!GameInfo.CurrentGame.FlagsGroup.ContainsKey($"BAG.{BAGOwner}.2"))
            {
                GameInfo.CurrentGame.FlagsGroup.Add($"BAG.{BAGOwner}.2", DefSec);
            }
            if (!GameInfo.CurrentGame.FlagsGroup.ContainsKey($"BAG.{BAGOwner}.3"))
            {
                GameInfo.CurrentGame.FlagsGroup.Add($"BAG.{BAGOwner}.3", DefKeyCard);
            }
            if (!GameInfo.CurrentGame.FlagsGroup.ContainsKey($"BAG.{BAGOwner}.4"))
            {
                GameInfo.CurrentGame.FlagsGroup.Add($"BAG.{BAGOwner}.4", "FAK");
            }
            if (!GameInfo.CurrentGame.FlagsGroup.ContainsKey($"BAG.{BAGOwner}.5"))
            {
                GameInfo.CurrentGame.FlagsGroup.Add($"BAG.{BAGOwner}.5", DefPhone);
            }
            currentItem = DefaultItem;
            SetItem();
        }
        void NextItem()
        {
            currentItem++;
            if (currentItem < 6)
            {

            }
            else
            {
                currentItem = 0;
            }
            SetItem();
        }
        void PreviousItem()
        {
            currentItem--;
            if (currentItem < 0)
            {
                currentItem = 5;
            }
            else
            {
            }
            SetItem();
        }
        void CheckSwitchItem()
        {

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                NextItem();
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
                PreviousItem();

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                //Empty Hand
                currentItem = 0;
                SetItem();
            }
            else
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //Primary
                currentItem = 1;
                SetItem();
            }
            else
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                //Secondary
                currentItem = 2;
                SetItem();
            }
            else
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                //Keycard
                currentItem = 3;
                SetItem();
            }
            else
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                //First Aid Kit
                currentItem = 4;
                SetItem();
            }
            else
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                //Mobile Phone
                currentItem = 5;
                SetItem();
            }

        }
        void Update()
        {
            if (GameInfo.CurrentGame.isPaused == false)
            {
                if (
                   GameInfo.CurrentGame.isWatching == false)
                    if (isCurrentItemV3Enabled == true)
                    {
                        if (CurrentItem.IsOnOperation() == false) CheckSwitchItem();
                    }
                    else
                    {
                        CheckSwitchItem();
                    }
            }

            if (Input.GetButton("Watch"))
            {
                if (
                   GameInfo.CurrentGame.isWatching == false)
                {
                    if (Camera.main.fieldOfView > WatchModeFov)
                    {
                        if (WatchMode00 == false)
                            WatchMode00 = true;
                        if (ObserveModeOverlay != null)
                            ObserveModeOverlay.alpha += Time.deltaTime * 2;//* (GameInfo.CurrentGame.UsingFOV - WatchModeFov);
                        Camera.main.fieldOfView -= Time.deltaTime * 2 * (GameInfo.CurrentGame.UsingFOV - WatchModeFov);
                    }
                    else
                    {
                        if (ObserveModeOverlay != null)
                            ObserveModeOverlay.alpha = 1;
                        GameInfo.CurrentGame.isAiming = true;
                        Camera.main.fieldOfView = WatchModeFov;
                        foreach (var item in bagItems)
                        {
                            item.Item.SetActive(false);
                        }
                        GameInfo.CurrentMouseSen = GameInfo.TargetMouseSen / 20;
                        GameInfo.CurrentGame.isWatching = true;
                    }
                }
            }
            else
            {
                if (GameInfo.CurrentGame.isWatching == true | WatchMode00 == true)
                {
                    if (Camera.main.fieldOfView < GameInfo.CurrentGame.UsingFOV)
                    {
                        if (ObserveModeOverlay != null)
                            ObserveModeOverlay.alpha -= Time.deltaTime * 2;//* (GameInfo.CurrentGame.UsingFOV - WatchModeFov) / 5;
                        Camera.main.fieldOfView += Time.deltaTime * 2 * (GameInfo.CurrentGame.UsingFOV - WatchModeFov);
                    }
                    else
                    {
                        if (ObserveModeOverlay != null)
                            ObserveModeOverlay.alpha = 0;
                        GameInfo.CurrentGame.isWatching = false;
                        WatchMode00 = false;
                        GameInfo.CurrentMouseSen = GameInfo.TargetMouseSen;
                        GameInfo.CurrentGame.isAiming = false; SetItem();
                        Camera.main.fieldOfView = GameInfo.CurrentGame.UsingFOV;
                    }
                }
            }
            if (
               GameInfo.CurrentGame.isWatching == false)
                try
                {
                    string id2 = id1;
                    switch (currentItem)
                    {
                        case 0:
                            {

                            }
                            break;
                        case 1:
                            {
                                //Primary Weapon
                                try
                                {
                                    id2 = GameInfo.CurrentGame.FlagsGroup[$"BAG.{BAGOwner}.1"];
                                }
                                catch (Exception)
                                {
                                }
                            }
                            break;
                        case 2:
                            {
                                try
                                {
                                    id2 = GameInfo.CurrentGame.FlagsGroup[$"BAG.{BAGOwner}.2"];
                                }
                                catch (Exception)
                                {
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    if (id1 != id2)
                    {
                        foreach (var item in bagItems)
                        {
                            if (item.ID == id1)
                            {
                                item.Item.SetActive(false);
                                break;
                            }
                        }
                        id1 = id2;
                        foreach (var item in bagItems)
                        {
                            if (item.ID == id1)
                            {
                                item.Item.SetActive(true);
                                ApplyItem(item.Item);
                                break;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }

            if (
               GameInfo.CurrentGame.isWatching == false)
                if (isCurrentItemV3Enabled == true)
                {
                    //Deal with keys...

                    if (Input.GetButton("Fire1"))
                    {
                        if (CurrentItem.IsOnOperation() == false)
                            CurrentItem.Primary();
                    }
                    else if (Input.GetButtonUp("Fire1"))
                    {
                        CurrentItem.UnPrimary();
                    }
                    //if (Input.GetButton("Fire2"))
                    //{
                    //    if (CurrentItem.IsOnOperation() == false)
                    //        CurrentItem.Secondary();
                    //}
                    //else if (Input.GetButtonUp("Fire2"))
                    //{
                    //    CurrentItem.UnSecondary();
                    //}
                    if (Input.GetButtonDown("Fight"))
                    {
                        if (CurrentItem.IsOnOperation() == false)
                            CurrentItem.Fight();
                    }
                    if (Input.GetButtonDown("FlashLight"))
                    {
                        if (CurrentItem.IsOnOperation() == false)
                            CurrentItem.FlashLight();
                    }
                }
        }
        string id1 = "";
        IHandItem CurrentItem = null;
        bool isCurrentItemV3Enabled = false;
        void SetItem()
        {
            foreach (var item in bagItems)
            {
                item.Item.SetActive(false);
            }
            id1 = "";
            switch (currentItem)
            {
                case 0:
                    {

                    }
                    break;
                case 1:
                    {
                        //Primary Weapon
                        try
                        {
                            id1 = GameInfo.CurrentGame.FlagsGroup[$"BAG.{BAGOwner}.1"];
                        }
                        catch (Exception)
                        {
                        }
                    }
                    break;
                case 2:
                    {
                        try
                        {
                            id1 = GameInfo.CurrentGame.FlagsGroup[$"BAG.{BAGOwner}.2"];
                        }
                        catch (Exception)
                        {
                        }
                    }
                    break;
                case 3:
                    {
                        try
                        {
                            id1 = GameInfo.CurrentGame.FlagsGroup[$"BAG.{BAGOwner}.3"];
                        }
                        catch (Exception e)
                        {
                            Debug.Log(e);
                        }
                    }
                    break;

                case 4:
                    {
                        try
                        {
                            id1 = GameInfo.CurrentGame.FlagsGroup[$"BAG.{BAGOwner}.4"];
                        }
                        catch (Exception)
                        {
                        }
                    }
                    break;
                case 5:
                    {
                        try
                        {
                            id1 = GameInfo.CurrentGame.FlagsGroup[$"BAG.{BAGOwner}.5"];
                        }
                        catch (Exception)
                        {
                        }
                    }
                    break;
                default:
                    break;
            }
            bool isFetch = false;
            foreach (var item in bagItems)
            {
                if (item.ID == id1)
                {
                    isFetch = true;
                    //Debug.Log("Take out:"+id1);
                    //GameInfo.CurrentGame.HandingItem = item.Item.GetComponent<HandableItem>();
                    //CurrentItem = item.Item.GetComponent<IHandItem>();
                    //if (CurrentItem == null) isCurrentItemV3Enabled = false;
                    //else isCurrentItemV3Enabled = CurrentItem.IsFPSSystemV3Enabled();
                    //item.Item.SetActive(true);
                    //try
                    //{

                    //    if (isCurrentItemV3Enabled == false)
                    //    {
                    //        GameInfo.CurrentGame.FirstPerson.Crosshair.sprite = GameInfo.CurrentGame.FirstPerson.DefaultCrosshair;
                    //        GameInfo.CurrentGame.FirstPerson.ViewportShakingIntensity = new Vector3(3, 0, 0);
                    //    }
                    //    else
                    //    {
                    //        GameInfo.CurrentGame.FirstPerson.ViewportShakingIntensity = CurrentItem.GetOverriddenViewportShakingIntensity();
                    //        if (CurrentItem.IsCrosshairOverridden() == false)
                    //        {
                    //            GameInfo.CurrentGame.FirstPerson.Crosshair.sprite = GameInfo.CurrentGame.FirstPerson.DefaultCrosshair;
                    //        }
                    //        else
                    //        {
                    //            GameInfo.CurrentGame.FirstPerson.Crosshair.sprite = CurrentItem.GetOverriddenCrosshair();
                    //        }
                    //    }
                    //}
                    //catch (Exception)
                    //{
                    //}
                    ApplyItem(item.Item);
                    break;
                }
            }
            if (isFetch == false)
            {
                GameInfo.CurrentGame.HandingItem = new HandableItem() { SecurityClearance = 0, ItemID = "EMPTY" };
            }
        }
        public void ApplyItem(GameObject item)
        {
            GameInfo.CurrentGame.HandingItem = item.GetComponent<HandableItem>();
            CurrentItem = item.GetComponent<IHandItem>();
            if (CurrentItem == null) isCurrentItemV3Enabled = false;
            else isCurrentItemV3Enabled = CurrentItem.IsFPSSystemV3Enabled();
            item.SetActive(true);
            try
            {

                if (isCurrentItemV3Enabled == false)
                {
                    GameInfo.CurrentGame.FirstPerson.Crosshair.sprite = GameInfo.CurrentGame.FirstPerson.DefaultCrosshair;
                    GameInfo.CurrentGame.FirstPerson.ViewportShakingIntensity = new Vector3(3, 0, 0);
                }
                else
                {
                    GameInfo.CurrentGame.FirstPerson.ViewportShakingIntensity = CurrentItem.GetOverriddenViewportShakingIntensity();
                    if (CurrentItem.IsCrosshairOverridden() == false)
                    {
                        GameInfo.CurrentGame.FirstPerson.Crosshair.sprite = GameInfo.CurrentGame.FirstPerson.DefaultCrosshair;
                    }
                    else
                    {
                        GameInfo.CurrentGame.FirstPerson.Crosshair.sprite = CurrentItem.GetOverriddenCrosshair();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
    public enum BagItemType
    {
        Primary,
        Secondary,
        Keycard,
        FirstAidKit,
        MobilePhone,
    }
}
