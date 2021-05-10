using System.Collections.Generic;
using EditorMod;
using Modules.ActorModule;
using Modules.BattleModule.Levels.Providers;
using Modules.BattleModule.Managers;
using Modules.BattleModule.Stats;
using Modules.CameraModule;
using Modules.CameraModule.Components;
using Modules.GridModule;
using Modules.GunModule;
using Modules.PlayerModule.Actors;
using Modules.TickModule;
using UnityEngine;

namespace Modules.BattleModule.Factories
{
    public class BattleSceneFactory
    {
        public AvailableActorsProvider AvailableActorsProvider { get; }
        public AvailableBattleStatsProvider AvailableBattleStatsProvider { get; }
        
        private readonly ITickManager _tickManager;
        private readonly LevelDataProvider _levelDataProvider;
        private readonly IReadOnlyList<PlayerActorData> _playerActorsCollection;
        private readonly GameConstructions _gameConstructions;
        

        public BattleSceneFactory(ITickManager tickManager, LevelDataProvider levelDataProvider,
            AvailableBattleStatsProvider battleStatsProvider, PlayerActorsCollection playerActorsCollection,
            AvailableActorsProvider availableActorsProvider, GameConstructions gameConstructions)
        {
            _tickManager = tickManager;
            _levelDataProvider = levelDataProvider;
            AvailableBattleStatsProvider = battleStatsProvider;
            _playerActorsCollection = playerActorsCollection;
            AvailableActorsProvider = availableActorsProvider;
            _gameConstructions = gameConstructions;
        }

        public BattleScene CreateBattleScene(CameraController cameraController, WeaponConfig weaponConfig)
        {
            var gridBuilder = new GridBuilder("Battle_Grid");
            var gridController = gridBuilder.Build(_levelDataProvider.GridData);

            gridController.FillBuildingCell(_gameConstructions.ActualBuilding);

            var playerActorsManager =
                CreatePlayerManager(gridController, _tickManager, cameraController, weaponConfig);
            var enemyActorsManager = CreateEnemyManager(gridController,cameraController, weaponConfig);

            var battleScene = new BattleScene(gridController, playerActorsManager, enemyActorsManager,
                cameraController);
            
            return battleScene;
        }

        private BattleActManager CreateEnemyManager(GridController grid, CameraController cameraController,
            WeaponConfig weaponConfig)
        {
            var enemyActors = new List<BattleActor>();
            foreach (var levelActor in _levelDataProvider.EnemyActorsData)
            {
                var actor = levelActor.ActorData.Actor;
                var position = grid[levelActor.Cell].Component.transform.position;
                var actorPrefab = Object.Instantiate(actor, position, Quaternion.identity);

                var primaryStats = AvailableBattleStatsProvider.IdentifiedActorsStats[levelActor.ActorData.Id];
                var statUpgrades = new[] {0, 0, 0, 0, 0};

                var battleActor = new BattleActor(actorPrefab, primaryStats, statUpgrades,
                    AvailableBattleStatsProvider.SecondaryStatsDataProvider.SecondaryStats, true)
                {
                    Placement = grid[levelActor.Cell]
                };
                
                enemyActors.Add(battleActor);
                battleActor.SetWeapon(weaponConfig.LoadWeapon("1"));
            }

            var enemyManager = new EnemyActManager(grid, enemyActors, _tickManager, cameraController);
            return enemyManager;
        }

        private BattleActManager CreatePlayerManager(GridController grid, ITickManager tickManager,
            CameraController cameraController, WeaponConfig weaponConfig)
        {
            var playerBattleActors = new List<BattleActor>();
            for (var i = 0; i < _playerActorsCollection.Count; i++)
            {
                var actorData = _playerActorsCollection[i];
                var actorPlacement = _levelDataProvider.PlacementCells[i];
                var actor = AvailableActorsProvider.GetActorById(actorData.Id);
                actor.ID = actorData.Id;
                var position = grid[actorPlacement].Component.transform.position;

                var prefab = Object.Instantiate(actor, position, Quaternion.identity);

                var primaryStats = AvailableBattleStatsProvider.IdentifiedActorsStats[actorData.Id];
                var statUpgrades = actorData.Upgrades;

                var battleActor = new BattleActor(prefab, primaryStats, statUpgrades,
                    AvailableBattleStatsProvider.SecondaryStatsDataProvider.SecondaryStats, false)
                {
                    Placement = grid[actorPlacement]
                };

                playerBattleActors.Add(battleActor);
                battleActor.SetWeapon(weaponConfig.LoadWeapon("0"));
            }

            var playerManager = new PlayerActManager(grid, playerBattleActors, tickManager, cameraController);
            
            var cameraFollower = cameraController.GameCamera.GetActorComponent<SmoothFollowerComponent>();
            var actorToFollow = playerManager.Actors[0].Actor;
            var follower = new FlyFollower(actorToFollow.transform);
            cameraFollower.SetFollower(follower);

            return playerManager;
        }
    }
}