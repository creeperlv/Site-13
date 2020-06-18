using UnityEditor;
using UnityEngine;
using Tagger;
using StylesHelper;

public class ErrorWindow : EditorWindow
{
    string infoString = "";

    [MenuItem("Tools/Alex's Door System Lite/Detect Errors")]
    static void ShowWindow()
    {
        ErrorWindow myWindow = new ErrorWindow();
        myWindow.ShowUtility();
        myWindow.titleContent = new GUIContent("Error Detection");
        myWindow.minSize = new Vector2(200, 170);
        myWindow.maxSize = myWindow.minSize;
    }

    void OnGUI()
    {
        bool playerError = false;
        bool detectionError = false;
        bool reachError = false;
        bool tagError = false;

        playerError = GameObject.FindGameObjectWithTag("Player") == null;

        if (!playerError)
        {
            if (GUILayout.Button(Styles.PlayerTagTrue, Styles.helpbox))
                infoString = "An object with the tag 'Player' was found.";

            detectionError = GameObject.FindGameObjectWithTag("Player").GetComponent<DoorDetectionLite>() == null;

            if (!detectionError)
            {
                if (GUILayout.Button(Styles.DetectionTrue, Styles.helpbox))
                    infoString = "The detection script component was found attached to the player.";

                reachError = GameObject.FindGameObjectWithTag("Player").GetComponent<DoorDetectionLite>().Reach == 0;

                if (!reachError)
                {
                    if (GUILayout.Button(Styles.ReachTrue, Styles.helpbox))
                        infoString = "The reach variable is not 0. \n";
                }

                else if (reachError)
                {
                    if (GUILayout.Button(Styles.ReachFalse, Styles.helpbox))
                    {
                        infoString = "The reach variable is 0. \n";
                        EditorGUIUtility.PingObject(GameObject.FindGameObjectWithTag("Player").GetComponent<DoorDetectionLite>());
                    }
                }
            }

            else if (detectionError)
            {
                if (GUILayout.Button(Styles.DetectionFalse, Styles.helpbox))
                {
                    EditorGUIUtility.PingObject(GameObject.FindGameObjectWithTag("Player"));
                    infoString = "The player doesn't have the detection script attached to it.";
                }

                if (GUILayout.Button(Styles.ReachUnknown, Styles.helpbox))
                {
                    infoString = "There is no information on the reach variable.";
                    EditorGUIUtility.PingObject(GameObject.FindGameObjectWithTag("Player"));
                }
            }
        }

        else if (playerError)
        {
            if (GUILayout.Button(Styles.PlayerTagFalse, Styles.helpbox))
                infoString = "There was no object found with the tag 'Player'.";

            if (GUILayout.Button(Styles.DetectionUnknown, Styles.helpbox))
                infoString = "There is no information on the detection script.";

            if (GUILayout.Button(Styles.ReachUnknown, Styles.helpbox))
                infoString = "There is no information on the reach variable.";
        }

        tagError = TagHelper.DoesDoorTagNotExist();

        if (!tagError)
        {
            if (GUILayout.Button(Styles.TagTrue, Styles.helpbox))
                infoString = "The tag 'Door' has been created. \n";
        }

        else if (tagError)
        {
            if (GUILayout.Button(Styles.TagFalse, Styles.helpbox))
                infoString = ("The tag 'Door' has not yet been created. \n");     
        }

        if(playerError || tagError || reachError || detectionError) EditorGUILayout.LabelField(infoString, Styles.helpbox);
        else EditorGUILayout.LabelField("No errors found! \n", Styles.helpbox);

        EditorGUILayout.Space();
        GUILayout.Label("V3.0.0", Styles.centeredVersionLabel);
        EditorGUILayout.Space();
    }
}