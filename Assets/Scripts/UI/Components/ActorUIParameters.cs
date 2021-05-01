using Common;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class ActorUIParameters : MonoBehaviour
    { 
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private ActorHealthBar _actorHealthBar;
        [SerializeField] private Text _name;
        [SerializeField] private Text _level;
        [SerializeField] private RectTransform _actionBar;
        [SerializeField] private Text _damage;
        [SerializeField] private Text _hitChance;
        [SerializeField] private Text _cantHit;

        private Canvas _canvas;
        private Transform _target;
        private Camera _camera;

        public void Initialize(Transform target, string actorName, int level, DynamicValue<int> health, int maxHealth,
            Canvas canvas, bool isEnemy)
        {
            _target = target;
            _name.text = actorName;
            _level.text = level.ToString();
            _actorHealthBar.Initialize(health, maxHealth, isEnemy);
            _canvas = canvas;
            _camera = _canvas.worldCamera;
            _actionBar.gameObject.SetActive(!isEnemy);
        }

        public void UpdatePosition()
        {
            if (!_target)
                return;

            var position = _camera.WorldToScreenPoint(_target.position) / _canvas.scaleFactor;
            _rectTransform.localPosition = position;
        }
    }
}