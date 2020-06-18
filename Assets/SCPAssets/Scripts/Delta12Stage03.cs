using Site13Kernel.MTF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Site13Kernel.Stories.Delta12
{
    public class Delta12Stage03 : MonoBehaviour
    {
        public List<NavMeshAgent> agents;
        public GameObject ToEnable;
        public GameObject ToDisable;
        void Start()
        {
        }
        private void OnEnable()
        {
            StartCoroutine(StartStory());
        }
        IEnumerator StartStory()
        {
            yield return new WaitForSeconds(1f);

            foreach (var item in agents)
            {
                yield return new WaitForSeconds(.01f);
                item.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Walk");
                item.GetComponent<Delta12Teammate>().PlayWalkSound = true;
            }
            foreach (var item in agents)
            {
                item.SetDestination(this.transform.position);
                item.isStopped = false;
            }
            yield break;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var A = collision.gameObject.GetComponent<Delta12Teammate>();
            if (A != null)
            {
                Debug.Log("D12ARRIVE");
            }
        }
        bool isNextStage = false;
        private void OnTriggerEnter(Collider other)
        {
            if (isNextStage == false)
            {

                var A = other.GetComponent<Delta12Teammate>();
                if (A != null)
                {
                    Debug.Log("D12ARRIVE_TRIGGER");
                    ToEnable.SetActive(true);
                    try
                    {
                        ToDisable.SetActive(false);
                    }
                    catch (System.Exception)
                    {
                    }
                    isNextStage = true;
                }
            }
        }
    }

}