using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Site13Kernel.UI.Customed
{
    public class CampaignButton : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler, IVisualElement
    {
        public GameObject HintImage;
        public Image BackgroundImage;
        public Action OnClick=null;
        [HideInInspector]
        public CampaignButtonGroup CampaignParent;
        protected CampaignButton()
        {
        }
        public void Init(CampaignButtonGroup parent)
        {
            CampaignParent = parent;

        }
        public void Hint()
        {
            if (HintImage != null) HintImage.SetActive(true);
        }
        public void Unhint()
        {
            if (HintImage != null) HintImage.SetActive(false);
        }
        private void Press()
        {
            if (IsActive() && IsInteractable())
            {
                if (CampaignParent != null)
                    CampaignParent.UnHintAll();
                this.Hint();
                if (OnClick != null)
                    OnClick();
            }
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                Press();
            }
        }

        public virtual void OnSubmit(BaseEventData eventData)
        {
            Press();
            if (IsActive() && IsInteractable())
            {
                DoStateTransition(SelectionState.Pressed, instant: false);
                StartCoroutine(OnFinishSubmit());
            }
        }

        private IEnumerator OnFinishSubmit()
        {
            float fadeTime = base.colors.fadeDuration;
            float elapsedTime = 0f;
            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
            }

            DoStateTransition(base.currentSelectionState, instant: false);
        }
    }
    [Serializable]
    public class CampaignButtonGroup
    {
        public List<CampaignButton> Children=new List<CampaignButton>();
        public void UnHintAll()
        {
            foreach (var item in Children)
            {
                item.Unhint();
            }
        }
    }
}
