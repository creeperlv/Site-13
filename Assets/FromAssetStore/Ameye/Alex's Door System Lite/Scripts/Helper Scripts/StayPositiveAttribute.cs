using System.Collections;

using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

#endif



///

///

///		Add [StayPositive] in front of a variable and the editor won't let it become a negative number.

///

///			[StayPositive] public float foo;

///

///		Works for float, int and vectors

///

///		by Fernando Ramallo byfernando.com

public class StayPositiveAttribute : PropertyAttribute { }



#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(StayPositiveAttribute))]

public class StayPositiveAttributeEditor : PropertyDrawer
{



    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        EditorGUI.BeginChangeCheck();

        EditorGUI.PropertyField(position, property, label);

        if (EditorGUI.EndChangeCheck())
        {

            switch (property.propertyType)
            {

                case SerializedPropertyType.Float:

                    property.floatValue = Mathf.Clamp(property.floatValue, 0, float.PositiveInfinity);

                    break;

                case SerializedPropertyType.Integer:

                    property.intValue = (int)Mathf.Clamp(property.intValue, 0, float.PositiveInfinity);

                    break;

                case SerializedPropertyType.Vector2:

                    property.vector2Value = new Vector2(

                            Mathf.Clamp(property.vector2Value.x, 0, float.PositiveInfinity),

                            Mathf.Clamp(property.vector2Value.y, 0, float.PositiveInfinity)

                        );

                    break;

                case SerializedPropertyType.Vector3:

                    property.vector3Value = new Vector3(

                            Mathf.Clamp(property.vector3Value.x, 0, float.PositiveInfinity),

                            Mathf.Clamp(property.vector3Value.y, 0, float.PositiveInfinity),

                            Mathf.Clamp(property.vector3Value.z, 0, float.PositiveInfinity)

                        );

                    break;

                case SerializedPropertyType.Vector4:

                    property.vector4Value = new Vector4(

                            Mathf.Clamp(property.vector4Value.x, 0, float.PositiveInfinity),

                            Mathf.Clamp(property.vector4Value.y, 0, float.PositiveInfinity),

                            Mathf.Clamp(property.vector4Value.z, 0, float.PositiveInfinity),

                            Mathf.Clamp(property.vector4Value.w, 0, float.PositiveInfinity)

                        );

                    break;

#if UNITY_2017_2_OR_NEWER

				case SerializedPropertyType.Vector2Int:

				property.vector2IntValue = new Vector2Int(

						(int)Mathf.Clamp(property.vector2IntValue.x, 0, float.PositiveInfinity),

						(int)Mathf.Clamp(property.vector2IntValue.y, 0, float.PositiveInfinity)

					);

				break;

				case SerializedPropertyType.Vector3Int:

				property.vector3IntValue = new Vector3Int(

						(int)Mathf.Clamp(property.vector3IntValue.x, 0, float.PositiveInfinity),

						(int)Mathf.Clamp(property.vector3IntValue.y, 0, float.PositiveInfinity),

						(int)Mathf.Clamp(property.vector3IntValue.z, 0, float.PositiveInfinity)

					);

				break;

#endif

            }

        }

    }

    override public float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {

        return EditorGUI.GetPropertyHeight(property, label);

    }

}

#endif