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
using Site13Kernel.GameLogic;
using Site_13ToolLib.Globalization;

namespace Site13Kernel.UI.Customed
{
    public class CampaignButton : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler, IVisualElement
    {
        public GameObject HintImage;
        public Image BackgroundImage;
        public Text MissionName;
        public Action OnClick=null;
        [HideInInspector]
        public CampaignButtonGroup CampaignParent;
        protected CampaignButton()
        {
        }
        MissionDefinition definition;
        public void Init(CampaignButtonGroup parent,MissionDefinition definition)
        {
            CampaignParent = parent;
            parent.Children.Add(this);
            this.definition= definition;
            BackgroundImage.sprite = GameInfo.CurrentGameDef.Sprites[this.definition.ImageID].LoadedSprite;
            MissionName.text = Language.GetString(LanguageCategory.UI, this.definition.NameID, this.definition.DispFallback);
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
