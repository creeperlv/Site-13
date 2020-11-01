using CLUNL.Data.Layer0.Buffers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.IO
{
    public class GameObjectActivityPoint : MonoBehaviour, IByteBufferable
    {
        private void Start()
        {
            
        }
        public void Deserialize(ByteBuffer buffer)
        {
            DataBuffer dataBuffer = DataBuffer.FromByteBuffer(buffer);
            this.gameObject.SetActive(dataBuffer.ReadBool());
        }

        public ByteBuffer Serialize()
        {
            DataBuffer dataBuffer = new DataBuffer();
            dataBuffer.WriteBool(this.gameObject.activeSelf);
            return dataBuffer.ObtainByteArray();
        }
    }

}