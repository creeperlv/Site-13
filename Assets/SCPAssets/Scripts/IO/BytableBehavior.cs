using CLUNL.Data.Layer0.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.IO
{
    /// <summary>
    /// Base class for ModularSaveSystem.
    /// </summary>
    public abstract class BytableBehavior : MonoBehaviour,IByteBufferable
    {
        public abstract void Deserialize(ByteBuffer Data);
        public abstract ByteBuffer Serialize();
    }

}