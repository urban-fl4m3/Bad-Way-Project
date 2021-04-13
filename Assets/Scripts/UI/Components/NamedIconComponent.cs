using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Components
{
    public class NamedIconComponent : MonoBehaviour, IPointerUpHandler,IPointerExitHandler,IPointerDownHandler
    {
        [SerializeField] private Text _name;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;
        [SerializeField] private GameObject statWindow;
        [SerializeField] private Text _state;
        public Text Name => _name;
        public Image Icon => _icon;
        public Button Button => _button;
        public Text State => _state;

        private bool windowEnable;

        
        public void OnPointerUp(PointerEventData eventData)
        {
            if (windowEnable)
            {
                Debug.Log("UnShow");
                windowEnable = false;
                statWindow.SetActive(windowEnable);
            }
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if (windowEnable)
            {
                Debug.Log("UnShow");
                windowEnable = false;
                statWindow.SetActive(windowEnable);
            }
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            
            if (eventData.button != PointerEventData.InputButton.Right)
            {
                return;
            }
            windowEnable = true;
            statWindow.SetActive(windowEnable);
            Debug.Log("Show");
        }
    }
}