using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Site13Kernel.GameLogic
{
    public class CampaignController : MonoBehaviour
    {
        public List<string> SubScenes=new List<string>();
        public List<Scene> _SubScenes=new List<Scene>();
        public List<AsyncOperation> __SubScenes=new List<AsyncOperation>();
        public List<BehaviorController> behaviorControllers= new List<BehaviorController>();
        void Start()
        {
            foreach (var item in SubScenes)
            {
                _SubScenes.Add(SceneManager.GetSceneByName(item));
            }
            foreach (var item in _SubScenes)
            {
                var AO=SceneManager.LoadSceneAsync(item.name, LoadSceneMode.Additive);
                AO.allowSceneActivation = false;
                __SubScenes.Add(AO);
            }
        }

        void Update()
        {

        }
    }

}
namespace Site13Kernel.GameLogic.CampaignActions
{
    [Serializable]
    public class CampaignAction { }
    [Serializable]
    public class LoadSceneByName : CampaignAction
    {
        public string Name;
    }
    [Serializable]
    public class WaitUntilLastSceneDone : CampaignAction
    {

    }
    [Serializable]
    public class SetCheckpoint : CampaignAction
    {

    }
}