using System.Collections.Generic;
using System.Runtime.InteropServices;
using EditorMod;
using Modules.ActorModule;
using Modules.BattleModule.Levels.Providers;
using Modules.BattleModule.Managers;
using Modules.BattleModule.Stats;
using Modules.CameraModule;
using Modules.GridModule;
using Modules.PlayerModule.Actors;
using Modules.TickModule;
using UI;
using UI.Factories;
using UI.Interface;
using UI.Models;
using UnityEngine;

namespace Modules.BattleModule.Factories
{
    public class BattleSceneFactory
    {
        private readonly ITickManager _tickManager;
        private readonly LevelDataProvider _levelDataProvider;
        private readonly AvailableBattleStatsProvider _battleStatsProvider;
        private readonly IReadOnlyList<PlayerActorData> _playerActorsCollection;
        private readonly AvailableActorsProvider _availableActorsProvider;
        private readonly GameConstructions _gameConstructions;
        private readonly WindowFactory _windowFactory;
        private readonly Camera _camera;

        private List<BattleActor> _createdActor= new List<BattleActor>();
        
        public AvailableActorsProvider AvailableActorsProvider => _availableActorsProvider;
        public AvailableBattleStatsProvider AvailableBattleStatsProvider => _battleStatsProvider;

        //Add not single level provider, but for all levels and add implement method "CreateBattleSceneByIndex" 
        //or why else this class should exists?
        public BattleSceneFactory(ITickManager tickManager, LevelDataProvider levelDataProvider,
            AvailableBattleStatsProvider battleStatsProvider, PlayerActorsCollection playerActorsCollection,
            AvailableActorsProvider availableActorsProvider, GameConstructions gameConstructions,
            WindowFactory windowFactory, Camera camera)
        {
            _tickManager = tickManager;
            _levelDataProvider = levelDataProvider;
            _battleStatsProvider = battleStatsProvider;
            _playerActorsCollection = playerActorsCollection;
            _availableActorsProvider = availableActorsProvider;
            _gameConstructions = gameConstructions;
            _windowFactory = windowFactory;
            _camera = camera;
        }

        public BattleScene CreateBattleScene(CameraController cameraController)
        {
            var gridBuilder = new GridBuilder("Battle_Grid");
            var gridController = gridBuilder.Build(_levelDataProvider.GridData);

            gridController.FillBuildingCell(_gameConstructions.ActualBuilding);

            var playerActorsManager =
                CreatePlayerManager(gridController, _tickManager, _windowFactory, cameraController);
            var enemyActorsManager = CreateEnemyManager(gridController);

            var battleScene = new BattleScene(gridController, playerActorsManager, enemyActorsManager,
                cameraController, _windowFactory);


            var model = new BattleActorParameterModel(_createdActor, cameraController);
            _windowFactory.AddWindow("ActorStatus",model);
            
            return battleScene;
        }

        //Add factory for battle actors. Why? Incapsulate instantiate object for simplest actor creation while 
        //battle runs. For example: enemy reinforcement.
        private BattleActManager CreateEnemyManager(GridController grid)
        {
            var enemyActors = new List<BattleActor>();
            foreach (var levelActor in _levelDataProvider.EnemyActorsData)
            {
                Actor actorPrefab = levelActor.ActorData.Actor;
                Vector3 position = grid[levelActor.Cell].Component.transform.position;
                var actorPrefab1 = Object.Instantiate(actorPrefab, position, Quaternion.identity);

                var primaryStats = _battleStatsProvider.IdentifiedActorsStats[levelActor.ActorData.Id];
                var statUpgrades = new[] {0, 0, 0, 0, 0};

                var battleActor = new BattleActor(actorPrefab1, primaryStats, statUpgrades,
                    _battleStatsProvider.SecondaryStatsDataProvider.SecondaryStats)
                {
                    Placement = grid[levelActor.Cell]
                };
                
                enemyActors.Add(battleActor);
                _createdActor.Add(battleActor);
              
            }

            var model = new BattleEnemyStateModel(enemyActors);
            _windowFactory.AddWindow("EnemyView",model);
            
            var enemyManager = new EnemyActManager(grid, enemyActors, _tickManager);
            return enemyManager;
        }

        private BattleActManager CreatePlayerManager(GridController grid, ITickManager tickManager,
            WindowFactory windowFactory, CameraController cameraController)
        {
            var playerBattleActors = new List<BattleActor>();
            for (var i = 0; i < _playerActorsCollection.Count; i++)
            {
                var actorData = _playerActorsCollection[i];
                var actorPlacement = _levelDataProvider.PlacementCells[i];
                var actorPrefab = _availableActorsProvider.GetActorById(actorData.Id);
                var position = grid[actorPlacement].Component.transform.position;

                actorPrefab = Object.Instantiate(actorPrefab, position, Quaternion.identity);

                var primaryStats = _battleStatsProvider.IdentifiedActorsStats[actorData.Id];
                var statUpgrades = actorData.Upgrades;

                var battleActor = new BattleActor(actorPrefab, primaryStats, statUpgrades,
                    _battleStatsProvider.SecondaryStatsDataProvider.SecondaryStats)
                {
                    Placement = grid[actorPlacement]
                };

                playerBattleActors.Add(battleActor);
                _createdActor.Add(battleActor);
               // _battleActorParametersView.CreateActorParametersWindow(battleActor);
            }

            
          //  battlePlayerControlView.Initialize(AvailableActorsProvider.AvailableActors);
            
            var playerManager = new PlayerActManager(grid, playerBattleActors, tickManager, windowFactory, 
                cameraController,AvailableActorsProvider.AvailableActors);
            
            cameraController.PointAtActor(playerManager.Actors[0].Actor.transform);

            return playerManager;
        }

    }
}