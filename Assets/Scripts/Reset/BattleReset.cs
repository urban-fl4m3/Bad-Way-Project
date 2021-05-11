using System.Collections.Generic;
using Modules.ActorModule;
using Modules.BattleModule;
using Modules.BattleModule.Levels.Providers;
using Modules.BattleModule.Managers;
using Modules.BattleModule.Stats;
using Modules.GridModule;
using Modules.PlayerModule.Actors;
using UI.Factories;
using UnityEngine;

namespace Reset
{
    public class BattleReset : IReset
    {
        private readonly LevelDataProvider _levelData;
        private readonly AvailableBattleStatsProvider _statsProvider;
        private readonly BattleActManager _playerActManager;
        private readonly BattleActManager _enemyActManager;
        private readonly PlayerActorsCollection _playerActorsCollection;
        private readonly WindowFactory _windowFactory;
        private readonly GridController _grid;

        public BattleReset(LevelDataProvider levelData, AvailableBattleStatsProvider statsProvider, 
            BattleActManager playerActManager, BattleActManager enemyActManager, GridController grid,
            PlayerActorsCollection playerActorsCollection, WindowFactory windowFactory)
        {
            _levelData = levelData;
            _statsProvider = statsProvider;
            _playerActManager = playerActManager;
            _enemyActManager = enemyActManager;
            _grid = grid;
            _playerActorsCollection = playerActorsCollection;
            _windowFactory = windowFactory;
        }

        public void Load()
        {
            _windowFactory.Reset();
            EnemyReset();
            PlayerReset();
            _playerActManager.ActStart();
        }

        private void EnemyReset()
        {
            var deadEnemies = _enemyActManager.DeadActors;
            var aliveEnemies = _enemyActManager.Actors;
            var allEnemies = new List<BattleActor>();
            
            allEnemies.AddRange(deadEnemies);
            allEnemies.AddRange(aliveEnemies);

            var aliveEnemiesStack = new Stack<BattleActor>(allEnemies);

            foreach (var enemyLevelActor in _levelData.EnemyActorsData)
            {
                var enemy = aliveEnemiesStack.Pop();
                var primaryStats = _statsProvider.IdentifiedActorsStats[enemyLevelActor.ActorData.Id];
                var statUpgrades = new[] {0, 0, 0, 0, 0};
                var secondaryStat = _statsProvider.SecondaryStatsDataProvider.SecondaryStats;
                
                enemy.Reset(primaryStats,statUpgrades,secondaryStat,true);
                enemy.Actor.Reset();
                var position = enemyLevelActor.Cell;
                enemy.Placement = _grid[position.x,position.y];
                enemy.Actor.Transform.position = enemy.Placement.CellComponent.transform.position;
            }
            
            _enemyActManager.Reset();
        }

        private void PlayerReset()
        {
            var deadActors = _playerActManager.DeadActors;
            var aliveActors = _playerActManager.Actors;
            var allActors = new List<BattleActor>();
            
            var placementCells = new Stack<Vector2Int>(_levelData.PlacementCells);

            allActors.AddRange(deadActors);
            allActors.AddRange(aliveActors);

            foreach (var playerActor in _playerActorsCollection)
            {
                foreach (var battleActor in allActors)
                {
                    if (battleActor.Id == playerActor.Id)
                    {
                        var primaryStats = _statsProvider.IdentifiedActorsStats[playerActor.Id];
                        var statUpgrades = playerActor.Upgrades;
                        var secondaryStat = _statsProvider.SecondaryStatsDataProvider.SecondaryStats;
                        battleActor.Reset(primaryStats, statUpgrades, secondaryStat, false);
                        battleActor.Actor.Reset();

                        var position = placementCells.Pop();
                        battleActor.Placement = _grid[position.x, position.y];
                        battleActor.Actor.Transform.position =
                            _grid[position.x, position.y].Component.transform.position;
                    }
                }
            }
            
            _playerActManager.Reset();
        }
    }
}