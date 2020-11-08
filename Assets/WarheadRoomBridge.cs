using CLUNL.Data.Layer0.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Stories
{
    public class WarheadRoomBridge : SCPInteractive, IByteBufferable
    {
        bool isBridgeOn = false;
        public float TotalTime = 2.5f;
        public GameObject Bridge;
        public Vector3 OriginPosition;
        public Vector3 TargetPosition;
        public void Deserialize(ByteBuffer buffer)
        {
            DataBuffer dataBuffer = DataBuffer.FromByteBuffer(buffer);
            isBridgeOn = dataBuffer.ReadBool();
            if (isBridgeOn)
            {
                Bridge.transform.localPosition = TargetPosition;
            }
            else
                Bridge.transform.localPosition = OriginPosition;
        }

        public ByteBuffer Serialize()
        {
            DataBuffer dataBuffer = new DataBuffer();
            dataBuffer.WriteBool(isBridgeOn);
            return dataBuffer.ObtainByteArray();
        }

        public override IEnumerator Move()
        {
            isOperating = true;
            if (isBridgeOn == false)
            {
                isBridgeOn = true;
                var Length = TargetPosition - OriginPosition;
                var Speed = Length / TotalTime;
                float timed = 0;
                while (timed < TotalTime)
                {
                    Bridge.transform.localPosition+= Speed * Time.deltaTime;
                    timed += Time.deltaTime;
                    yield return null;
                }
                Bridge.transform.localPosition = TargetPosition;
            }
            yield return null;
            isOperating = false;
        }
    }

}