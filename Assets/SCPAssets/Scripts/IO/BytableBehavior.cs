using CLUNL.Data.Layer0.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.IO
{
    public class BytableBehavior : MonoBehaviour
    {
        public virtual void Deserialize(ByteBuffer Data)
        {

        }
        public virtual ByteBuffer Serialize() => null;
    }

}