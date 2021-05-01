using System;
using Common;
using Modules.ActorModule.Components;
using UnityEngine;

namespace Modules.ActorModule
{
    public class Actor : MonoBehaviour
    {
        public event EventHandler ActorSelected;
        public event EventHandler ActorDeselected;
        
        public Transform Transform => transform;
        public Transform TargetForUI;
        public Transform ThirdPersonCamera;

        private readonly TypeContainer _container = new TypeContainer();

        
        private void Awake()
        {
            Debug.Log("2");
            var components = GetComponents<IActorComponent>();
            foreach (var actorComponent in components)
            {
                actorComponent.Initialize(_container);
            }
            
            _container.Resolve<ActorCollisionComponent>().Selected += OnClick;
            _container.Resolve<ActorCollisionComponent>().Deselected += OnActorUnSelect;
        }

        private void OnActorUnSelect(object sender, EventArgs e)
        {
            ActorDeselected?.Invoke(this,null);
        }

        private void OnClick(object sender, EventArgs e)
        {
            ActorSelected?.Invoke(this, EventArgs.Empty);
        }
        
        public T GetActorComponent<T>() where T : class, IActorComponent
        {
            return _container.Resolve<T>();
        }
    }
}