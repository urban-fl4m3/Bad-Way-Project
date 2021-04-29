using Common;
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

        private Transform _target;
        
        public void Initialize(Transform target, string actorName, int level, DynamicValue<int> health, int maxHealth)
        {
            _target = target;
            _name.text = actorName;
            _level.text = level.ToString();
            _actorHealthBar.Initialize(health, maxHealth);
        }

        public void UpdatePosition()
        {
            if (!_target)
                return;

            //Cringe peredat' cameru
            var camera = Camera.main;
            var screeOffset = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f);

            var position = camera.WorldToScreenPoint(_target.position) - screeOffset;
            _rectTransform.localPosition = position;
        }
    }
}