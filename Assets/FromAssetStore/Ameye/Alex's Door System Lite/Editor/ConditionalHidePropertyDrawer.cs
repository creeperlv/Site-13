using UnityEngine;
using UnityEditor;

//Original version of the ConditionalHideAttribute created by Brecht Lecluyse (www.brechtos.com)
//Modified by: Alexander Ameye

[CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
public class ConditionalHidePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ConditionalHideAttribute CondHideAtt = (ConditionalHideAttribute)attribute;
        bool enabled = GetConditionalHideAttributeResult(CondHideAtt, property);

        bool wasEnabled = GUI.enabled;
        GUI.enabled = enabled;
        if (!CondHideAtt.HideInInspector || enabled) EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = wasEnabled;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ConditionalHideAttribute CondHideAtt = (ConditionalHideAttribute)attribute;
        bool enabled = GetConditionalHideAttributeResult(CondHideAtt, property);

        if (!CondHideAtt.HideInInspector || enabled) return EditorGUI.GetPropertyHeight(property, label);

        else
        {
            //The property is not being drawn
            //We want to undo the spacing added before and after the property
            return -EditorGUIUtility.standardVerticalSpacing;
            //return 0.0f;
        }


        /*
        //Get the base height when not expanded
        var height = base.GetPropertyHeight(property, label);

        // if the property is expanded go thru all its children and get their height
        if (property.isExpanded)
        {
            var propEnum = property.GetEnumerator();
            while (propEnum.MoveNext())
                height += EditorGUI.GetPropertyHeight((SerializedProperty)propEnum.Current, GUIContent.none, true);
        }
        return height;*/
    }

    private bool GetConditionalHideAttributeResult(ConditionalHideAttribute CondHideAtt, SerializedProperty property)
    {
        bool enabled = false;

        //Get the full relative property path of the sourcefield so we can have nested hiding
        string propertyPath = property.propertyPath; //returns the property path of the property we want to apply the attribute to
        string conditionPath = propertyPath.Replace(property.name, CondHideAtt.ConditionalSourceField); //changes the path to the conditionalsource property path
        SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);
        //SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(CondHideAtt.ConditionalSourceField);

        if (sourcePropertyValue != null)
        {
            enabled = CheckPropertyType(sourcePropertyValue);
        }
        else
        {
            //Debug.LogWarning("Attempting to use a ConditionalHideAttribute but no matching SourcePropertyValue found in object: " + CondHideAtt.ConditionalSourceField);
        }

        conditionPath = propertyPath.Replace(property.name, CondHideAtt.ConditionalSourceField2); //changes the path to the conditionalsource property path
        SerializedProperty sourcePropertyValue2 = property.serializedObject.FindProperty(conditionPath);
        //SerializedProperty sourcePropertyValue2 = property.serializedObject.FindProperty(CondHideAtt.ConditionalSourceField2);
        if (sourcePropertyValue2 != null)
        {
            enabled = enabled && CheckPropertyType(sourcePropertyValue2);
        }
        else
        {
            //Debug.LogWarning("Attempting to use a ConditionalHideAttribute but no matching SourcePropertyValue found in object: " + CondHideAtt.ConditionalSourceField);
        }

        if (CondHideAtt.Inverse) enabled = !enabled;

        return enabled;
    }


    //CHECK WHAT PROPERTY TYPE THERE IS USED AS A SOURCE
    private bool CheckPropertyType(SerializedProperty sourcePropertyValue)
    {
        switch (sourcePropertyValue.propertyType)
        {
            case SerializedPropertyType.Boolean:
                return sourcePropertyValue.boolValue;
            case SerializedPropertyType.ObjectReference:
                return sourcePropertyValue.objectReferenceValue != null;
            //This is hacky, won't work if more than 3 elements in the neum
            case SerializedPropertyType.Enum:
                return sourcePropertyValue.enumValueIndex == 0;


            default:
                Debug.LogError("Data type of the property used for conditional hiding [" + sourcePropertyValue.propertyType + "] is currently not supported");
                return true;
        }
    }


}
