using Site13Kernel.Weapon;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Site13Kernel.Weapon
{
    public class MaterialOnAmmo : MonoBehaviour
    {
        public List<MeshRenderer> Renderers;
        public string ID;

        [HideInInspector]
        public TraditionalWeapon Parent;
        Color Origin;
        void Start()
        {
            Origin= Renderers.First().material.GetColor("_EmissionColor");
            Parent = GetComponent<TraditionalWeapon>();
        }
        int last=0;
        // Update is called once per frame
        void Update()
        {
            if (Parent == null)
            {
                Parent = GetComponent<TraditionalWeapon>();
            }
            else
            {
                var a = int.Parse(GameInfo.CurrentGame.FlagsGroup[Parent.BagAlias + ":" + ID]);
                if (last != a)
                {
                    if (a == 0)
                    {
                        foreach (var item in Renderers)
                        {
                            item.material.SetColor("_EmissionColor", new Color(0, 0, 0, 1));
                        }
                    }
                    else if (last == 0)
                    {
                        foreach (var item in Renderers)
                        {
                            item.material.SetColor("_EmissionColor", Origin);
                        }

                    }
                    last = a;
                }
            }
            
                
        }
    }

}
