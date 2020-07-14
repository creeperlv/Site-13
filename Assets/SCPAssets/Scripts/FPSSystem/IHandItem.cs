using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.FPSSystem
{
    /// <summary>
    /// An interface that describes behaviors of a hand item.
    /// <br/>
    /// This is base interface of FPSSystem V3
    /// </summary>
    public interface IHandItem
    {
        void Init(string data);//Data can be something like "MSG:PICKUP,(PickableParameter)"
        string GetData();
        /// <summary>
        /// Called when play pressed primary operate key (usually 'Mouse 1'/'Left Click')
        /// </summary>
        void Primary();
        void UnPrimary();
        /// <summary>
        /// Called when play pressed secondary operate key (usually 'Mouse 2'/'Right Click')
        /// </summary>

        void Secondary();
        void UnSecondary();
        /// <summary>
        /// Called when "Q" pressed.
        /// </summary>
        void Fight();
        /// <summary>
        /// Called when play pressed reload key (usually 'r')
        /// </summary>
        void Reload();
        bool IsPrimaryCompleted();
        bool IsSecondaryCompleted();
        bool IsFightCompleted();
        bool IsReloadCompleted();
        bool IsOnOperation();
        bool IsFPSSystemV3Enabled();
    }
    public interface IPickableItem
    {
        string GetPickUpMsg();
        string Pickup();
    }

}
