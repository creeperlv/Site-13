// SupportWindow.cs
// Created by Alexander Ameye
// Version 3.0.0

using UnityEngine;
using UnityEditor;
using StylesHelper;

public class SupportWindow : EditorWindow
{
    [MenuItem("Tools/Alex's Door System Lite/Support")]
    public static void ShowWindow()
    {
        GetWindow(typeof(SupportWindow));
        SupportWindow myWindow = (SupportWindow)GetWindow(typeof(SupportWindow));
        myWindow.titleContent = new GUIContent("Support");
        GetWindow(typeof(ChangeLog)).Close();
    }

    public static void Init()
    {
        SupportWindow myWindow = (SupportWindow)GetWindow(typeof(SupportWindow));
        myWindow.Show();
    }

    void OnGUI()
    {
        SupportWindow myWindow = (SupportWindow)GetWindow(typeof(SupportWindow));
        myWindow.minSize = new Vector2(300, 260);
        myWindow.maxSize = myWindow.minSize;

        if (GUILayout.Button(Styles.Forum, Styles.helpbox))
            Application.OpenURL("https://forum.unity.com/threads/v2-4-0-alexs-door-system-lite-free.445297/#post-2880291");

        if (GUILayout.Button(Styles.Documentation, Styles.helpbox))
            Application.OpenURL("https://alexdoorsystem.github.io/liteversion/");

        if (GUILayout.Button(Styles.Contact, Styles.helpbox))
            Application.OpenURL("mailto:alexanderameye@gmail.com?");

        if (GUILayout.Button(Styles.Twitter, Styles.helpbox))
            Application.OpenURL("https://twitter.com/alexanderameye");

        if (GUILayout.Button(Styles.Review, Styles.helpbox))
            Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/account/downloads/search=Alex%20s%20Door%20System");

        if (GUILayout.Button(Styles.Changelog, Styles.helpbox))
        {
            GetWindow(typeof(ChangeLog));
            Close();
        }
    }
}
