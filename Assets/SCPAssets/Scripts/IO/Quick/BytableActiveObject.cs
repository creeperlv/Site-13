using CLUNL.Data.Layer0.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.IO.Quick
{

    public class BytableActiveObject : BytableBehavior
    {
        public GameObject TargetObject;
        public bool DefaultActive=true;
        public void Start()
        {
            TargetObject.SetActive(DefaultActive);
        }
        public override void Deserialize(ByteBuffer Data)
        {
            if (Data.GetGroup()[0] == 1)
            {
                TargetObject.SetActive(true);
            }
            else
            {
                TargetObject.SetActive(false);
            }
        }

        public override ByteBuffer Serialize()
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.AppendGroup(new[] { (byte)(TargetObject.activeSelf?1:0) });
            return buffer;
        }
    }

}