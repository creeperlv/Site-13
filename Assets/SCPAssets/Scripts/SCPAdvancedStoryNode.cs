#if UNITY_STANDALONE
using Site_13_Behavior;
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{

    public class SCPAdvancedStoryNode : SCPStoryNodeBaseCode
    {
        [Serializable]
        public class SCPVersion
        {
            public int Major;
            public int Minior;
            public int Build;
            public int Path;
        }
        public SCPVersion TargetVersion;
        [Header("Version Strict will override Downgradable and Upgradable.", order = 0)]
        [Tooltip("Version Strict will override Downgradable and Upgradable.")]
        public bool isStrictVerion;
        public bool isDowngradable;
        public bool isUpgradable;
        public List<GameObjectPair> GameObjects;
        public List<SystemObjectPair> SystemObjects;
        [System.Serializable]
        public class GameObjectPair
        {
            public string name;
            public UnityEngine.Object obj;
        }
        [System.Serializable]
        public class SystemObjectPair
        {
            public string name;
            public SystemObject SystemObject;
        }
        public enum Type
        {
            String, Int, Float, Double, Boolean
        }
        [System.Serializable]
        public class SystemObject
        {
            public Type ObjectType;
            public string TexturalContent = "";
            public int Integer = 0;
            public float Float = 0;
            public double Double = 0;
            public bool Boolean = false;
        }
        public string MethodPath = "";
#if UNITY_STANDALONE
        IAdvancedNode AdvNode;
#endif
        Dictionary<string, UnityEngine.Object> GamObjs;
        Dictionary<string, object> SysObjs;
        private void Start()
        {
            //isStoryRequiresPlayer = true;
            #region Generate Dictionary of GameObjects
            GamObjs = new Dictionary<string, UnityEngine.Object>();
            foreach (var item in GameObjects)
            {
                GamObjs.Add(item.name, item.obj);
            }
            #endregion
            #region Generate Dictionary of SystemObject
            SysObjs = new Dictionary<string, object>();
            foreach (var item in SystemObjects)
            {
                object data = null;
                switch (item.SystemObject.ObjectType)
                {
                    case Type.String:
                        data = item.SystemObject.TexturalContent;
                        break;
                    case Type.Int:
                        data = item.SystemObject.Integer;
                        break;
                    case Type.Float:
                        data = item.SystemObject.Float;
                        break;
                    case Type.Double:
                        data = item.SystemObject.Double;
                        break;
                    case Type.Boolean:
                        data = item.SystemObject.Boolean;
                        break;
                    default:
                        break;
                }
                SysObjs.Add(item.name, data);
            }
            #endregion
            //Find the Node through the given path.
#if UNITY_STANDALONE
            try
            {
                var t = System.Reflection.Assembly.GetAssembly(typeof(IAdvancedNode)).GetType(MethodPath);
                AdvNode = (IAdvancedNode)Activator.CreateInstance(t);
                Version MethodVer = AdvNode.GetVersion();
                Version Target = new Version(TargetVersion.Major, TargetVersion.Minior, TargetVersion.Build, TargetVersion.Path);
                Debug.Log("Loaded.");

                switch ((MethodVer.CompareTo(Target)))
                {
                    case 0:
                        break;
                    case 1:
                        if (isStrictVerion == true)
                        {
                            AdvNode = new MismatchReplacement();
                        }
                        if (isUpgradable == false)
                        {
                            AdvNode = new MismatchReplacement();
                        }
                        break;
                    case -1:
                        if (isStrictVerion == true)
                        {
                            AdvNode = new MismatchReplacement();
                        }
                        if (isDowngradable == false)
                        {
                            AdvNode = new MismatchReplacement();
                        }
                        break;
                    default:
                        break;
                }

                if (MethodVer < Target)
                {
                    Debug.Log("Downgrade");
                }
                else
                {
                    Debug.Log("Upgrade");
                }
                Debug.Log("Version Check Complete.");
            }
            catch (System.Exception e)
            {
                Debug.Log("Error:" + e.Message);
            }
            a = (IEnumerator a) => { StartCoroutine(a); };
#endif
        }
        Action<IEnumerator> a;
        public override void StartStory()
        {
            if(Application.platform == RuntimePlatform.WindowsEditor|| Application.platform == RuntimePlatform.LinuxEditor|| Application.platform == RuntimePlatform.OSXEditor)
            {

                Debug.Log("Should Run:" + MethodPath);
            }
            else
            {
                AdvNode.Run(GamObjs, SysObjs, out isStarted, ref a);
            }
        }
    }
#if UNITY_STANDALONE
    public class MismatchReplacement : IAdvancedNode
    {
        public Version GetVersion() => new Version(0, 0, 0, 0);

        public void Run(Dictionary<string, UnityEngine.Object> GameObjs, Dictionary<string, object> Parameters, out bool isStarted, ref Action<IEnumerator> Launch)
        {
            isStarted = true;
            GameInfo.CurrentGame.notification.ShowNotification("<color=red>Error</color>: Behavior version <color=red>mismatch</color>!");
        }
    }
#endif
}