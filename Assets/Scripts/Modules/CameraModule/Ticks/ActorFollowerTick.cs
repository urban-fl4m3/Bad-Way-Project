using System;
using System.Collections.Generic;
using Modules.CameraModule.Components;
using Modules.TickModule;
using UnityEditor;
using UnityEngine;

namespace Modules.CameraModule.Ticks
{
    public class ActorFollowerTick : ITickLateUpdate
    {
        public event EventHandler<Vector3> PositionChanged;
        public bool Enabled { get; set; }
        
        private readonly float _smooth;
        private readonly Vector3 _offset;
        private readonly Transform _cameraTransform;
        
        private Vector3 _cameraPosition;
        private Quaternion _cameraRotation;
        private Quaternion _cameraCurrentRotation;
        
        private Transform _emptyPoint;
        private int selectedEnemy;

        private IFollower _follower;
        
        public ActorFollowerTick(Transform source, float smooth, Vector3 offset)
        {
            _smooth = smooth;
            _offset = offset;
            _cameraTransform = source;
            _cameraRotation = _cameraTransform.rotation;
            _cameraCurrentRotation = _cameraRotation;
            _cameraPosition = _cameraTransform.position;
            
            var emptyGameObject = new GameObject("emptyPoint");
            
            _emptyPoint = emptyGameObject.transform;
        }
        public void Tick()
        {
            if (!Enabled)
                return;
            
            _follower.Follow();

            if (_cameraTransform.position != _cameraPosition|| _cameraTransform.rotation !=_cameraCurrentRotation)
            {
                _cameraPosition = _cameraTransform.position;
                _cameraCurrentRotation = _cameraTransform.rotation;
                
                PositionChanged?.Invoke(this, _cameraPosition);
            }

        }
        public void FollowActor(IFollower follower)
        {
            _follower = follower;
            _follower.SetParameter(_offset, _cameraTransform, _smooth, _cameraRotation, _emptyPoint);
        }

        
    }
}