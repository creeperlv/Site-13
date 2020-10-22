using HUDNavi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel.HUD
{
    public class SCPNavipointTarget : SCPStoryNodeBaseCode
    {
        public float Delay;
        public List<SCPNavipointTarget> NextTargets=new List<SCPNavipointTarget>();
        NavigationTarget target;
        // Start is called before the first frame update
        void Start()
        {
            target = GetComponent<NavigationTarget>();
            target.Show = false;
            StartCoroutine(Activation());
        }
        public override void StartStory()
        {
            target.Show = false;
            foreach (var item in NextTargets)
            {
                item.gameObject.SetActive(true);
            }
            this.gameObject.SetActive(false);
        }
        public IEnumerator Activation()
        {
            yield return new WaitForSeconds(Delay);
            target.Show = true;
        }
    }

}
