using Site13Kernel.MTF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;

namespace Site13Kernel.Stories.Delta12
{
    public class Delta12Stage01 : SCPBaseScript
    {
        public List<NavMeshAgent> agents;
        public List<Transform> TargetSeats;
        public GameObject Stage02;
        public VideoPlayer briefVideo;
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
            }
        }
        IEnumerator StoryPrt2()
        {
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].SetDestination(TargetSeats[i].position);
            }
            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Delta-12! I have waited for you for 1 hour!");
            yield return new WaitForSeconds(3);
            foreach (var item in agents)
            {
                item.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Idle1");
                item.GetComponent<Delta12Teammate>().PlayWalkSound = false;
                var a=item.transform.rotation;
                a.eulerAngles= new Vector3(0,180,0);
                item.transform.rotation = a;
            }
            foreach (var item in agents)
            {
                item.isStopped = true;
            }
            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("D-12-Cap: Sorry, to ensure we are not followed, we took another route.",4);
            yield return new WaitForSeconds(4);
            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("It doesn't matter.");
            yield return new WaitForSeconds(3);
            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("Shell we start the brief?");
            yield return new WaitForSeconds(3);
            GameInfo.CurrentGame.PublicSubtitle.ShowSubtitle("D-12-Cap: Sure.");
            briefVideo.Play();
            yield return new WaitForSeconds(30);
            Stage02.SetActive(true);
            this.gameObject.SetActive(false);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Delta12Teammate>() != null)
            {
                StartCoroutine(StoryPrt2());
            }
        }
    }

}