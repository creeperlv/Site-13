﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    /// <summary>
    /// An interface that describes behaviors of a hand item.
    /// </summary>
    public interface IHandItem
    {
        void Init(string data);//Data can be something like "MSG:PICKUP,(PickableParameter)"
        string GetData();
        /// <summary>
        /// Called when play pressed primary operate key (usually 'Mouse 1'/'Left Click')
        /// </summary>
        /// <param name="isHolding"></param>   
        void Primary(ref bool isHolding);
        /// <summary>
        /// Called when play pressed secondary operate key (usually 'Mouse 2'/'Right Click')
        /// </summary>
        /// <param name="isHolding"></param>

        void Secondary(ref bool isHolding);
        /// <summary>
        /// Called when play pressed reload key (usually 'r')
        /// </summary>
        /// <param name="isHolding"></param>

        void Reload(ref bool isHolding);
    }
    public interface IPickableItem
    {
        string GetPickUpMsg();
        string Pickup();
    }

}
