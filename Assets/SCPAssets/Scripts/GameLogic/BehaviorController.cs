using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.GameLogic
{
    public class BehaviorController : MonoBehaviour
    {
        public BehaviorCollection ControlledBehavior;
        // Start is called before the first frame update
        public T GetBehavior<T>() where T:ControlledBehavior
        {
            foreach (var item in ControlledBehavior.OnInit)
            {
                if (item is T)
                {
                    return (T)item;
                }
            }
            foreach (var item in ControlledBehavior.OnUpdate)
            {
                if (item is T)
                {
                    return (T)item;
                }
            }
            foreach (var item in ControlledBehavior.OnFixedUpdate)
            {
                if (item is T)
                {
                    return (T)item;
                }
            }
            return null;
        }
        void Start()
        {
            foreach (var item in ControlledBehavior.OnInit)
            {
                item.Init();
            }
        }

        // Update is called once per frame
        void Update()
        {
            var t=Time.deltaTime;
            foreach (var item in ControlledBehavior.OnUpdate)
            {
                item.Refresh(t);
            }
        }
        private void FixedUpdate()
        {
            var t=Time.fixedDeltaTime;
            foreach (var item in ControlledBehavior.OnFixedUpdate)
            {
                item.FixedRefresh(t);
            }
        }
    }
    [Serializable]
    public class BehaviorCollection
    {

        public List<ControlledBehavior> OnInit=new List<ControlledBehavior>();
        public List<ControlledBehavior> OnUpdate=new List<ControlledBehavior>();
        public List<ControlledBehavior> OnFixedUpdate=new List<ControlledBehavior>();
    }
    [Serializable]
    public class ControlledBehavior : MonoBehaviour
    {
        public virtual void Init()
        {

        }
        public virtual void Refresh(float deltaTime)
        {

        }
        public virtual void FixedRefresh(float fixedDeltaTime)
        {

        }
    }
}