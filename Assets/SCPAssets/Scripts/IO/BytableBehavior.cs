using CLUNL.Data.Layer0.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BytableBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public virtual void Deserialize(ByteBuffer Data)
    {

    }
    public virtual ByteBuffer Serialize() => null;
    // Update is called once per frame
    void Update()
    {
        
    }
}
