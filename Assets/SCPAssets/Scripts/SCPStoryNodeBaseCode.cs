using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public class SCPStoryNodeBaseCode : SCPBaseScript
    {
        [HideInInspector]
        public bool isStoryRequiresPlayer = false;
        public virtual void StartStory()
        {

        }
        public virtual void StartStory(GameObject Player)
        {

        }
        bool isColliderIn = false;
        // Update is called once per frame
        void Update()
        {
            if (isColliderIn == true)
            {
                if (isStarted == false)
                {

                    if (isStoryRequiresPlayer == false)
                    {
                        StartStory();
                    }
                    else
                    {
                        try
                        {
                            StartStory(Player);

                        }
                        catch (System.Exception)
                        {
                        }
                    }
                }
            }
        }
        GameObject Player;
        private void OnTriggerEnter(Collider other)
        {
            try
            {
                if (other.gameObject.GetComponent<SCPFirstController>() != null)
                {
                    isColliderIn = true;
                    if (isStoryRequiresPlayer)
                    {
                        Player = other.gameObject;
                    }
                }

            }
            catch (System.Exception)
            {
            }
            try
            {
                SideEnter(other);
            }
            catch
            {
            }
        }
        public virtual void SideEnter(Collider other)
        {
        }

        public virtual void SideExit(Collider other)
        {

            
        }
        private void OnTriggerExit(Collider other)
        {
            try
            {
                if (other.gameObject.GetComponent<SCPFirstController>() != null)
                {
                    isColliderIn = false;
                    SideExit(other);
                }

            }
            catch (System.Exception)
            {
            }
        }
    }
}
