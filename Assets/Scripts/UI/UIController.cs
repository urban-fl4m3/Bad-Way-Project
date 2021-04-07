using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        public event EventHandler MovementClicked;

        [SerializeField] private Button _moveButton;
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _hideButton;
        
        private void Awake()
        {
            _moveButton.onClick.AddListener(OnMovementButtonClick);    
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    
        private void OnMovementButtonClick()
        {
            MovementClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}