using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Site13Kernel.DynamicScene
{
    public class CrossSubSceneDataController : MonoBehaviour
    {
        public static CrossSubSceneDataController CurrentController;
        public List<SingleData> Datas;
        public bool ShowDataPreview = false;
        void Start()
        {
            CurrentController = this;
        }
        public void AddData(string Name, string SceneID, object Data)
        {
            SingleData singleData = new SingleData();
            singleData.Name = Name;
            singleData.SceneID = SceneID;
            singleData.Data = Data;
            foreach (var item in Datas)
            {
                if (item.Name == Name & item.SceneID == SceneID) throw new Exception("EXCEPT:001-001-0001");
            }
            Datas.Add(singleData);
        }
        public SingleData FindData(string Name, string SceneID)
        {
            foreach (var item in Datas)
            {
                if (item.Name == Name & item.SceneID == SceneID) return item;
            }
            return null;
        }
        private void FixedUpdate()
        {
            if (CurrentController != this)
            {
                foreach (var item in Datas)
                {
                    item.SelfDestruct();
                }
                Datas.Clear();
            }
        }
        void Update()
        {
#if UNITY_EDITOR
            if (Application.isEditor)
            {
                if (ShowDataPreview)
                    foreach (var item in Datas)
                    {
                        item.UpdatePreview();
                    }
            }
#endif
        }
    }
    [Serializable]
    public class SingleData
    {
        public string Name;
        public string SceneID;
        object data = null;
        public object Data { get { return data; } set { data = value; isChanged = true; } }
        bool isChanged;
        public string DataPreview;
        public void UpdatePreview()
        {
            if (isChanged)
            {
                DataPreview = data.ToString();
                isChanged = false;
            }
        }
        public void SelfDestruct()
        {
            Name = null;
            SceneID = null;
            data = null;
        }
    }
}