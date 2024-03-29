﻿using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEditor.Hardware;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class ActorHealthBar: MonoBehaviour
    {
        [SerializeField] private Image _healthPointPrefab;
        [SerializeField] private Text _healthPointText;
        [SerializeField] private Transform _healthPointBar;
        [SerializeField] private GameObject _damageText;
        [SerializeField] private Transform _damagePanel;

        private int _maxHealth;
        private DynamicValue<int> _health;
        private int _lastHealth;
        private List<Image> _healthPoints;
        private List<Text> _damageTexts = new List<Text>();
        private List<Text> _usedTexts = new List<Text>();
        private Color _color;
        
        public void Initialize(DynamicValue<int> nowHealth, int maxHealth, bool isEnemy)
        {
            var color = Color.green;
            if (isEnemy)
                color = Color.red;

            _color = color;
            _health = nowHealth;
            _lastHealth = _health.Value;
            _maxHealth = maxHealth;

            _healthPointText.text = _health.Value.ToString();
            _healthPoints = new List<Image>();
            _damageTexts = new UnityPool<Text>( _damageText.GetComponent<Text>());
            
            for (var a = 0; a < maxHealth; a++)
            {
                var healthPoint = Instantiate(_healthPointPrefab, _healthPointBar);
                healthPoint.color = color;
                _healthPoints.Add(healthPoint);
            }
            
            _health.Changed += OnHealthChange;
        }

        public void OnEnable()
        {
            if(_usedTexts.Count==0)
                return;
            
            foreach (var usedText in _usedTexts)
            {
                var col = usedText.color;
                usedText.color = new Color(col.r, col.g, col.b, 0);
                usedText.transform.localPosition= Vector3.zero;
            }
            _damageTexts.AddRange(_usedTexts);
            _usedTexts.Clear();
        }

        private void OnHealthChange(object sender, int e)
        {
            _healthPointText.text = _health.Value.ToString();
            for (var i = 0; i < _healthPoints.Count; i++)
            {
                _healthPoints[i].color = i >= _health.Value ? Color.gray : _color;
            }

            if (_damageTexts.Count > 0)
            {
                StartCoroutine(ShowDamagePoint(_lastHealth-e, _damageTexts[0]));
            }
            else
            {
                var damageText = Instantiate(_damageText, _damagePanel).GetComponent<Text>();
                _damageTexts.Add(damageText);       
                StartCoroutine(ShowDamagePoint(_lastHealth-e,_damageTexts[0]));
            }

            _lastHealth = e;    
        }

        private IEnumerator ShowDamagePoint(int e, Text damageText)
        {
            _usedTexts.Add(damageText);
            _damageTexts.Remove(damageText);
            
            var color = damageText.color;
            color = new Color(color.r, color.g, color.b, 1);
            damageText.color = color;
            
            var position = Vector3.zero;
            var offsetPosition = position + new Vector3(40, 25); 

            damageText.text = "-" + e;

            while (damageText.color.a!=0)
            {
                position = Vector3.Lerp(position,offsetPosition,Time.deltaTime);
                color.a = Mathf.MoveTowards(color.a, 0, Time.deltaTime);
                
                damageText.color = color;
                damageText.transform.localPosition = position;
                yield return null;
            }

            _usedTexts.Remove(damageText);
            _damageTexts.Add(damageText);
        }
        
    }
}