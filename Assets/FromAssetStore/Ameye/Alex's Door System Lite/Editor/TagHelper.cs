// TagHelper.cs
// Created by Alexander Ameye
// Version 3.0.0

using UnityEditor;
using UnityEngine;

namespace Tagger
{
    public static class TagHelper
    {
        [MenuItem("Tools/Alex's Door System Lite/Create Door Tag")]
        public static void AddDoorTag()
        {
            Object[] asset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset");
            if ((asset != null) && (asset.Length > 0))
            {
                SerializedObject so = new SerializedObject(asset[0]);
                SerializedProperty tags = so.FindProperty("tags");

                for (int i = 0; i < tags.arraySize; ++i)
                {
                    if (tags.GetArrayElementAtIndex(i).stringValue == "Door")
                    {
                        Debug.Log("Tag 'Door' already exists.");
                        return;
                    }
                }

                tags.InsertArrayElementAtIndex(tags.arraySize);
                tags.GetArrayElementAtIndex(tags.arraySize - 1).stringValue = "Door";
                so.ApplyModifiedProperties();
                so.Update();

                Debug.Log("Tag 'Door' was created.");
            }
        }

        public static bool DoesDoorTagNotExist()
        {
            Object[] asset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset");

            if ((asset != null) && (asset.Length > 0))
            {
                SerializedObject so = new SerializedObject(asset[0]);
                SerializedProperty tags = so.FindProperty("tags");

                for (int i = 0; i < tags.arraySize; ++i)
                {
                    if (tags.GetArrayElementAtIndex(i).stringValue == "Door")
                        return false;
                }
            }

            return true;
        }
    }
}

