using System;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        public event EventHandler MovementClicked;
        public event EventHandler AtackClicked;
        public event EventHandler SelectedClick;

        [SerializeField] private Button _moveButton;
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _hideButton;
        [SerializeField] private Button _actorButton1;
        [SerializeField] private Button _actorButton2;
        [SerializeField] private Button _actorButton3;
        
        private void Awake()
        {
            _moveButton.onClick.AddListener(OnMovementButtonClick);    
            _attackButton.onClick.AddListener(OnAtackButtonClick);
            _hideButton.onClick.AddListener(OnHideButtonClick);
            //_actorButton1.onClick.AddListener(OnActorSelectClick(1));
          //  _actorButton2.onClick.AddListener(OnActorSelectClick(1));
          //  _actorButton3.onClick.AddListener(OnActorSelectClick(1));
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnActorSelectClick(int i)
        {
            
        }
        private void OnMovementButtonClick()
        {
            MovementClicked?.Invoke(this, EventArgs.Empty);
        }
        private void OnAtackButtonClick()
        {
            AtackClicked?.Invoke(this, EventArgs.Empty);
        }
        private void OnHideButtonClick()
        {
            
        }

    }
}