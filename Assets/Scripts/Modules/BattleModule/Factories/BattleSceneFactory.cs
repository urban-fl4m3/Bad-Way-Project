using System.Collections.Generic;
using Modules.ActorModule;
using Modules.BattleModule.Levels.Providers;
using Modules.BattleModule.Managers;
using Modules.BattleModule.Stats;
using Modules.CameraModule;
using Modules.GridModule;
using Modules.PlayerModule.Actors;
using Modules.TickModule;
using UI;
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

        public AvailableActorsProvider AvailableActorsProvider => _availableActorsProvider;

        public AvailableBattleStatsProvider AvailableBattleStatsProvider => _battleStatsProvider;

        //Add not single level provider, but for all levels and add implement method "CreateBattleSceneByIndex" 
        //or why else this class should exists?
        public BattleSceneFactory(ITickManager tickManager, LevelDataProvider levelDataProvider,
            AvailableBattleStatsProvider battleStatsProvider, PlayerActorsCollection playerActorsCollection,
            AvailableActorsProvider availableActorsProvider)
        {
            _tickManager = tickManager;
            _levelDataProvider = levelDataProvider;
            _battleStatsProvider = battleStatsProvider;
            _playerActorsCollection = playerActorsCollection;
            _availableActorsProvider = availableActorsProvider;
        }

        public BattleScene CreateBattleScene(BattlePlayerControlsView battlePlayerControlsView,
            CameraController cameraController)
        {
            var gridBuilder = new GridBuilder("Battle_Grid");
            var gridController = gridBuilder.Build(_levelDataProvider.GridData);

            var playerActorsManager =
                CreatePlayerManager(gridController, _tickManager, battlePlayerControlsView, cameraController);
            var enemyActorsManager = CreateEnemyManager(gridController,battlePlayerControlsView);

            var battleScene = new BattleScene(gridController, playerActorsManager, enemyActorsManager,
                cameraController);
            
            return battleScene;
        }

        //Add factory for battle actors. Why? Incapsulate instantiate object for simplest actor creation while 
        //battle runs. For example: enemy reinforcement.
        private BattleActManager CreateEnemyManager(GridController grid, BattlePlayerControlsView battlePlayerControlsView)
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
            }
            battlePlayerControlsView.SubscribeEnemy(enemyActors);
            var enemyManager = new EnemyActManager(grid, enemyActors, _tickManager);
            return enemyManager;
        }

        private BattleActManager CreatePlayerManager(GridController grid, ITickManager tickManager,
            BattlePlayerControlsView battlePlayerControlsView, CameraController cameraController)
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
            }

            
            battlePlayerControlsView.Initialize(AvailableActorsProvider.AvailableActors, playerBattleActors);
            
            var playerManager = new PlayerActManager(grid, playerBattleActors, tickManager, battlePlayerControlsView, 
                cameraController);
            cameraController.PointAtActor(playerManager.Actors[0].Actor.transform);

            return playerManager;
        }
    }
}