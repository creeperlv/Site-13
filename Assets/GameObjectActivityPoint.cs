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
            var b = buffer.GetGroup();
            gameObject.SetActive(BitConverter.ToBoolean(b, 0));
        }

        public ByteBuffer Serialize()
        {
            ByteBuffer vs = new ByteBuffer();
            byte[] b = BitConverter.GetBytes(this.gameObject.activeSelf);
            vs.AppendGroup(vs);
            return vs;
        }
    }

}