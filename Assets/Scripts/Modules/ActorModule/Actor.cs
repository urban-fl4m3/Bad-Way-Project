using System;
using Common;
using Modules.ActorModule.Components;
using Modules.BattleModule;
using UnityEngine;

namespace Modules.ActorModule
{
    public class Actor : MonoBehaviour
    {
        public EventHandler<BattleActor> ActorSelect;
        public EventHandler ActorUnSelect;
        public BattleActor BattleActor;
        
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
            ActorSelect?.Invoke(this,BattleActor);
        }
        
        public T GetActorComponent<T>()
            where T : class, IActorComponent
        {
            return _container.Resolve<T>();
        }
    }
}