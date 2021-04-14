using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Components
{
    public class NamedIconComponent : MonoBehaviour, IPointerUpHandler,IPointerExitHandler,IPointerDownHandler
    {
        [SerializeField] private Text _name;
        [SerializeField] private Text _state;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;
        [SerializeField] private GameObject statWindow;

        private bool _windowEnable;

        public void SetContext(string nameText,  string stateText, Sprite icon)
        {
            _name.text = nameText;
            _state.text = stateText;
            _icon.sprite = icon;
        }

        public void AddButtonListener(EventHandler eventHandler)
        {
            _button.onClick.AddListener(() =>
            {
                eventHandler?.Invoke(this, EventArgs.Empty);
            });
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            if (_windowEnable)
            {
                _windowEnable = false;
                statWindow.SetActive(_windowEnable);
            }
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            if (_windowEnable)
            {
                _windowEnable = false;
                statWindow.SetActive(_windowEnable);
            }
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Right)
            {
                return;
            }
            
            _windowEnable = true;
            statWindow.SetActive(_windowEnable);
        }

        private void OnDestroy()
        {
            Clear();
        }

        private void OnDisable()
        {
            Clear();
        }

        private void Clear()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}