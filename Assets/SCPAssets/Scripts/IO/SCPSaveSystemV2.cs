using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Site13Kernel.Scripts.IO
{
    public class SCPSaveSystemV2
    {

    }
    public class SavSysV2Obj
    {
        public string ObjID;
        public string ObjName;
        public static SavSysV2Obj GetFromUnityObj(GameObject obj)
        {
            SavSysV2Obj savSysV2Obj = new SavSysV2Obj();
            savSysV2Obj.ObjID = ""+obj.GetInstanceID();
            var str = typeof(string);
            savSysV2Obj.ObjName = obj.name;
            var a= obj.GetComponents(typeof(MonoBehaviour));
            foreach (var item in a)
            {
                item.GetType().FindMembers(System.Reflection.MemberTypes.Field, System.Reflection.BindingFlags.Public, new System.Reflection.MemberFilter((_a, _b) => {
                    {
                        //a.ReflectedType
                    }
                    return true; }), item);
            }
            return savSysV2Obj;
        }
    }
    public class GenericComponentInfo
    {
        public string ComponentPath;
    }
}
