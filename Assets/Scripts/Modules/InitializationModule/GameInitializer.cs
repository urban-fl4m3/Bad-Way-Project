using Modules.EcsModule;
using UnityEngine;

namespace Modules.InitializationModule
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private GameObject _testObject;
        
        private void Start()
        {
            var worldBuilder = new WorldBuilder();
            var battleWorldController = worldBuilder.Build("Battle World");
        }
    }
}
