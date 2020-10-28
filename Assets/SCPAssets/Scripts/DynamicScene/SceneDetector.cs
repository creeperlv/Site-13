using Site13Kernel.IO;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Site13Kernel.DynamicScene
{
    public class SceneDetector : MonoBehaviour
    {
        public int TargetSceneID = -1;
        public string TargetSceneName = "#";
        public bool AutoLoad = false;
        object FinalLoadedSceneIdentifier;
        Scene LoadedScene;
        // Start is called before the first frame update
        void Start()
        {
            if (AutoLoad == true)
            {
                LoadTargetScene();
            }
        }
        void LoadTargetScene()
        {

            if (TargetSceneName != "#")
            {
                try
                {
                    {
                        var async = SceneManager.LoadSceneAsync(TargetSceneName, new LoadSceneParameters(LoadSceneMode.Additive));
                        async.completed += Async_completed;
                        FinalLoadedSceneIdentifier = TargetSceneName;
                        //var gos = LoadedScene.GetRootGameObjects();
                        //foreach (var item in gos)
                        //{
                        //    if (item.name == "ModularScenePrefab")
                        //    {
                        //        try
                        //        {
                        //            var Objects = item.GetComponent<ModularSceneObjects>();
                        //            foreach (var Component in Objects.Components)
                        //            {
                        //                Component.Init();
                        //            }
                        //            Objects.SaveModule.Register();
                        //            Objects.SaveModule.Load();
                        //        }
                        //        catch (Exception e)
                        //        {
                        //            if (Application.isEditor) Debug.LogError(e);
                        //        }
                        //    }
                        //}
                    }
                }
                catch (System.Exception ee)
                {
                    Debug.LogError(ee);
                    try
                    {
                        if (TargetSceneID != -1)
                        {
                            var async = SceneManager.LoadSceneAsync(TargetSceneID, new LoadSceneParameters(LoadSceneMode.Additive));
                            async.completed += Async_completed;
                            FinalLoadedSceneIdentifier = TargetSceneID;
                            //var gos = LoadedScene.GetRootGameObjects();
                            //foreach (var item in gos)
                            //{
                            //    if (item.name == "ModularScenePrefab")
                            //    {
                            //        try
                            //        {
                            //            var Objects = item.GetComponent<ModularSceneObjects>();
                            //            foreach (var Component in Objects.Components)
                            //            {
                            //                Component.Init();
                            //            }
                            //            Objects.SaveModule.Register();
                            //            Objects.SaveModule.Load();
                            //        }
                            //        catch (Exception e)
                            //        {
                            //            if (Application.isEditor) Debug.LogError(e);
                            //        }
                            //    }
                            //}
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

                        var async = SceneManager.LoadSceneAsync(TargetSceneID, new LoadSceneParameters(LoadSceneMode.Additive));

                        async.completed += Async_completed;
                        FinalLoadedSceneIdentifier = TargetSceneID;
                        //var gos = LoadedScene.GetRootGameObjects();
                        //    Debug.Log("Root Objs:"+gos.Length);
                        //    Debug.Log("Is scene loaded?"+LoadedScene.isLoaded);
                        //foreach (var item in gos)
                        //{
                        //    Debug.Log("Is it target:"+item.name);
                        //    if (item.name == "ModularScenePrefab")
                        //    {
                        //        try
                        //        {
                        //            var Objects = item.GetComponent<ModularSceneObjects>();
                        //            foreach (var Component in Objects.Components)
                        //            {
                        //                Component.Init();
                        //            }
                        //            Objects.SaveModule.Register();
                        //            Objects.SaveModule.Load();

                        //        }
                        //        catch (Exception e)
                        //        {
                        //            if (Application.isEditor) Debug.LogError(e);
                        //        }
                        //    }
                        //}
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }

        private void Async_completed(AsyncOperation obj)
        {
            if (FinalLoadedSceneIdentifier is int)
            {

                LoadedScene = SceneManager.GetSceneByBuildIndex((int)FinalLoadedSceneIdentifier);
            }
            else
            {
                LoadedScene = SceneManager.GetSceneByName((string)FinalLoadedSceneIdentifier);

            }
            var gos = LoadedScene.GetRootGameObjects();
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

        void UnloadScene()
        {
            try
            {
                var gos = LoadedScene.GetRootGameObjects();
                foreach (var item in gos)
                {
                    if (item.name == "ModularScenePrefab")
                    {
                        try
                        {
                            var Objects = item.GetComponent<ModularSceneObjects>();
                            foreach (var Component in Objects.Components)
                            {
                                Component.OnDispose();
                            }
                            Objects.SaveModule.Save();
                            Objects.SaveModule.Unregister();

                        }
                        catch (Exception e)
                        {
                            if (Application.isEditor) Debug.LogError(e);
                        }
                    }
                }
                SceneManager.UnloadSceneAsync(LoadedScene);
            }
            catch (Exception)
            {
            }
            FinalLoadedSceneIdentifier = null;//Reset Identifier to allow reload of the scene.
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<SCPFirstController>() != null)
            {
                if (FinalLoadedSceneIdentifier == null)
                {
                    LoadTargetScene();
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
    }

}