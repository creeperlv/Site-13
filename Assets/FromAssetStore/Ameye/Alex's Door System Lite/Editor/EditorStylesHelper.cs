// EditorStylesHelper.cs
// Created by Alexander Ameye
// Version 1.1.0

using UnityEngine;
using UnityEditor;

namespace StylesHelper
{
    public static class Styles
    {
        internal static GUIStyle helpbox;
        internal static GUIStyle centeredVersionLabel;
        internal static GUIStyle helpIcon;

        internal static GUIContent Forum;
        internal static GUIContent Documentation;
        internal static GUIContent Contact;
        internal static GUIContent Twitter;
        internal static GUIContent Review;
        internal static GUIContent Changelog;
        internal static GUIContent HelpIcon;
        internal static GUIContent NewIcon;
        internal static GUIContent VersionLabel;
        internal static GUIContent errordetection;

        // Error Detection
        internal static GUIContent PlayerTagTrue;
        internal static GUIContent PlayerTagFalse;

        internal static GUIContent DetectionTrue;
        internal static GUIContent DetectionFalse;
        internal static GUIContent DetectionUnknown;

        internal static GUIContent ReachTrue;
        internal static GUIContent ReachFalse;
        internal static GUIContent ReachUnknown;

        internal static GUIContent TagTrue;
        internal static GUIContent TagFalse;



        static Styles()
        {
            Forum = IconContent("<size=11><b> Support Forum</b></size>", "forum", "");
            Documentation = IconContent("<size=11><b> Online Documentation</b></size>", "documentation", "");
            Contact = IconContent("<size=11><b> Contact</b></size>", "contact", "");
            Review = IconContent("<size=11><b> Rate and Review</b></size>", "rateandreview", "");
            Twitter = IconContent("<size=11><b> Twitter</b></size>", "twitter", "");
            Changelog = IconContent("<size=11><b> Changelog</b></size>", "new", "");

            NewIcon = IconContent(" Whats new?", "new", "");
            errordetection = IconContent(" Detect errors!", "errordetection", "");
            VersionLabel = IconContent("Version 3.0.0", "", "");
            HelpIcon = IconContent("", "help", "Need help?");

            PlayerTagTrue = IconContent(" Player", "true", "");
            PlayerTagFalse = IconContent(" Player", "false", "");

            DetectionTrue = IconContent(" Detection", "true", "");
            DetectionFalse = IconContent(" Detection", "false", "");
            DetectionUnknown = IconContent(" Detection", "help", "");

            ReachTrue = IconContent(" Reach", "true", "");
            ReachFalse = IconContent(" Reach", "false", "");
            ReachUnknown = IconContent(" Reach", "help", "");

            TagTrue = IconContent(" Door", "true", "");
            TagFalse = IconContent(" Door", "false", "");

            helpbox = new GUIStyle(EditorStyles.helpBox)
            {
                alignment = TextAnchor.MiddleLeft,
                richText = true
            };

            centeredVersionLabel = new GUIStyle(EditorStyles.centeredGreyMiniLabel)
            {
                alignment = TextAnchor.MiddleCenter,
                padding = new RectOffset(0, 0, 0, 0),
                margin = new RectOffset(0, 0, 0, 0)
            };

            helpIcon = new GUIStyle(EditorStyles.centeredGreyMiniLabel)
            {
                alignment = TextAnchor.MiddleRight,
                padding = new RectOffset(0, 0, 0, 0),
                margin = new RectOffset(0, 0, 0, 0)
            };
        }

        static GUIContent IconContent(string text, string icon, string tooltip)
        {
            Texture2D cached = EditorGUIUtility.Load("Assets/Ameye/Alex's Door System Lite/Icons/" + icon + ".png") as Texture2D;
            return new GUIContent(text, cached, tooltip);
        }
    }
}
