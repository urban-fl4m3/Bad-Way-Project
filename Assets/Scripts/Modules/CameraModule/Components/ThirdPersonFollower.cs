using System.Collections.Generic;
using UnityEngine;

namespace Modules.CameraModule.Components
{
    public class ThirdPersonFollower: IFollower
    {
        private Transform _target;
        private Transform _emptyObject;
        private Transform _actor;
        private List<Transform> _enemies;
        private Transform _cameraTransform;
        private Quaternion _cameraRotation;
        private Vector3 _offfset;
        private Vector3 _smoothPosition;
        private float _smooth;
        private int _selectedEnemy;

        public ThirdPersonFollower(List<Transform> enemies,Transform target, Transform actor)
        {
            _enemies = enemies;
            _target = target;
            _actor = actor;
        }

        public void Follow()
        {
            _smoothPosition = Vector3.Lerp(_smoothPosition, _target.transform.position, _smooth * Time.deltaTime);
            _cameraTransform.position = _smoothPosition;
                
            _emptyObject.LookAt(_enemies[_selectedEnemy].position+Vector3.up);
            _emptyObject.position = _smoothPosition;

            _actor.eulerAngles = new Vector3(0, _emptyObject.eulerAngles.y);
            
            _cameraTransform.rotation =
                Quaternion.Lerp(_cameraTransform.rotation, _emptyObject.rotation, _smooth * Time.deltaTime);
        }

        public void SetParameter(Vector3 offset, Transform camera, float smooth, Quaternion cameraRotation,
            Transform emptyObj)
        {
            _offfset = offset;
            _cameraTransform = camera;
            _smooth = smooth;
            _cameraRotation = cameraRotation;
            _smoothPosition = _cameraTransform.position;
            MouseEventsHandler.OnMouseScroll += OnMouseScroll;
            _emptyObject = emptyObj;
        }

        private void OnMouseScroll(object sender, int a)
        {
            _selectedEnemy += a;

            if (_selectedEnemy >= _enemies.Count)
                _selectedEnemy = 0;
            
            if (_selectedEnemy < 0)
                _selectedEnemy = _enemies.Count - 1;
        }
    }
}