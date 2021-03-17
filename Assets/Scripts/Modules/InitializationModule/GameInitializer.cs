using Modules.GridModule;
using Modules.GridModule.Data;
using UnityEngine;

namespace Modules.InitializationModule
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private GridData _gridData;

        private GridController _gridController;
        
        private void Start()
        {
            var gridBuilder = new GridBuilder("Default Grid");
            _gridController = gridBuilder.Build(_gridData);
        }
    }
}
