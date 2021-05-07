using UnityEngine;

namespace Modules.CameraModule.Components
{
    public class FlyFollower: IFollower
    {
        private Transform _target;
        private Transform _cameraTransform;
        private Quaternion _cameraRotation;
        private Vector3 _offfset;
        private Vector3 _smoothPosition;
        private float _smooth;
        private Vector2 _nowPosition = Vector2.zero;

        public FlyFollower(Transform target)
        {
            _target = target;
        }

        public void Follow()
        {
            var mouseOffset = new Vector3(_nowPosition.x, 0, _nowPosition.y);
            _smoothPosition = Vector3.Lerp(_smoothPosition, _target.transform.position, _smooth * Time.deltaTime);
            _cameraTransform.position = _smoothPosition + _offfset + mouseOffset;
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
            _smoothPosition = _cameraTransform.position-offset;
            MouseEventsHandler.OnMouseOverScene += OnMouseOverScene;
        }

        private void OnMouseOverScene(object sender, Vector2 vector2)
        {
            _nowPosition += vector2 * Time.deltaTime * 10;
        }
    }
}