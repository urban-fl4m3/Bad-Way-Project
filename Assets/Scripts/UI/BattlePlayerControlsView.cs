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
        public event EventHandler<int> SelectedClick;

        [SerializeField] private Button _moveButton;
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _hideButton;
        //Префаб кнопки. Будем создавать столько, сколько нам нужно
        [SerializeField] private Transform _iconsParent;
        [SerializeField] private NamedIconComponent _actorSelectorButton;
        
        //У нас должны быть кнопки в списке, т.к. ты никогда не знаешь сколько у нас персонажей в бою
        //их инициализация должна проходить в отдельном методе        
        private UnityPool<NamedIconComponent> _buttonPool;
        private List<BattleActor> _playerActors;
        //Меняем awake на свой метод, для большего контроля юнити объектом
        //Нужно передать не просто константу, а вытащить количество текущих активных юнитов у игрока
        //А скорее всего даже массив с данными, чтобы мы могли настраивать еще и иконки
        public void Initialize(List<ActorDataProvider> actorDataProviders, List<BattleActor> playerActors)
        {
            var actorsCount = actorDataProviders.Count;
            _playerActors = playerActors;
            _moveButton.onClick.AddListener(OnMovementButtonClick);    
            _attackButton.onClick.AddListener(OnAtackButtonClick);
            _hideButton.onClick.AddListener(OnHideButtonClick);

            //Первая перегрузка конструктора, но если мы захотим создавать иконки, то лучше использовать следующую
            //_buttonPool = new UnityPool<Button>(_actorSelectorButton, actorsCount);
            _buttonPool = new UnityPool<NamedIconComponent>(_actorSelectorButton);
            _buttonPool.ToParent(_iconsParent);

            for (var i = 0; i < actorsCount; i++)
            {
                var buttonInstance = _buttonPool.Instantiate();
                
                //buttonInstance.State.text = _availableBattleStatsProvider.SecondaryStatsDataProvider.SecondaryStats
                
                //Важно создать новый int перед вызовом метода в делегате, иначе по правилм C#
                //у тебя всегда будет i == Count самого списка. Отвязывать индекс нужно делегироания!!
                //Проверь что будет, если перенести index = i или вообще использовать само i внутри делегата,
                //чтобы понять, как работает C# в таком случае
                var index = i;

                var actorName = actorDataProviders[index].name;
                var actorIcon = actorDataProviders[index].Icon;
                
                buttonInstance.SetContext(actorName, "", actorIcon);
                buttonInstance.AddButtonListener((sender, args) =>
                {
                    OnActorSelectClick(index);   
                });
                
                //Здесь мы должны сетить иконку актера исходя из данных, переданных методу Initialize
                //Хранить иконку можем в классе данных актера
            }

            //Метод для создания объектов в пуле из переданного в него префаба
            //Трансформ родителя нужно указывать заранее 
            //_buttonPool.Instantiate();
            
            //Метод для ресайза пула. Все созданные элементы, не входящие в новый размер будут скрыты
            //buttonPool.Resize(2);
            
            //Метод для подогрева пула к определенному размеру. Он заполнит пул, пока его размер не будет достигнут
            //указанной величины. Если величина меньше размера пула, то будет создано 0 объектов. Метод возвращает кол-во
            //созданных объектов в пуле
            //_buttonPool.Warm(2);
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