using System;
using System.Collections.Generic;
using Modules.BattleModule;
using Modules.BattleModule.Stats.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class ActorHealthBar: MonoBehaviour
    {
        [SerializeField] private Image _healthPointPrefab;
        [SerializeField] private Text _healthPointText;
        [SerializeField] private Transform _healthPointBar;

        private List<Image> _healthPoints;
        private BattleActor _battleActor;

        public void Initialize(BattleActor battleActor)
        {
            _battleActor = battleActor;
            _battleActor.HealthChanged += OnHealthChange;
            _healthPoints = new List<Image>();

            int health = battleActor.Stats[SecondaryStat.Health];
            _healthPointText.text = health.ToString();
            
            for (int i = 0; i < health; i++)
            {
                var helthpoint = Instantiate(_healthPointPrefab, _healthPointBar);
                _healthPoints.Add(helthpoint);
            }
        }

        private void OnHealthChange(object sender, int e)
        {
            int nowHealth = _battleActor.Stats[SecondaryStat.Health];
            
            _healthPointText.text = nowHealth.ToString();

            for (int i = 0; i < _healthPoints.Count; i++)
            {
                if (i > nowHealth)
                {
                    _healthPoints[i].color = Color.gray;
                }
            }
        }

        
    }
}