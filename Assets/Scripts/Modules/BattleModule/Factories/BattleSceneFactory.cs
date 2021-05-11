using System.Collections.Generic;
using System.Linq;
using EditorMod;
using Modules.ActorModule;
using Modules.BattleModule.Levels.Providers;
using Modules.BattleModule.Managers;
using Modules.BattleModule.Stats;
using Modules.CameraModule;
using Modules.GridModule;
using Modules.GunModule;
using Modules.PlayerModule.Actors;
using Modules.TickModule;
using Reset;
using UI.Factories;
using Unity.Mathematics;
using UnityEngine;

namespace Modules.BattleModule.Factories
{
    public class BattleSceneFactory
    {
        public AvailableActorsProvider AvailableActorsProvider { get; }
        public AvailableBattleStatsProvider AvailableBattleStatsProvider { get; }
        
        private readonly ITickManager _tickManager;
        private readonly LevelDataProvider _levelDataProvider;
        private readonly PlayerActorsCollection _playerActorsCollection;
        private readonly GameConstructions _gameConstructions;
        private readonly WindowFactory _windowFactory;


        public BattleSceneFactory(ITickManager tickManager, LevelDataProvider levelDataProvider,
            AvailableBattleStatsProvider battleStatsProvider, PlayerActorsCollection playerActorsCollection,
            AvailableActorsProvider availableActorsProvider, GameConstructions gameConstructions,WindowFactory windowFactory)
        {
            _tickManager = tickManager;
            _levelDataProvider = levelDataProvider;
            AvailableBattleStatsProvider = battleStatsProvider;
            _playerActorsCollection = playerActorsCollection;
            AvailableActorsProvider = availableActorsProvider;
            _gameConstructions = gameConstructions;
            _windowFactory = windowFactory;
        }

        public BattleScene CreateBattleScene(CameraController cameraController, WeaponConfig weaponConfig)
        {
            var gridBuilder = new GridBuilder("Battle_Grid");
            var gridController = gridBuilder.Build(_levelDataProvider.GridData);

            gridController.FillBuildingCell(_gameConstructions.ActualBuilding);

            var playerActorsManager = CreatePlayerManager(gridController, _tickManager, cameraController, weaponConfig);
            var enemyActorsManager = CreateEnemyManager(gridController,cameraController, weaponConfig);

            var reset = new BattleReset(_levelDataProvider, AvailableBattleStatsProvider, playerActorsManager,
                enemyActorsManager, gridController, _playerActorsCollection, _windowFactory);
            
            var battleScene = new BattleScene(gridController, playerActorsManager, enemyActorsManager, reset);
            
            
            return battleScene;
        }

        private BattleActManager CreateEnemyManager(GridController grid, CameraController cameraController,
            WeaponConfig weaponConfig)
        {
            var enemyActors = _levelDataProvider.EnemyActorsData.Select(levelActor =>
                CreateActor(levelActor.ActorData.Actor, levelActor.ActorData.Id, levelActor.Cell,
                    weaponConfig.LoadWeapon("1"), grid, true)).ToList();

            var enemyManager = new EnemyActManager(grid, enemyActors, _tickManager, cameraController);
            return enemyManager;
        }

        private BattleActManager CreatePlayerManager(GridController grid, ITickManager tickManager,
            CameraController cameraController, WeaponConfig weaponConfig)
        {
            var placementCells = new Stack<Vector2Int>(_levelDataProvider.PlacementCells);

            var playerBattleActors = _playerActorsCollection.Select((t, i) =>
                CreateActor(AvailableActorsProvider.AvailableActors[i].Actor, t.Id, placementCells.Pop(),
                    weaponConfig.LoadWeapon("0"), grid, false)).ToList();

            var playerManager = new PlayerActManager(grid, playerBattleActors, tickManager, cameraController);
            return playerManager;
        }

        private BattleActor CreateActor(Actor actor, int actorId, Vector2Int position, WeaponInfo weaponInfo,
            GridController grid, bool isEnemy)
        {
            var cellPosition = grid[position].Component.transform.position;
            
            var prefab = Object.Instantiate(actor, cellPosition, quaternion.identity);

            var primaryStats = AvailableBattleStatsProvider.IdentifiedActorsStats[actorId];
            var statUpgrades = new[] {0, 0, 0, 0, 0};
            
            var battleActor = new BattleActor(actorId, prefab, primaryStats, statUpgrades,
                AvailableBattleStatsProvider.SecondaryStatsDataProvider.SecondaryStats, isEnemy)
            {
                Placement = grid[position]
            };
            battleActor.SetWeapon(weaponInfo);
            return battleActor;
        }
    }
}