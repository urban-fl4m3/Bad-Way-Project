using System;
using Common;
using Modules.ActorModule.Components;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Modules.ActorModule
{
    public class Actor : MonoBehaviour
    {
        public event EventHandler<Actor> ActorSelect;
        public event EventHandler ActorUnSelect;
        public event EventHandler<int> HealthChange;
        public Transform Transform => transform;
        
        private readonly TypeContainer _container = new TypeContainer();

        public int _valueHealth;
        public int _health;
        
        private void Awake()
        {
            var components = GetComponents<IActorComponent>();
            foreach (var actorComponent in components)
            {
                actorComponent.Initialize(_container);
            }
            
            _container.Resolve<ActorCollisionComponent>().ActorSelected += OnActorClick;
            _container.Resolve<ActorCollisionComponent>().ActorUnSelcted += OnActorUnSelect;
        }

        private void OnActorUnSelect(object sender, EventArgs e)
        {
            ActorUnSelect?.Invoke(this,null);
        }

        private void OnActorClick(object sender, Actor e)
        {
            ActorSelect?.Invoke(this,this);
        }
        
        public T GetActorComponent<T>()
            where T : class, IActorComponent
        {
            return _container.Resolve<T>();
        }

        public void SetHelth(int maxHealth, int health)
        {
            _valueHealth = maxHealth;
            _health = 10;
        }

        public void GetDamage(int DP)
        {
            _health -= DP;
            HealthChange?.Invoke(this,_health);
            
            if(_health <= 0)
                GetActorComponent<ActorAnimationComponent>().Animator.SetTrigger("Death");
        }
    }
}