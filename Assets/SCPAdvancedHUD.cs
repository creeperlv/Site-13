using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Obsolete]
public class SCPAdvancedHUD : MonoBehaviour
{
    public float RefreshInterval = .2f;
    public List<HUDObjectBinding> HUDObjects;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    float Timed=0;
    // Update is called once per frame
    void Update()
    {
        if (Timed > RefreshInterval)
        {
            foreach (var item in HUDObjects)
            {
                var position=Camera.main.WorldToScreenPoint(item.OriginObj.position);
                position.z = 0;
                
                var rt=item.OnScreenObj.transform;
                rt.position = position;
            }
            Timed = 0;
        }
        Timed += Time.deltaTime;
    }
}
[Serializable]
public class HUDObjectBinding
{
    public Transform OriginObj;
    public GameObject OnScreenObj;
}