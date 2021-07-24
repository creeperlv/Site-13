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
            foreach (var item in OnUpdate)
            {
                item.Refresh();
            }
        }
        private void FixedUpdate()
        {
            foreach (var item in OnFixedUpdate)
            {
                item.FixedRefresh();
            }
        }
    }
    [Serializable]
    public class ControlledBehavior : MonoBehaviour
    {
        public virtual void Init()
        {

        }
        public virtual void Refresh()
        {

        }
        public virtual void FixedRefresh()
        {

        }
    }
}