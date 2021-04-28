using Common;
using Modules.BattleModule;
using UI.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class ActorUIParameters : MonoBehaviour
    { 
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Transform _target;
        [SerializeField] private ActorHealthBar _actorHealthBar;
        [SerializeField] private Text _name;
        [SerializeField] private Text _level;
        
        public RectTransform RectTransform => _rectTransform;
        public DynamicValue<int> health;

        public void Initialize(Transform target, string name, int level, DynamicValue<int> health, int maxHealth)
        {
            _target = target;
            _name.text = name;
            _level.text = level.ToString();
            _actorHealthBar.Initialize(health, maxHealth);
        }

        public void UpdatePosition()
        {
            if (!_target)
                return;

            Camera camera = Camera.main;
            Vector3 screeOffset = new Vector3(Screen.width / 2, Screen.height / 2);

            var position = camera.WorldToScreenPoint(_target.position) - screeOffset;
            _rectTransform.localPosition = position;
        }
    }
}