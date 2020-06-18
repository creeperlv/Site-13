// DoorDetectionLiteEditor.cs
// Created by Alexander Ameye
// Version 3.0.0

using UnityEngine;
using StylesHelper;
using UnityEditor;

[CustomEditor(typeof(DoorDetectionLite)), CanEditMultipleObjects]
public class DoorDetectionLiteEditor : Editor
{
    SerializedProperty isTemporaryDisabled, reachProp, characterProp;
    SerializedProperty ReadingPanel,Reading, RealReading;

    public void OnEnable() { 
        reachProp = serializedObject.FindProperty("Reach");
        isTemporaryDisabled = serializedObject.FindProperty("IsTemporaryDisabled");
        characterProp = serializedObject.FindProperty("Character");
        ReadingPanel = serializedObject.FindProperty("ReadingPanel");
        Reading = serializedObject.FindProperty("Reading");
        RealReading = serializedObject.FindProperty("RealReading");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUIStyle style = new GUIStyle()
        {
            richText = true
            
        };

        EditorGUILayout.LabelField("<size=24>SCP Interactive Detector</size>", style);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("<size=18>Settings</size>", style);
        EditorGUILayout.LabelField("<size=16>Operating</size>", style);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(reachProp, new GUIContent("Reach", "How close the player has to be in order to be able to operate objects."));
        EditorGUILayout.PropertyField(characterProp, new GUIContent("Character"));
        EditorGUILayout.PropertyField(isTemporaryDisabled, new GUIContent("Is Temporary Disabled"));
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("<size=16>Reading</size>", style);
        EditorGUILayout.PropertyField(ReadingPanel, new GUIContent("Reading Panel"));
        EditorGUILayout.PropertyField(Reading, new GUIContent("Reading Image"));
        EditorGUILayout.PropertyField(RealReading, new GUIContent("Real Reading Panel"));
        serializedObject.ApplyModifiedProperties();


        if (Application.isPlaying) return;

        serializedObject.ApplyModifiedProperties();
    }
}
