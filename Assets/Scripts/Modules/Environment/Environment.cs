using UnityEngine;

namespace Modules.Environment
{
    public class Environment: MonoBehaviour
    {
        public EnvironmentType EnvironmentType;

        public bool CanMove()
        {
            return EnvironmentType == EnvironmentType.Window || EnvironmentType == EnvironmentType.Decoration || EnvironmentType == EnvironmentType.Wall;
    }
    }
}