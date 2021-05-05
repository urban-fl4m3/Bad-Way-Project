using EditorMod;
using Modules.ActorModule;
using Modules.ActorModule.Concrete;
using Modules.BattleModule.Factories;
using Modules.BattleModule.Levels.Providers;
using Modules.BattleModule.Stats;
using Modules.CameraModule;
using Modules.GunModule;
using Modules.PlayerModule;
using Modules.PlayerModule.Actors;
using Modules.TickModule;
using Schemes;
using Schemes.Implementations;
using UI.Configs;
using UI.Factories;
using UnityEngine;

namespace Modules.InitializationModule
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private LevelDataProvider _levelData;
        [SerializeField] private AvailableBattleStatsProvider _statsProvider;
        [SerializeField] private AvailableActorsProvider _actorsProvider;
        [SerializeField] private GameConstructions _gameConstructions;
        [SerializeField] private WindowViewsConfig _viewsConfig;
        [SerializeField] private WeaponConfig _weaponConfig;
        
        [SerializeField] private CameraActor _gameCamera;
        [SerializeField] private CameraActor _uiCamera;
        
        private CameraController _cameraController;
        private IBaseScheme _battleScheme;
        private WindowFactory _windowsFactory;
        
        private void Start()
        {
            _gameConstructions.BuildingInGrid(_levelData);
            
            var player = GetPlayer();
            var tick = GetTickManager();

            _cameraController = new CameraController(tick, _gameCamera, _uiCamera);
            _cameraController.StartBattle();
            
            _windowsFactory = new WindowFactory(_cameraController, _viewsConfig);
            
            var battleSceneFactory = new BattleSceneFactory(tick, _levelData, _statsProvider,
                player.ActorsCollection, _actorsProvider, _gameConstructions);

            var battleScene = battleSceneFactory.CreateBattleScene(_cameraController, _weaponConfig);

            _battleScheme = new BattleScheme(_windowsFactory, battleScene, battleSceneFactory, _cameraController);
            _battleScheme.Execute();
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

        private void OnDestroy()
        {
            _battleScheme.Complete();
        }
    }
}
