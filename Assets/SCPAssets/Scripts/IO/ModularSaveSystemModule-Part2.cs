using CLUNL.Data.Layer0.Buffers;
using CLUNL.DirectedIO;
using Site13Kernel.DynamicScene;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
namespace Site13Kernel.IO
{
    public partial class ModularSaveSystemModule
    {
        public void ResolveTransform(ref Transform transform, ref ByteBuffer buffer)
        {
            var t=Utilities.FromBytes(buffer.GetGroup());
            transform.position = t.Item1;
            transform.rotation = t.Item2;
            transform.localScale = t.Item3;
        }
    }
}