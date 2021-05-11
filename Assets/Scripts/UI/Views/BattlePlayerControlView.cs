using System;
using Common;
using UI.Components;
using UI.Interface;
using UI.Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class BattlePlayerControlView : MonoBehaviour, IViewModel, IButtonSubscriber, ICanvasView, IActorButton
    {
        [SerializeField] private Button _moveButton;
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _hideButton;
        [SerializeField] private Transform _iconsParent;
        [SerializeField] private NamedIconComponent _actorSelectorButton;
        public Canvas Canvas { get; set ; }
        

        public GameObject GameObject => gameObject;
        
        private UnityPool<NamedIconComponent> _buttonPool;

        private BattlePlayerControlViewModel _model;

        public void ResolveModel(IModel model)
        {
            _model = (BattlePlayerControlViewModel) model;
            _model.PlayerStanding.Changed += OnPlayerMoving;
            _model.PlayerEndTurn.Changed += OnPlayerEndTurn;
            CreateActorIcon();
        }

        public void Clear()
        {
            
        }
        
        public void ResetCanvas()
        {
            
        }

        public void SubscribeButtons()
        {
            _attackButton.onClick.AddListener(() => { _model.AttackClicked?.Invoke(this, EventArgs.Empty); });
            _moveButton.onClick.AddListener(() => { _model.MovementClicked?.Invoke(this, EventArgs.Empty); });

            for (var a = 0; a < _buttonPool.Count; a++)
            {
                var index = a;
                _buttonPool[a].AddButtonListener((sender, args) =>
                {
                    _model.SelectedClick?.Invoke(this, index);
                });
            }
        }

        public void UnsubscribeButtons()
        {
            _attackButton.onClick.RemoveAllListeners();
            _moveButton.onClick.RemoveAllListeners();
        }

        public void CreateActorIcon()
        {
            var actorDataProvider = _model.ActorDataProvider;
            var actorCount = _model.ActorDataProvider.Count;
            
            _buttonPool = new UnityPool<NamedIconComponent>(_actorSelectorButton);
            _buttonPool.ToParent(_iconsParent);

            for (var i = 0; i < actorCount; i++)
            {
                var buttonInstance = _buttonPool.Instantiate();
                var index = i;

                var actorName = actorDataProvider[index].Name;
                var actorIcon = actorDataProvider[index].Icon;
                
                buttonInstance.SetContext(actorName, "" ,actorIcon);
            }
        }

        private void OnPlayerEndTurn(object sender, bool e)
        {
            Canvas.enabled = !e;
        }

        private void OnPlayerMoving(object sender, bool e)
        {
            _moveButton.interactable = e;
            _attackButton.interactable = e;
            _hideButton.interactable = e;
        }
        
    }
}