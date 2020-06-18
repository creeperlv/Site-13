using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedPanel : MonoBehaviour
{
    public float Alpha =1.0f;
    CanvasRenderer CRenderer;
    // Start is called before the first frame update
    void Start()
    {
        CRenderer = GetComponent<CanvasRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //CRenderer.
        Debug.Log(CRenderer.GetInheritedAlpha());
        if (CRenderer.GetAlpha() != Alpha)
        {
            CRenderer.SetAlpha(Alpha);
            Texture texture = Texture2D.whiteTexture;
            CRenderer.SetAlphaTexture(texture);

        }
    }
}
