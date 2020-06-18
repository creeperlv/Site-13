using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public class RandomRoomProp : MonoBehaviour
    {
        public List<GameObject> Props;
        // Start is called before the first frame update
        void Start()
        {
            var ExSeed = transform.GetInstanceID();
            System.Random random = new System.Random(GameInfo.CurrentGame.BaseSeed + ExSeed);
            foreach (var item in Props)
            {

                bool willShow = false;
                if (random.Next(0, 100) > 60) willShow = true;
                //item.transform.GetChild(i).gameObject.SetActive(willShow);
                item.SetActive(willShow);
                //for (int i = 0; i < item.transform.childCount; i++)
                //{
                //}
            }
        }

    }

}