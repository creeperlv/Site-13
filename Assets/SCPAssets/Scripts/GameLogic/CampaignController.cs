using CLUNL.Data.Serializables.CheckpointSystem;
using CLUNL.Data.Serializables.CheckpointSystem.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Site13Kernel.GameLogic
{
    public class CampaignController : MonoBehaviour , ICheckpointData
    {
        public string CampaignName;
        public List<string> SubScenes=new List<string>();
        [HideInInspector]
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
            CheckpointSystem.Init(Application.persistentDataPath);
            StartCoroutine(Execute());
        }
        public void RevertCheckPoint()
        {
            var Last=CheckpointSystem.CurrentCheckpointSystem.EnumerateCheckpoints()[0];
            CheckpointSystem.CurrentCheckpointSystem.GetOrCreateCheckPoint(Last);
        }
        int Step=0;
        IEnumerator Execute()
        {
            yield return null;
        }
        void Update()
        {

        }

        public string GetName()
        {
            return "SceneCommon";
        }

        public List<object> Save()
        {
            return new List<object> { new IntNumber(Step) };
        }

        public void Load(List<object> data)
        {
            Step = (IntNumber) data[0];
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
    public class WaitForEnterAABB : CampaignAction
    {
        public float3 A; 
        public float3 B; 
    }
    [Serializable]
    public class WaitUntilHealth : CampaignAction
    {
        /// <summary>
        /// Negative: Smaller than value. Positive: Greater than value.
        /// </summary>
        public float Threshold;
    }
    [Serializable]
    public class SetCheckpoint : CampaignAction
    {

    }
}