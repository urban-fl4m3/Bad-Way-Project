using Modules.ActorModule;
using Modules.BattleModule.Levels.Providers;
using Modules.BattleModule.Stats;
using Modules.GridModule;
using UnityEngine;

namespace Modules.InitializationModule
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private LevelDataProvider _levelData;
        [SerializeField] private AvailableBattleStatsProvider _statsProvider;
        [SerializeField] private AvailableActorsProvider _availableActorsProvider;

        private GridController _gridController;
        
        private void Start()
        {
            var gridBuilder = new GridBuilder("Default Grid");
            _gridController = gridBuilder.Build(_levelData.GridData);

            foreach (var levelActor in _levelData.Actors)
            {
                var actorPrefab = _availableActorsProvider.GetActorById(levelActor.Id);
                var position = _gridController[levelActor.Cell].Component.transform.position;

                Instantiate(actorPrefab, position, Quaternion.identity);
            }
        }
    }
}
