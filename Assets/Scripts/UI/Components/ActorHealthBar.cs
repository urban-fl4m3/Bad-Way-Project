﻿using System.Collections.Generic;
using Common;
using Modules.BattleModule;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class ActorHealthBar: MonoBehaviour
    {
        [SerializeField] private Image _healthPointPrefab;
        [SerializeField] private Text _healthPointText;
        [SerializeField] private Transform _healthPointBar;

        private int _maxHealth;
        private DynamicValue<int> _health;
        private List<Image> _healthPoints;

        public void Initialize(DynamicValue<int> nowHealth, int maxHealth)
        {
            _health = nowHealth;
            _maxHealth = maxHealth;

            _healthPointText.text = _health.Value.ToString();
            _healthPoints = new List<Image>();
            
            for (var a = 0; a < maxHealth; a++)
            {
                var healthPoint = Instantiate(_healthPointPrefab, _healthPointBar);
                _healthPoints.Add(healthPoint);
            }

            _health.Changed += OnHealthChange;
        }

        private void OnHealthChange(object sender, int e)
        {
            _healthPointText.text = _health.Value.ToString();

            for (var i = 0; i < _healthPoints.Count; i++)
            {
                if (i > _health.Value)
                {
                    _healthPoints[i].color = Color.gray;
                }
            }
        }

        
    }
}