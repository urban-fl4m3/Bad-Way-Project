using System;
using System.Collections.Generic;
using Common;
using Modules.BattleModule;
using UI.Components;
using UI.Interface;
using UI.Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BattlePlayerControlView : MonoBehaviour, IViewModel, IButtonSubscriber, ICanvasView
    {
        [SerializeField] private Button _moveButton;
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _hideButton;
        [SerializeField] private Transform _iconsParent;
        [SerializeField] private NamedIconComponent _actorSelectorButton;
        [SerializeField] private Canvas _canvas;
        
        public Canvas Canvas => _canvas;
        public GameObject GameObject => gameObject;
        
        private UnityPool<NamedIconComponent> _buttonPool;
        private List<BattleActor> _enemyActors;

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
            var actorsCount = actorDataProviders.Count;

            _buttonPool = new UnityPool<NamedIconComponent>(_actorSelectorButton);
            _buttonPool.ToParent(_iconsParent);

            for (var i = 0; i < actorsCount; i++)
            {
                var buttonInstance = _buttonPool.Instantiate();
                var index = i;

                var actorName = actorDataProviders[index].name;
                var actorIcon = actorDataProviders[index].Icon;
                
                buttonInstance.SetContext(actorName, "", actorIcon);
                buttonInstance.AddButtonListener((sender, args) =>
                {
                    OnActorSelectClick(index);   
                });
            }
            
            _model = (BattlePlayerControlViewModel) model;
            _model.Abc.Changed += AbcOnChanged;
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
        }
        
        public void UnsubscribeButtons()
        {
            _attackButton.onClick.RemoveAllListeners();
        }
    }
}