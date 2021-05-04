using System;
using Modules.ActorModule.Components;
using Modules.CameraModule.Ticks;
using Modules.TickModule;
using UnityEngine;

namespace Modules.CameraModule.Components
{
    public class SmoothFollowerComponent : BaseActorComponent<SmoothFollowerComponent>
    {
        public event EventHandler<Vector3> PositionChanged;
        
        [SerializeField] private float _smooth;
        [SerializeField] private Vector3 _offset;

        private ActorFollowerTick _actorFollowerTick;

        public void Initialize(ITickManager tickManager)
        {
            _actorFollowerTick = new ActorFollowerTick(transform, _smooth, _offset);
            _actorFollowerTick.PositionChanged += HandlePositionChanged;
            tickManager.AddTick(this, _actorFollowerTick);

            _actorFollowerTick.Enabled = true;
        }

        public void FollowActor(Transform actor, Transform cameraPos)
        {
            _actorFollowerTick.FollowActor(actor, cameraPos);
        }

        private void HandlePositionChanged(object sender, Vector3 e)
        {
            PositionChanged?.Invoke(this, e);
        }
    }
}