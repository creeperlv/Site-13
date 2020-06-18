using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialHolder : MonoBehaviour
{
    public Renderer material;
    
    public float CR;
    public float CG;
    public float CB;
    public float ER;
    public float EG;
    public float EB;
    private void Update()
    {
        {
            var c = material.sharedMaterial.color;
            c.r = CR;
            c.g = CG;
            c.b = CB;
            material.sharedMaterial.color = c;
        }
        {
            Color color = new Color(ER, EG, EB);
            material.sharedMaterial.SetColor("_EmissionColor",color);
        }
        
    }
}
