using Modules.BattleModule;
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

        public void OnChanged(bool value)
        {
            gameObject.SetActive(value);
        }
        
        public RectTransform RectTransform => _rectTransform;
        
        public void Initialize(BattleActor target, BattleActorParametersView battleActorParametersView)
        {
            _target = target.Actor.TargetForUI;
            _actorHealthBar.Initialize(target);
            _name.text = target.Actor.name;
            battleActorParametersView.OnEnableUI += OnChanged;
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