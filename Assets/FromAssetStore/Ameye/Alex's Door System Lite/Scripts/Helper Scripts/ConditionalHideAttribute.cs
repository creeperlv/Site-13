using UnityEngine;
using System;
using System.Collections;

//Original version of the ConditionalHideAttribute created by Brecht Lecluyse (www.brechtos.com)
//Modified by: Alexander Ameye

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class ConditionalHideAttribute : PropertyAttribute
{
    public string ConditionalSourceField = ""; //Based on what condition the field should appear/dissapear
    public string ConditionalSourceField2 = "";
    public bool HideInInspector = false;
    public bool Inverse = false;


    public ConditionalHideAttribute(string conditionalSourceField)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.HideInInspector = false;
        this.Inverse = false;
    }

    public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.HideInInspector = hideInInspector;
        this.Inverse = false;
    }

    public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector, bool inverse)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.HideInInspector = hideInInspector;
        this.Inverse = inverse;
    }

    public ConditionalHideAttribute(bool hideInInspector = false)
    {
        this.ConditionalSourceField = "";
        this.HideInInspector = hideInInspector;
        this.Inverse = false;
    }

    public ConditionalHideAttribute(string conditionalSourceField, string conditionalSourceField2, bool hideInInspector, bool inverse)
    {
      this.ConditionalSourceField = conditionalSourceField;
      this.ConditionalSourceField2 = conditionalSourceField2;
      this.HideInInspector = hideInInspector;
      this.Inverse = inverse;
    }

    public ConditionalHideAttribute(string conditionalSourceField, string conditionalSourceField2)
    {
      this.ConditionalSourceField = conditionalSourceField;
      this.ConditionalSourceField2 = conditionalSourceField2;
      this.HideInInspector = false;
      this.Inverse = false;
    }

    public ConditionalHideAttribute(string conditionalSourceField, string conditionalSourceField2,bool hideInInspector)
    {
      this.ConditionalSourceField = conditionalSourceField;
      this.ConditionalSourceField2 = conditionalSourceField2;
      this.HideInInspector = hideInInspector;
      this.Inverse = false;
    }
}
