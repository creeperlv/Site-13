// DoorRotationLiteEditor.cs
// Created by Alexander Ameye
// Version 3.0.0

using UnityEngine;
using StylesHelper;
using UnityEditor;

[CustomEditor(typeof(DoorRotationLite)), CanEditMultipleObjects]
public class DoorRotationLiteEditor : Editor
{
    int ToolBarIndex;

    SerializedProperty doorScaleProp, pivotPositionProp, hingePositionProp, visualizeHingeProp, hingeColorProp;
    SerializedProperty initialAngleProp, rotationAngleProp, rotationSideProp, speedProp, timesMoveableProp;


    public void OnEnable()
    {
        ToolBarIndex = 1;

        doorScaleProp = serializedObject.FindProperty("DoorScale");
        pivotPositionProp = serializedObject.FindProperty("PivotPosition");
        hingePositionProp = serializedObject.FindProperty("HingePosition");
        visualizeHingeProp = serializedObject.FindProperty("VisualizeHinge");
        hingeColorProp = serializedObject.FindProperty("HingeColor");

        initialAngleProp = serializedObject.FindProperty("InitialAngle");
        rotationAngleProp = serializedObject.FindProperty("RotationAngle");
        rotationSideProp = serializedObject.FindProperty("RotationSide");
        speedProp = serializedObject.FindProperty("Speed");
        timesMoveableProp = serializedObject.FindProperty("TimesMoveable");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DoorRotationLite DoorRotationLite = target as DoorRotationLite;

        string[] menuOptions = new string[2];
        menuOptions[0] = "Door";
        menuOptions[1] = "Rotations";

        EditorGUILayout.Space();

        ToolBarIndex = GUILayout.Toolbar(ToolBarIndex, menuOptions);

        GUIStyle style = new GUIStyle()
        {
            richText = true
        };

        switch (ToolBarIndex)
        {
            //Door and hinge settings
            case 0:
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("<b>Door Settings</b>", style);
                EditorGUILayout.PropertyField(doorScaleProp, new GUIContent("Scale"));
                EditorGUILayout.PropertyField(pivotPositionProp, new GUIContent("Pivot Position"));
                EditorGUILayout.Space();
                if (DoorRotationLite.DoorScale == DoorRotationLite.ScaleOfDoor.Unity3DUnits && DoorRotationLite.PivotPosition == DoorRotationLite.PositionOfPivot.Centered)
                {
                    EditorGUILayout.LabelField("<b>Hinge Settings</b>", style);
                    EditorGUILayout.PropertyField(hingePositionProp, new GUIContent("Position"));
                    EditorGUILayout.Space();
                }

                if (DoorRotationLite.DoorScale == DoorRotationLite.ScaleOfDoor.Other && DoorRotationLite.PivotPosition == DoorRotationLite.PositionOfPivot.Centered)
                    EditorGUILayout.HelpBox("If your door is not scaled in Unity3D units and the pivot position is not already positioned correctly, the hinge algorithm will not work as expected.", MessageType.Error);

                else if (Tools.pivotMode == PivotMode.Center)
                    EditorGUILayout.HelpBox("Make sure the tool handle is placed at the active object's pivot point.", MessageType.Warning);

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("<b>Debug Settings</b>", style);
                EditorGUILayout.PropertyField(visualizeHingeProp, new GUIContent("Visualize Hinge", "Visualizes the position of the hinge in-game by a colored cube."));
                if (DoorRotationLite.VisualizeHinge) EditorGUILayout.PropertyField(hingeColorProp, new GUIContent("Hinge Color", "The color of the visualization of the hinge."));

                EditorGUILayout.Space();
                EditorGUILayout.LabelField(Styles.VersionLabel, Styles.centeredVersionLabel);
                serializedObject.ApplyModifiedProperties();
                break;

            case 1:
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("<b>Rotation Settings</b>", style);
                EditorGUILayout.PropertyField(initialAngleProp, new GUIContent("Initial Angle", "The initial angle of the door/window."));
                EditorGUILayout.PropertyField(rotationAngleProp, new GUIContent("Rotation Angle", "The amount of degrees the door/window rotates."));
                EditorGUILayout.PropertyField(rotationSideProp, new GUIContent("Rotation Side", "Which way the door will rotate."));
                EditorGUILayout.PropertyField(speedProp, new GUIContent("Speed", "The speed of the door."));
                EditorGUILayout.PropertyField(timesMoveableProp, new GUIContent("Times Moveable", "0 = infinite times"));

                EditorGUILayout.Space();
                EditorGUILayout.LabelField(Styles.VersionLabel, Styles.centeredVersionLabel);
                serializedObject.ApplyModifiedProperties();
                break;
            default: break;
        }

        if (Application.isPlaying) return;

        serializedObject.ApplyModifiedProperties();
    }
}
