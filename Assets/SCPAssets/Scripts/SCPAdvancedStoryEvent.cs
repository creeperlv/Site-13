using UnityEngine;
using UnityEngine.Events;

namespace Site13Kernel
{
    public class SCPAdvancedStoryEvent : MonoBehaviour
    {
        public UnityEvent EnterEvent;
        public UnityEvent ExitEvent;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<SCPFirstController>() != null)
            {
                EnterEvent.Invoke();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<SCPFirstController>() != null)
            {
                ExitEvent.Invoke();
            }
        }
    }
}
