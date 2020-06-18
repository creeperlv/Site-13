// ChangeLog.cs
// Created by Alexander Ameye
// Version 1.1.0

using UnityEngine;
using UnityEditor;
using StylesHelper;

public class ChangeLog : EditorWindow
{
    Vector2 scrollPos;
    static void ShowWindow()
    {
        ChangeLog myWindow = new ChangeLog();
        myWindow.ShowUtility();
        myWindow.titleContent = new GUIContent("Version Changes");
        GetWindow(typeof(SupportWindow)).Close();
    }

    public static bool ThreeDotZeroFold
    {
        get { return EditorPrefs.GetBool("ThreeDotZeroFold", false); }
        set { EditorPrefs.SetBool("ThreeDotZeroFold", value); }
    }

    public static bool TwoDotFourFold
    {
        get { return EditorPrefs.GetBool("TwoDotFourFold", false); }
        set { EditorPrefs.SetBool("TwoDotFourFold", value); }
    }


    static GUIStyle _foldoutStyle;
    static GUIStyle FoldoutStyle
    {
        get
        {
            if (_foldoutStyle == null)
            {
                _foldoutStyle = new GUIStyle(EditorStyles.foldout)
                {
                    font = EditorStyles.boldFont
                };
            }
            return _foldoutStyle;
        }
    }

    static GUIStyle _boxStyle;
    public static GUIStyle BoxStyle
    {
        get
        {
            if (_boxStyle == null)
            {
                _boxStyle = new GUIStyle(EditorStyles.helpBox);
            }
            return _boxStyle;
        }
    }

    void OnGUI()
    {
        ChangeLog myWindow = (ChangeLog)GetWindow(typeof(ChangeLog));
        myWindow.minSize = new Vector2(350, 350);
        myWindow.maxSize = myWindow.minSize;
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false);

        EditorGUILayout.Space();
        GUILayout.Label(Styles.NewIcon, Styles.centeredVersionLabel);
        EditorGUILayout.Space();

        ThreeDotZeroFold = BeginFold("New in version 3.0.0 (March 2018)", ThreeDotZeroFold);
        if (ThreeDotZeroFold)
        {
            EditorGUILayout.LabelField("• Reworked asset and forum page");
            EditorGUILayout.LabelField("• Improved codebase");
            EditorGUILayout.LabelField("• Bug fixes and general improvements");
        }
        EndFold();

        TwoDotFourFold = BeginFold("New in version 2.4.0 (December 2017)", TwoDotFourFold);
        if (TwoDotFourFold)
        {
            EditorGUILayout.LabelField("• Completely re-written rotation code");
            EditorGUILayout.LabelField("• Migrated documentation to MkDocs");
            EditorGUILayout.LabelField("• Improved codebase");
            EditorGUILayout.LabelField("• Bug fixes and general improvements");
        }
        EndFold();

        EditorGUILayout.EndScrollView();
    }

    public static bool BeginFold(string foldName, bool foldState)
    {
        EditorGUILayout.BeginVertical(BoxStyle);
        GUILayout.Space(3);
        foldState = EditorGUI.Foldout(EditorGUILayout.GetControlRect(),
        foldState, foldName, true, FoldoutStyle);
        if (foldState) GUILayout.Space(3);
        return foldState;
    }

    public static void EndFold()
    {
        GUILayout.Space(3);
        EditorGUILayout.EndVertical();
        GUILayout.Space(0);
    }
}
