using Site_13_IL.Interact;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.Extensions
{
    public class ExtensionManager
    {
        public static void AddSite13ILInteract(string id, Func<InteractParameter,object> func )
        {
            InteractivesPool.Interactives.Add(id, func);
        }
        static List<SCPEvent> Events = new List<SCPEvent>();
        public static void AddEvent(SCPEvent Event) {
            try
            {
                int i = Events.IndexOf(Event);
                if (i >= 0)
                {

                }
                else
                {
                    Events.Add(Event);
                }
            }
            catch (System.Exception)
            {
            }
        }
        public static void AddHandler(string EventName,string Entry,Extension extension)
        {
            foreach (var item in Events)
            {
                if (item.Name == EventName)
                {
                    item.SubHandlers.Add(extension, Entry);
                }
            }
        }
    }
    public class SCPEvent {
        public string Name;
        public Dictionary<Extension, string> SubHandlers = new Dictionary<Extension, string>();
        /**
         * Handler:
         *  Extension : The extension registered itself to current event
         *  string : The entry.
         **/
    }
    public enum ExtensionType
    {
        DLL,Site13IL
        /**
         * DLL - Dynamic Link Library
         *      Suit for Desktop. DLL Extensions will be disabled on Consoles like Xbox.
         *          Dest Performance.
         *          .Net Standard 2.0
         * Site13IL - Site-13-IL
         *      Suit for all platform.
         *          Bad performance.
         *          Defined by me.
         *          As a replacement of MSIL. Primary compile C# to Site-13-IL.
         *          History: CVCIL (CVCSystem) -> Site-13-IL(What Happened to Site-13?)
         **/
    }
    public class Extension
    {
        public ExtensionType type = ExtensionType.Site13IL;
        public string File;
        public Dictionary<string, string> Events = new Dictionary<string, string>();
    }
}