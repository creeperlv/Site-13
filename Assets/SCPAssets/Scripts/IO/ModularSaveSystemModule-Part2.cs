using CLUNL.Data.Layer0.Buffers;
using CLUNL.Data.Serializables.CheckpointSystem;
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
    public partial class ModularSaveSystemModule: ICheckpointData
    {
        public void ResolveTransform(ref Transform transform, ref ByteBuffer buffer)
        {
            var t=Utilities.FromBytes(buffer.GetGroup());
            transform.position = t.Item1;
            transform.rotation = t.Item2;
            transform.localScale = t.Item3;
        }
        public string GetName()
        {
            throw new NotImplementedException();
        }

        List<object> ICheckpointData.Save()
        {
            throw new NotImplementedException();
        }

        public void Load(List<object> data)
        {
            throw new NotImplementedException();
        }
    }
}