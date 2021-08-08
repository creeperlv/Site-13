using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Site13Kernel.UI.Customed
{
    public class UIButton : Selectable, IPointerClickHandler, ISubmitHandler, IVisualElement
    {
        public Text ContentText;
        public string Content
        {
            get
            {
                return ContentText.text;
            }
            set
            {
                ContentText.text= value;
            }
        }
        [Serializable]
        public class ButtonClickedEvent : UnityEvent
        {
        }

        [SerializeField]
        private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();

        protected UIButton()
        {
        }
        public ButtonClickedEvent onClick
        {
            get
            {
                return m_OnClick;
            }
            set
            {
                m_OnClick = value;
            }
        }

        private void Press()
        {
            if (!IsActive() || !IsInteractable())
                return;

            UISystemProfilerApi.AddMarker("Button.onClick", this);
            m_OnClick.Invoke();
        }
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            Press();
        }

        public virtual void OnSubmit(BaseEventData eventData)
        {
            Press();

            // if we get set disabled during the press
            // don't run the coroutine.
            if (!IsActive() || !IsInteractable())
                return;

            DoStateTransition(SelectionState.Pressed, false);
            StartCoroutine(OnFinishSubmit());
        }

        private IEnumerator OnFinishSubmit()
        {
            var fadeTime = colors.fadeDuration;
            var elapsedTime = 0f;

            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
            }

            DoStateTransition(currentSelectionState, false);
        }

        public void SetProperty(string Key, object Value)
        {
            switch (Key)
            {
                case "Visibility":
                    this.gameObject.SetActive(Value);
                    break;
                default:
                    SideSetProperty(Key, Value);
                    break;
            }

        }

        public virtual void SideSetProperty(string Key, object Value)
        {

        }
        public void SetProperty(string Key, string Value)
        {
            switch (Key)
            {
                case "Visibility":
                    this.gameObject.SetActive(bool.Parse(Value));
                    break;
                default:
                    SideSetProperty(Key, Value);
                    break;
            }

        }
        public virtual void SideSetProperty(string Key, string Value)
        {

        }
        public Property GetProperty(string Key)
        {
            switch (Key)
            {
                case "Visibility":
                    return new Property { Key = Key, Value = this.gameObject.activeInHierarchy };
                default:

                    break;
            }
            return SideGetProperty(Key);
        }
        public virtual Property SideGetProperty(string Key)
        {
            return null;
        }
        public object GetPropertyValue(string Key)
        {
            return GetProperty(Key).Value;
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }

}
