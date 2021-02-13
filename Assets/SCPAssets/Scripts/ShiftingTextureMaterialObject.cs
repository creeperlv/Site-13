using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Animations
{

    public class ShiftingTextureMaterialObject : MonoBehaviour
    {
        Material TargetMaterial;
        // Start is called before the first frame update
        void Start()
        {
            TargetMaterial = this.GetComponent<MeshRenderer>().material;
        }
        public float SpeedX;
        public float SpeedY;
        public float TimeGap = 0;
        float passedT=0;
        // Update is called once per frame
        void Update()
        {
            passedT += Time.deltaTime;
            if (passedT > TimeGap)
            {
                if(TimeGap==0)
                TargetMaterial.mainTextureOffset += new Vector2(SpeedX * Time.deltaTime, SpeedY * Time.deltaTime);
                else
                TargetMaterial.mainTextureOffset += new Vector2(SpeedX * TimeGap, SpeedY * TimeGap);
            }
        }
    }

}