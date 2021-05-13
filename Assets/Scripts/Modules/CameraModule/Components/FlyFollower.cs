using UnityEngine;

namespace Modules.CameraModule.Components
{
    public class FlyFollower: IFollower
    {
        private Transform _target;
        private Transform _cameraTransform;
        private Vector3 _centerPosition;
        private Quaternion _cameraRotation;
        private Vector3 _offfset;
        private Vector3 _smoothPosition;
        private float _smooth;
        private float _startRotateY;
        private Vector3 _nowPosition = Vector3.zero;

        public FlyFollower(Transform target)
        {
            _target = target;
        }

        public void Follow()
        {
            var smoothOffset = Quaternion.Euler(0, _startRotateY, 0) * _offfset;
            
            _smoothPosition = Vector3.Lerp(_smoothPosition, _target.transform.position, _smooth * Time.deltaTime);
            _cameraTransform.position = _smoothPosition + smoothOffset + _nowPosition;
            
            var eulRotation = _cameraRotation.eulerAngles;
            _cameraRotation.eulerAngles = new Vector3(eulRotation.x, _startRotateY, eulRotation.z);
            
            var eulerAngles = _cameraTransform.eulerAngles;
            eulerAngles = new Vector3(eulerAngles.x, _startRotateY,eulerAngles.z);
            
            _cameraTransform.eulerAngles = eulerAngles;

            _cameraTransform.rotation =
                Quaternion.Lerp(_cameraTransform.rotation, _cameraRotation, _smooth * Time.deltaTime);
        }

        public void SetParameter(Vector3 offset, Transform camera, float smooth, Quaternion cameraRotation,
            Transform emptyObj)
        {
            _offfset = offset;
            _cameraTransform = camera;
            _smooth = smooth;
            _cameraRotation = cameraRotation;
            _startRotateY = _cameraTransform.eulerAngles.y;
            
            var position = _cameraTransform.position;
            var smoothOffset = Quaternion.Euler(0, _startRotateY, 0) * _offfset;
            
            _smoothPosition = position-smoothOffset;
            _centerPosition = position;
            
            InputEventsHandler.OnWASDInput += OnMouseOverScene;
            InputEventsHandler.OnMouseScrollRotate += OnMouseRotate;
        }

        private void OnMouseRotate(object sender, float e)
        {
            _startRotateY += e * Time.deltaTime * 90;
        }

        private void OnMouseOverScene(object sender, Vector2 vector2)
        {
            var vector = Quaternion.Euler(0, _startRotateY, 0) * new Vector3(vector2.x,0,vector2.y);
            _nowPosition += vector * (Time.deltaTime * 10);
        }
        
    }
}