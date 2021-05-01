using System.Collections;
using System.Collections.Generic;
using Common;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

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
        private List<Image> _healthPoints;

        public void Initialize(DynamicValue<int> nowHealth, int maxHealth, bool isEnemy)
        {
            var color = Color.green;
            if (isEnemy)
                color = Color.red;

            _health = nowHealth;
            _maxHealth = maxHealth;

            _healthPointText.text = _health.Value.ToString();
            _healthPoints = new List<Image>();
            
            for (var a = 0; a < maxHealth; a++)
            {
                var healthPoint = Instantiate(_healthPointPrefab, _healthPointBar);
                healthPoint.color = color;
                _healthPoints.Add(healthPoint);
            }

            _health.Changed += OnHealthChange;
        }

        private void OnHealthChange(object sender, int e)
        {
            _healthPointText.text = _health.Value.ToString();
            
            StartCoroutine(ShowDamagePoint(e));
            
            for (var i = 0; i < _healthPoints.Count; i++)
            {
                if (i > _health.Value)
                {
                    _healthPoints[i].color = Color.gray;
                }
            }
        }
        
        private IEnumerator ShowDamagePoint(int e)
        {
            var damageText = Instantiate(_damageText, _damagePanel).GetComponent<Text>();
            var color = damageText.color;
            var position = damageText.transform.localPosition;
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
            
            Destroy(damageText.gameObject);
        }
        
    }
}