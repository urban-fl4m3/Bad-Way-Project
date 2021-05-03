using System;
using Common;
using Common.Commands;
using Modules.BattleModule;
using Modules.GridModule.Cells;
using UI.Models;
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
        private BattleActor _actor;
        private Transform _target;
        private Camera _camera;
        private BattleActorParameterModel _model;
        private DynamicValue<BattleActor> _attackActor;
        public RectTransform RectTransform => _rectTransform;

        public void Initialize(BattleActor actor, string actorName, int level, DynamicValue<int> health, int maxHealth,
            Canvas canvas, bool isEnemy, BattleActorParameterModel model)
        {
            _model = model;
            _attackActor = model.ActorAttack;
            _actor = actor;
            _target = actor.Actor.TargetForUI;
            _name.text = actorName;
            _level.text = level.ToString();
            _actorHealthBar.Initialize(health, maxHealth, isEnemy);
            _canvas = canvas;
            _camera = _canvas.worldCamera;
            _actionBar.gameObject.SetActive(!isEnemy);

            if (isEnemy)
            {
                _model.CellSelected += OnCellSelected;
                _model.CellDeselected += OnCellDeselected;
                _attackActor.Changed += OnAttackClick;
            }
            
        }

        private void OnAttackClick(object sender, BattleActor e)
        {
            if (e!=null)
            {
                OnCellSelected(this, e.Placement);
            }
            else
            {
                _cantHit.gameObject.SetActive(false);
                _damage.gameObject.SetActive(false);
                _hitChance.gameObject.SetActive(false);
            }
        }

        private void OnCellDeselected(object sender, EventArgs e)
        {
            _cantHit.gameObject.SetActive(false);
            _damage.gameObject.SetActive(false);
            _hitChance.gameObject.SetActive(false);
        }
        private void OnCellSelected(object sender, Cell e)
        {
            var chance = WeaponMath.HitChance(_actor.Placement, e);
            
            if (chance == 0)
            {
                _cantHit.gameObject.SetActive(true);
                _damage.gameObject.SetActive(false);
                _hitChance.gameObject.SetActive(false);
            }
            else
            {
                _cantHit.gameObject.SetActive(false);
                
                _damage.gameObject.SetActive(true);
                _damage.text = WeaponMath.ActorWeapon.Damage.ToString();
                
                _hitChance.gameObject.SetActive(true);
                _hitChance.text = chance.ToString();
            }
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