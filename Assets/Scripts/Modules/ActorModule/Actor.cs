using System;
using Common;
using Modules.ActorModule.Components;
using UnityEngine;

namespace Modules.ActorModule
{
    public class Actor : MonoBehaviour
    {
        public event EventHandler<Actor> ActorSelect;
        public event EventHandler ActorUnSelect;
        
        public Transform Transform => transform;
        public Transform TargetForUI;
        
        private readonly TypeContainer _container = new TypeContainer();

        
        private void Awake()
        {
            var components = GetComponents<IActorComponent>();
            foreach (var actorComponent in components)
            {
                actorComponent.Initialize(_container);
            }
            
            _container.Resolve<ActorCollisionComponent>().ActorSelected += OnActorClick;
            _container.Resolve<ActorCollisionComponent>().ActorUnSelected += OnActorUnSelect;
        }

        private void OnActorUnSelect(object sender, EventArgs e)
        {
            ActorUnSelect?.Invoke(this,null);
        }

        private void OnActorClick(object sender, EventArgs e)
        {
            ActorSelect?.Invoke(this,this);
        }
        
        public T GetActorComponent<T>()
            where T : class, IActorComponent
        {
            return _container.Resolve<T>();
        }
    }
}