using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public class SCPBagSystem : MonoBehaviour
    {
        public List<HandableItem> Items;
        // Start is called before the first frame update
        void Start()
        {
            SetItem();
        }
        int currentItem = 0;
        private void Update()
        {
            if (Input.GetButton("ControllerNextItem"))
            {
                NextItem();
                return;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > .5f)
                NextItem();
            if (Input.GetAxis("Mouse ScrollWheel") < -.5f)
                PreviousItem();
            {
                if (Input.GetKey(KeyCode.Alpha1))
                {
                    if (GameInfo.CurrentGame.EquippedItems[0] == true)
                        currentItem = 0;
                    SetItem();
                }
                else
                if (Input.GetKey(KeyCode.Alpha2))
                {
                    if (GameInfo.CurrentGame.EquippedItems[1] == true)
                        currentItem = 1;
                    SetItem();
                }
                else
                if (Input.GetKey(KeyCode.Alpha3))
                {
                    if (GameInfo.CurrentGame.EquippedItems[2] == true)
                        currentItem = 2;
                    SetItem();
                }
                else
                if (Input.GetKey(KeyCode.Alpha4))
                {
                    if (GameInfo.CurrentGame.EquippedItems[3] == true)
                        currentItem = 3;
                    SetItem();
                }
                else
                if (Input.GetKey(KeyCode.Alpha5))
                {
                    if (GameInfo.CurrentGame.EquippedItems[4] == true)
                        currentItem = 4;
                    SetItem();
                }
                else
                if (Input.GetKey(KeyCode.Alpha6))
                {
                    if (GameInfo.CurrentGame.EquippedItems[5] == true)
                        currentItem = 5;
                    SetItem();
                }
                else
                if (Input.GetKey(KeyCode.Alpha7))
                {
                    if (GameInfo.CurrentGame.EquippedItems[6] == true)
                        currentItem = 6;
                    SetItem();
                }
            }
        }
        void SetItem()
        {
            try
            {
                foreach (var item in Items)
                {
                    item.gameObject.SetActive(false);
                }
                GameInfo.CurrentGame.HandingItem = Items[currentItem];
                Items[currentItem].gameObject.SetActive(true);
            }
            catch (System.Exception)
            {
            }
        }
        void NextItem()
        {
            try
            {
                if (GameInfo.CurrentGame.EquippedItems[currentItem + 1] == false)
                {
                    currentItem++; NextItem(); return;
                }
            }
            catch (System.Exception)
            {

            }
            try
            {
                if (GameInfo.CurrentGame.EquippedItems[0] == false)
                {
                    currentItem++; NextItem(); return;
                }
            }
            catch (System.Exception)
            {

            }
            currentItem++;
            if (currentItem < Items.Count)
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
            try
            {
                if (GameInfo.CurrentGame.EquippedItems[currentItem - 1] == false)
                {
                    currentItem--; PreviousItem(); return;
                }
            }
            catch (System.Exception)
            {

            }
            try
            {
                if (currentItem - 1 < 0)
                    if (GameInfo.CurrentGame.EquippedItems[Items.Count - 1] == false)
                    {
                        currentItem=Items.Count-1; PreviousItem(); return;
                    }
            }
            catch (System.Exception)
            {

            }
            currentItem--;
            if (currentItem < 0)
            {
                currentItem = Items.Count - 1;
            }
            else
            {
            }
            SetItem();
        }
    }
}
