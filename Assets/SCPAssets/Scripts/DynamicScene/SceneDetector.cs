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
                    SceneManager.LoadScene(TargetSceneName, LoadSceneMode.Additive);
                    FinalLoadedSceneIdentifier = TargetSceneName;
                }
                catch (System.Exception)
                {
                    try
                    {
                        if (TargetSceneID != -1)
                        {
                            SceneManager.LoadScene(TargetSceneID, LoadSceneMode.Additive);
                            FinalLoadedSceneIdentifier = TargetSceneID;
                            var gos = SceneManager.GetSceneByName(TargetSceneName).GetRootGameObjects();
                            foreach (var item in gos)
                            {
                                if (item.name == "ModularScenePrefab")
                                {
                                    try
                                    {
                                        var Objects = item.GetComponent<ModularSceneObjects>();
                                        Objects.SaveModule.Register();
                                        Objects.SaveModule.Load();
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            else
            {
                try
                {
                    if (TargetSceneID != -1)
                    {
                        SceneManager.LoadScene(TargetSceneID, LoadSceneMode.Additive);
                        FinalLoadedSceneIdentifier = TargetSceneID;
                        var gos = SceneManager.GetSceneByBuildIndex(TargetSceneID).GetRootGameObjects();
                        foreach (var item in gos)
                        {
                            if (item.name == "ModularScenePrefab")
                            {
                                try
                                {
                                    var Objects = item.GetComponent<ModularSceneObjects>();
                                    Objects.SaveModule.Register();
                                    Objects.SaveModule.Load();

                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        void UnloadScene()
        {
            try
            {
                if (FinalLoadedSceneIdentifier is string)
                {
                    SceneManager.UnloadSceneAsync((string)FinalLoadedSceneIdentifier);
                    var gos = SceneManager.GetSceneByName(TargetSceneName).GetRootGameObjects();
                    foreach (var item in gos)
                    {
                        if (item.name == "ModularScenePrefab")
                        {
                            try
                            {
                                var Objects = item.GetComponent<ModularSceneObjects>();
                                Objects.SaveModule.Save();
                                Objects.SaveModule.Unregister();

                            }
                            catch
                            {
                            }
                        }
                    }
                }
                else if (FinalLoadedSceneIdentifier is int)
                {
                    SceneManager.UnloadSceneAsync((int)FinalLoadedSceneIdentifier);
                    var gos = SceneManager.GetSceneByBuildIndex((int)FinalLoadedSceneIdentifier).GetRootGameObjects();
                    foreach (var item in gos)
                    {
                        if (item.name == "ModularScenePrefab")
                        {
                            try
                            {

                                var Objects = item.GetComponent<ModularSceneObjects>();
                                Objects.SaveModule.Save();
                                Objects.SaveModule.Unregister();

                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
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