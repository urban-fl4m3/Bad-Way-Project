using System;
using System.Collections.Generic;
using Common;
using Modules.ActorModule;
using Modules.BattleModule;
using Modules.BattleModule.Stats;
using UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BattlePlayerControlsView : MonoBehaviour
    {
        public event EventHandler MovementClicked;
        public event EventHandler AtackClicked;
        public event EventHandler<Actor> ActorClick;
        public event EventHandler<int> SelectedClick;

        [SerializeField] private Button _moveButton;
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _hideButton;
        [SerializeField] private GameObject _enemyWindow;
        [SerializeField] private Transform _iconsParent;
        [SerializeField] private NamedIconComponent _actorSelectorButton;
        
        private UnityPool<NamedIconComponent> _buttonPool;
        private List<BattleActor> _playerActors;

        public void SubscribeEnemy(IEnumerable<BattleActor> enemyActor)
        {
            foreach (var actor in enemyActor)
            {
                actor.Actor.ActorSelect += OnEnemyActorClick;
            }
        }
        
        public void Initialize(List<ActorDataProvider> actorDataProviders, List<BattleActor> playerActors)
        {
            var actorsCount = actorDataProviders.Count;
            _playerActors = playerActors;
            
            _moveButton.onClick.AddListener(OnMovementButtonClick);    
            _attackButton.onClick.AddListener(OnAtackButtonClick);
            _hideButton.onClick.AddListener(OnHideButtonClick);

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
        }

        private void OnEnemyActorClick(object sender, Actor e)
        {
            _enemyWindow.SetActive(true);
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
            Debug.Log($"Player has selected {i} character");

            SelectedClick?.Invoke(this, i);
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