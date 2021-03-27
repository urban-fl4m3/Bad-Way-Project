using Modules.BattleModule.Factories;
using Modules.BattleModule.Levels.Providers;
using Modules.BattleModule.Stats;
using UnityEngine;

namespace Modules.InitializationModule
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private LevelDataProvider _levelData;
        [SerializeField] private AvailableBattleStatsProvider _statsProvider;

        private void Start()
        {
            var battleSceneFactory = new BattleSceneFactory(_levelData, _statsProvider);
            var battleScene = battleSceneFactory.CreateBattleScene();
        }
    }
}
