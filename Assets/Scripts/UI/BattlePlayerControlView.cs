using System;
using Common;
using UI.Components;
using UI.Interface;
using UI.Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI
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

        public void SetActiveAllButton(bool isActive)
        {
            _moveButton.interactable = isActive;
            _attackButton.interactable = isActive;
            _hideButton.interactable = isActive;
        }
        
        public void ResolveModel(IModel model)
        {
            _model = (BattlePlayerControlViewModel) model;
            CreateActorIcon();
        }
        
        public void Clear()
        {
            
        }

        private void AbcOnChanged(object sender, bool e)
        {
            if (e)
            {
                
            }
            else
            {
                
            }
        }

        public void SubscribeButtons()
        {
            _attackButton.onClick.AddListener(() =>
            {
                _model.AttackClicked?.Invoke(this, EventArgs.Empty);
            });

            _moveButton.onClick.AddListener(() =>
            {
                _model.MovementClicked?.Invoke(this, EventArgs.Empty);
            });
            
            for (int a = 0; a < _buttonPool.Count; a++)
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

            for (int i = 0; i < actorCount; i++)
            {
                var buttonInstance = _buttonPool.Instantiate();
                var index = i;

                var actorName = actorDataProvider[index].Name;
                var actorIcon = actorDataProvider[index].Icon;
                
                buttonInstance.SetContext(actorName, "" ,actorIcon);
            }
        }

        
    }
}