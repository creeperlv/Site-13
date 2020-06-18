using System.IO;
using UnityEditor;
using UnityEngine;

namespace SWS
{
    [InitializeOnLoad]
    public class PromoWindow : EditorWindow
    {
        private static Texture2D reviewWindowImage;
        private static string imagePath = "/Image/logo.png";
        private static string keyName = "SplendidExplosionLite";

        //data:
        //active, counter, lastCheck

        static PromoWindow()
        {
            EditorApplication.update += Startup;
        }


        static void Startup()
        {
            EditorApplication.update -= Startup;

            if (!EditorPrefs.HasKey(keyName))
            {
                string[] data = new string[3];
                data[0] = "true;";
                data[1] = "0;";
                data[2] = "0";
                EditorPrefs.SetString(keyName, data[0] + data[1] + data[2]);
            }

            Count();
        }


        [MenuItem("Window/SplendidExplostionLite/GetFullVersion")]
        static void Init()
        {
            EditorWindow.GetWindowWithRect(typeof(PromoWindow), new Rect(0, 0, 300, 320), false, "Get Full Version");
        }


        void OnGUI()
        {
            if (reviewWindowImage == null)
            {
                var script = MonoScript.FromScriptableObject(this);
                string path = Path.GetDirectoryName(AssetDatabase.GetAssetPath(script));
                reviewWindowImage = AssetDatabase.LoadAssetAtPath(path + imagePath, typeof(Texture2D)) as Texture2D;
            }

            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(-2);
            GUILayout.Label(reviewWindowImage);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Please get Full Version of Spledid Explosion.");
            EditorGUILayout.LabelField("Your support helps us to improve this product.");
            EditorGUILayout.LabelField("Thank you!");

            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Get Full Version"))
            {
                Help.BrowseURL("https://assetstore.unity.com/packages/slug/153321");
                DisableRating();
            }
            if (GUILayout.Button("Later"))
            {
                string[] data = new string[3];
                data = EditorPrefs.GetString(keyName).Split(';');
                data[0] = data[0] + ";";
                data[1] = "5;";

                EditorPrefs.SetString(keyName, data[0] + data[1] + data[2]);
                this.Close();
            }
            if (GUILayout.Button("Never"))
            {
                DisableRating();
            }
            EditorGUILayout.EndHorizontal();
        }


        static void Count()
        {
            string[] data = new string[3];
            data = EditorPrefs.GetString(keyName).Split(';');

            if (data[0] == "false")
                return;

            double time = GenerateUnixTime();
            double diff = time - double.Parse(data[2]);
            int counter = int.Parse(data[1]);
            if (diff < 20) return;

            data[0] = data[0] + ";";
            data[1] = (counter + 1) + ";";
            data[2] = time.ToString();
            EditorPrefs.SetString(keyName, data[0] + data[1] + data[2]);

            if (counter > 6)
                Init();
        }


        static double GenerateUnixTime()
        {
            var epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            return (System.DateTime.UtcNow - epochStart).TotalHours;
        }


        void DisableRating()
        {
            string[] data = new string[3];
            data = EditorPrefs.GetString(keyName).Split(';');

            data[0] = "false;";
            data[1] = "0;";
            EditorPrefs.SetString(keyName, data[0] + data[1] + data[2]);
            this.Close();
        }
    }
}