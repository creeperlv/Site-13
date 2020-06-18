using Site_13_Plug_in_Lib;
using Site_13_Plug_in_Lib.PluginSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Plugin
{
    public class PluginPool
    {
        public static int PluginCount=0;
        public static List<PluginInfo> Plugins = new List<PluginInfo>();
        public static List<ISite13Plugin> OnBootloader = new List<ISite13Plugin>();
        public static FunctionsCollection functionsCollection = new FunctionsCollection();
    }
    public class PluginInfo
    {
        public string PluginID="";
        public string PluginName="";
        public string BaseFolder="";
        public string PluginEntrance="";
        public bool isDisabled=false;
    }
}