using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skip1004Props : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        {
            try
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (Random.Range((int)0, (int)10)>= 5)
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                    else
                    {
                        transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }
            catch (System.Exception)
            {
            }
        }
    }

}
