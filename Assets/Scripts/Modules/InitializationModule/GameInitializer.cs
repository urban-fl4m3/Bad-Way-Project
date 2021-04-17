using Modules.ActorModule;
using Modules.BattleModule.Factories;
using Modules.BattleModule.Levels.Providers;
using Modules.BattleModule.Stats;
using Modules.CameraModule;
using Modules.PlayerModule;
using Modules.PlayerModule.Actors;
using Modules.TickModule;
using UI;
using UnityEngine;

namespace Modules.InitializationModule
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private LevelDataProvider _levelData;
        [SerializeField] private AvailableBattleStatsProvider _statsProvider;
        [SerializeField] private AvailableActorsProvider _actorsProvider;
        [SerializeField] private BattlePlayerControlsView battlePlayerControlsView;
        [SerializeField] private CameraController _cameraController;

        private void Start()
        {
            var player = GetPlayer();
            var tick = GetTickManager();
            
            var battleSceneFactory = new BattleSceneFactory(tick, _levelData, _statsProvider,
                player.ActorsCollection, _actorsProvider);
            
            var battleScene = battleSceneFactory.CreateBattleScene(battlePlayerControlsView,_cameraController);
            
            
            battleScene.StartBattle(); 
        }

        private static Player GetPlayer()
        {
            var playerSwatGuyData = new PlayerActorData(0, 1, new [] {1, 1, 1, 1, 2});
            var playerSwatGuyData1 = new PlayerActorData(0, 3, new[] {7, 3, 4, 5, 5});

            var playerActorsCollection = new PlayerActorsCollection();
            playerActorsCollection.AddActorData(playerSwatGuyData);
            playerActorsCollection.AddActorData(playerSwatGuyData1);

            var player = new Player(playerActorsCollection);
            return player;
        }

        private static ITickManager GetTickManager()
        {
            var processor = new GameObject("_Tick_Processor").AddComponent<TickProcessor>();

            return new TickManager(processor);
        }
    }
}
