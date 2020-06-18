using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel{

    public class SCPReading : SCPInteractive
    {
        public Sprite ReadingMaterial;
        // Start is called before the first frame update
        void Start()
        {
            isRequireLockCamera = true;
        }

        public override IEnumerator Move(GameObject Player)
        {
            Player.GetComponent<DoorDetectionLite>().Reading.sprite=ReadingMaterial;
            yield break;
        }
    }

}