using System;
using Modules.TickModule;
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
        
        private Transform _selectedActor;
        private Transform _thirdPlayerPosition;
        private Vector3 _smoothPosition;
        private Vector3 _cameraPosition;
        private Quaternion _cameraRotation;
        
        public ActorFollowerTick(Transform source, float smooth, Vector3 offset)
        {
            _smooth = smooth;
            _offset = offset;
            _cameraTransform = source;
            _cameraRotation = _cameraTransform.rotation;
            _cameraPosition = _cameraTransform.position;
            _smoothPosition = _cameraPosition;
        }
        
        public void Tick()
        {
            if (Enabled)
            {
                _smoothPosition = Vector3.Lerp(_smoothPosition, _selectedActor.position,
                    _smooth * Time.deltaTime);
                _cameraTransform.rotation = Quaternion.Lerp(
                    _cameraTransform.rotation,
                    _cameraRotation, 
                    Time.deltaTime * _smooth);
                
                _cameraTransform.position = _smoothPosition + _offset;

                if (_cameraTransform.position != _cameraPosition)
                {
                    _cameraPosition = _cameraTransform.position;
                    PositionChanged?.Invoke(this, _cameraPosition);
                }
            }
        }
        
        public void FollowActor(Transform actor, Transform cameraPos)
        {
            _selectedActor = actor;
            _thirdPlayerPosition = cameraPos;
        }
    }
}