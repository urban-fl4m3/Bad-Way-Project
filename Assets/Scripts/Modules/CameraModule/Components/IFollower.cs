using UnityEngine;

namespace Modules.CameraModule.Components
{
    public interface IFollower
    {
        void Follow();

        void SetParameter(Vector3 offset, Transform camera, float smooth, Quaternion rotation, Transform emptyObj);
    }
}