using CLUNL.Data.Layer0.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.IO
{
    /// <summary>
    /// EndPoint for new save system.
    /// </summary>
    public class SaveSystemEndPoint : MonoBehaviour
    {
        public EndPointType EndPointType;

        public void Deserialize(ByteBuffer buffer)
        {

        }

        public ByteBuffer Serialize()
        {
            return null;
        }
    }
    public enum EndPointType
    {
        /// <summary>
        /// Save data will include current object.
        /// </summary>
        Inclusive,
        /// <summary>
        /// Save data will not include current object.
        /// </summary>
        Exclusive
    }
}