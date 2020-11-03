using CLUNL.Data.Layer0.Buffers;
using Site13Kernel.IO;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Site13Kernel.DynamicScene
{
    public class SceneDetector : MonoBehaviour, IByteBufferable
    {
        public int TargetSceneID = -1;
        public string TargetSceneName = "#";
        public bool AutoLoad = false;
        bool loaded = false;
        object FinalLoadedSceneIdentifier;
        Scene LoadedScene;
        // Start is called before the first frame update
        void Start()
        {
        }
        void LoadTargetScene()
        {
            loaded = true;
            if (TargetSceneName != "#")
            {
                try
                {
                    {
                        FinalLoadedSceneIdentifier = TargetSceneName;
                        LoadedScene = SceneManager.GetSceneByName(TargetSceneName);
                        //var async = SceneManager.LoadSceneAsync(TargetSceneName, new LoadSceneParameters(LoadSceneMode.Additive));
                        //async.allowSceneActivation = false;
                        StartCoroutine(StartCompleted());
                    }
                }
                catch (System.Exception ee)
                {
                    Debug.LogError(ee);
                    try
                    {
                        if (TargetSceneID != -1)
                        {
                            FinalLoadedSceneIdentifier = TargetSceneID;
                            LoadedScene = SceneManager.GetSceneByBuildIndex(TargetSceneID);

                            //var async = SceneManager.LoadSceneAsync(TargetSceneID, new LoadSceneParameters(LoadSceneMode.Additive));
                            //async.allowSceneActivation = false;
                            StartCoroutine(StartCompleted());
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }
                }
            }
            else
            {
                try
                {
                    if (TargetSceneID != -1)
                    {

                        FinalLoadedSceneIdentifier = TargetSceneID;
                        Debug.Log(FinalLoadedSceneIdentifier);
                        LoadedScene = SceneManager.GetSceneByBuildIndex(TargetSceneID);
                        //var async = SceneManager.LoadSceneAsync(TargetSceneID, new LoadSceneParameters(LoadSceneMode.Additive));
                        //async.allowSceneActivation = false;
                        StartCoroutine(StartCompleted());
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
        //IEnumerator StartCompleted(AsyncOperation obj)
        IEnumerator StartCompleted()
        {
            //while (LoadedScene.IsValid() == false)
            //{
            //        Debug.Log("SC:" + SceneManager.sceneCount);
            //    for (int i = 0; i < SceneManager.sceneCount; i++)
            //    {
            //        var scene = SceneManager.GetSceneAt(i);
            //        Debug.Log("S:" + scene.buildIndex);
            //        if (FinalLoadedSceneIdentifier is int)
            //        {
            //            if (scene.buildIndex == (int)FinalLoadedSceneIdentifier)
            //            {
            //                LoadedScene = scene;
            //                Debug.Log("S-V:" + scene.IsValid());
            //                Debug.Log("S-V2:" + LoadedScene.IsValid());
            //            }

            //        }
            //        else
            //        {
            //            if (scene.name == (string)FinalLoadedSceneIdentifier) LoadedScene = scene;
            //        }
            //    }
            //    //if (FinalLoadedSceneIdentifier is int)
            //    //{
            //    //    LoadedScene = SceneManager.GetSceneByBuildIndex((int)FinalLoadedSceneIdentifier);
            //    //}
            //    //else
            //    //{
            //    //    LoadedScene = SceneManager.GetSceneByName((string)FinalLoadedSceneIdentifier);
            //    //}
            //    yield return null;
            //}
            //Debug.Log(FinalLoadedSceneIdentifier + ":" + LoadedScene.name);
            //obj.allowSceneActivation = true;
            if (FinalLoadedSceneIdentifier is int)
                LoadedScene = SceneManager.LoadScene((int)FinalLoadedSceneIdentifier, new LoadSceneParameters() { loadSceneMode = LoadSceneMode.Additive });
            else
                LoadedScene = SceneManager.LoadScene((string)FinalLoadedSceneIdentifier, new LoadSceneParameters() { loadSceneMode = LoadSceneMode.Additive });
            yield return null;
            var gos = LoadedScene.GetRootGameObjects();
            Debug.Log("GOS:" + gos.Length);
            foreach (var item in gos)
            {
                if (item.name == "ModularScenePrefab")
                {
                    try
                    {
                        var Objects = item.GetComponent<ModularSceneObjects>();
                        foreach (var Component in Objects.Components)
                        {
                            Component.Init();
                        }
                        Debug.Log("Register SFWR.");
                        Objects.SaveModule.Register();
                        Objects.SaveModule.Load();

                    }
                    catch (Exception e)
                    {
                        if (Application.isEditor) Debug.LogError(e);
                    }
                }
            }
        }
        private void Async_completed(AsyncOperation obj)
        {

        }
        IEnumerator RealUnload()
        {
            ((ModularSaveSystem)GameInfo.CurrentGame.CurrentSceneSaveSystem).SaveGI();
            var gos = LoadedScene.GetRootGameObjects();
            foreach (var item in gos)
            {
                if (item.name == "ModularScenePrefab")
                {
                    //try
                    //{
                    var Objects = item.GetComponent<ModularSceneObjects>();
                    foreach (var Component in Objects.Components)
                    {
                        Component.OnDispose();
                    }
                    Objects.SaveModule.Save();
                    Objects.SaveModule.Unregister();

                    //}
                    //catch (Exception e)
                    //{
                    //    if (Application.isEditor) Debug.LogError(e);
                    //}
                }
            }
            yield return null;
            SceneManager.UnloadSceneAsync(LoadedScene);
            //}
            //catch (Exception)
            //{
            //}
            FinalLoadedSceneIdentifier = null;//Reset Identifier to allow reload of the scene.

        }
        void UnloadScene()
        {
            loaded = false;
            StartCoroutine(RealUnload());
            //try
            //{
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<SCPFirstController>() != null)
            {
                if (FinalLoadedSceneIdentifier == null)
                {
                    if (loaded == false)
                    {
                        Debug.Log("Load from enterance:" + TargetSceneID);
                        LoadTargetScene();
                    }
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<SCPFirstController>() != null)
            {
                UnloadScene();
            }
        }
        public void SideStart()
        {
            if (isDeserialize == false)
            {
                if (AutoLoad) { Debug.Log("Auto Load."); LoadTargetScene(); }
            }
        }
        bool isDeserialize = false;
        public void Deserialize(ByteBuffer buffer)
        {
            isDeserialize = true;
            loaded = true; Debug.Log("MSS Processed:" + TargetSceneID);
            DataBuffer dataBuffer = DataBuffer.FromByteBuffer(buffer);
            if (dataBuffer.ReadBool() == true)
            {
                loaded = true; Debug.Log("Load by MSS");
                LoadTargetScene();
            }
        }

        public ByteBuffer Serialize()
        {
            DataBuffer dataBuffer = new DataBuffer();
            dataBuffer.WriteBool(loaded);
            return dataBuffer.ObtainByteArray();
        }
    }

}