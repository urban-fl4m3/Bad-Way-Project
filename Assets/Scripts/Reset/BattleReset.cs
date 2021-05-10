using System.Collections.Generic;
using Modules.ActorModule;
using Modules.BattleModule;
using Modules.BattleModule.Levels.Providers;
using Modules.BattleModule.Stats;
using Modules.GridModule;
using Modules.PlayerModule.Actors;
using UnityEngine;

namespace Reset
{
    public class BattleReset: BaseReset
    {
        
        private readonly LevelDataProvider _levelData;
        private readonly AvailableBattleStatsProvider _statsProvider;
        private readonly AvailableActorsProvider _actorsProvider;
        private readonly PlayerActorsCollection _playerActorsCollection;
        private readonly BattleScene _battleScene;
        private readonly GridController _grid;

        public BattleReset(LevelDataProvider levelData, AvailableBattleStatsProvider statsProvider,
            AvailableActorsProvider actorsProvider, BattleScene battleScene, GridController grid,
            PlayerActorsCollection playerActorsCollection)
        {
            _levelData = levelData;
            _statsProvider = statsProvider;
            _actorsProvider = actorsProvider;
            _battleScene = battleScene;
            _grid = grid;
            _playerActorsCollection = playerActorsCollection;
        }

        protected override void OnLoad()
        {
            EnemyReset();
            PlayerReset();
        }
        
        void EnemyReset()
        {
            var deadEnemies = _battleScene.EnemyActManager.DeadActors;
            var aliveEnemies = _battleScene.EnemyActManager.Actors;
            var allEnemies = new List<BattleActor>();
            
            allEnemies.AddRange(deadEnemies);
            allEnemies.AddRange(aliveEnemies);

            var aliveEnemiesStack = new Stack<BattleActor>(allEnemies);
            Debug.Log(aliveEnemiesStack.Count);

            foreach (var enemyLevelActor in _levelData.EnemyActorsData)
            {
                var enemy = aliveEnemiesStack.Pop();
                var primaryStats = _statsProvider.IdentifiedActorsStats[enemyLevelActor.ActorData.Id];
                var statUpgrades = new[] {0, 0, 0, 0, 0};
                var secondaryStat = _statsProvider.SecondaryStatsDataProvider.SecondaryStats;
                
                enemy.Reset(primaryStats,statUpgrades,secondaryStat,true);
                
                var position = enemyLevelActor.Cell;
                enemy.Placement = _grid[position.x,position.y];
                enemy.Actor.Transform.position = enemy.Placement.CellComponent.transform.position;
            }
        }
        private void PlayerReset()
        {
            var deadActors = _battleScene.PlayerActManager.DeadActors;
            var aliveActors = _battleScene.PlayerActManager.Actors;
            var allActors = new List<BattleActor>();
            
            allActors.AddRange(deadActors);
            allActors.AddRange(aliveActors);

            foreach (var playerActor in _playerActorsCollection)
            {
                foreach (var battleActor in allActors)
                {
                    if (battleActor.Actor.ID == playerActor.Id)
                    {
                        var primaryStats = _statsProvider.IdentifiedActorsStats[playerActor.Id];
                        var statUpgrades = playerActor.Upgrades;
                        var secondaryStat = _statsProvider.SecondaryStatsDataProvider.SecondaryStats;
                        battleActor.Reset( primaryStats,statUpgrades,secondaryStat,false);

                        var position = _levelData.PlacementCells[playerActor.Id];
                        Debug.Log(position);
                        battleActor.Placement = _grid[position.x,position.y];
                        battleActor.Actor.Transform.position = _grid[position.x,position.y].Component.transform.position;
                    }
                }
            }
        }
    }
}