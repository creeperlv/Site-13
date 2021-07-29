using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.GameLogic
{
    public class BehaviorController : MonoBehaviour
    {
        public List<ControlledBehavior> OnInit=new List<ControlledBehavior>();
        public List<ControlledBehavior> OnUpdate=new List<ControlledBehavior>();
        public List<ControlledBehavior> OnFixedUpdate=new List<ControlledBehavior>();
        // Start is called before the first frame update
        public T GetBehavior<T>() where T:ControlledBehavior
        {
            foreach (var item in OnInit)
            {
                if (item is T)
                {
                    return (T)item;
                }
            }
            foreach (var item in OnUpdate)
            {
                if (item is T)
                {
                    return (T)item;
                }
            }
            foreach (var item in OnFixedUpdate)
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
            foreach (var item in OnInit)
            {
                item.Init();
            }
        }

        // Update is called once per frame
        void Update()
        {
            var t=Time.deltaTime;
            foreach (var item in OnUpdate)
            {
                item.Refresh(t);
            }
        }
        private void FixedUpdate()
        {
            var t=Time.fixedDeltaTime;
            foreach (var item in OnFixedUpdate)
            {
                item.FixedRefresh(t);
            }
        }
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