using CLUNL.Data.Layer0.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public class SCPStoryNodeBaseCode : SCPBaseScript,IByteBufferable
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

        public void Deserialize(ByteBuffer buffer)
        {
            DataBuffer db=DataBuffer.FromByteBuffer(buffer);
            isStarted = db.ReadBool();

        }

        public ByteBuffer Serialize()
        {
            DataBuffer dataBuffer = new DataBuffer();
            dataBuffer.WriteBool(isStarted);
            return dataBuffer.ObtainByteArray();
        }
    }
}
