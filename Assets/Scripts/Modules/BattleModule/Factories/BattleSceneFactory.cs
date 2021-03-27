﻿using System.Collections.Generic;
using System.Linq;
using Modules.ActorModule;
using Modules.BattleModule.Levels.Providers;
using Modules.BattleModule.Managers;
using Modules.BattleModule.Stats;
using Modules.GridModule;
using Modules.PlayerModule.Actors;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace Modules.BattleModule.Factories
{
    public class BattleSceneFactory
    {
        private readonly LevelDataProvider _levelDataProvider;
        private readonly AvailableBattleStatsProvider _battleStatsProvider;
        private readonly IReadOnlyList<PlayerActorData> _playerActorsCollection;
        private readonly AvailableActorsProvider _availableActorsProvider;

        //Add not single level provider, but for all levels and add implement method "CreateBattleSceneByIndex" 
        //or why else this class should exists?
        public BattleSceneFactory(LevelDataProvider levelDataProvider, AvailableBattleStatsProvider battleStatsProvider,
            PlayerActorsCollection playerActorsCollection, AvailableActorsProvider availableActorsProvider)
        {   
            _levelDataProvider = levelDataProvider;
            _battleStatsProvider = battleStatsProvider;
            _playerActorsCollection = playerActorsCollection;
            _availableActorsProvider = availableActorsProvider;
        }

        public BattleScene CreateBattleScene()
        {
            var gridBuilder = new GridBuilder("Battle_Grid");
            var gridController = gridBuilder.Build(_levelDataProvider.GridData);

            var playerActorsManager = CreatePlayerManager(gridController);
            var enemyActorsManager = CreateEnemyManager(gridController);

            var battleScene = new BattleScene(playerActorsManager, enemyActorsManager);

            return battleScene;
        }

        //Add factory for battle actors. Why? Incapsulate instantiate object for simplest actor creation while 
        //battle runs. For example: enemy reinforcement.
        private BattleActorManager CreateEnemyManager(GridController grid)
        {
            var enemyActors = (
                from levelActor in _levelDataProvider.EnemyActorsData 
                let actorPrefab = levelActor.ActorData.Actor 
                let position = grid[levelActor.Cell].Component.transform.position 
                select Object.Instantiate(actorPrefab, position, Quaternion.identity)
                into actorPrefab 
                select new BattleActor(actorPrefab)).ToList();

            var enemyManager = new EnemyBattleActorsManager(enemyActors);
            return enemyManager;
        }

        private BattleActorManager CreatePlayerManager(GridController grid)
        {
            var playerBattleActors = new List<BattleActor>();
            for (var i = 0; i < _playerActorsCollection.Count; i++)
            {
                var actorData = _playerActorsCollection[i];
                var actorPlacement = _levelDataProvider.PlacementCells[i];
                var actorPrefab = _availableActorsProvider.GetActorById(actorData.Id);
                var position = grid[actorPlacement].Component.transform.position;

                actorPrefab = Object.Instantiate(actorPrefab, position, Quaternion.identity);
                var battleActor = new BattleActor(actorPrefab);
                playerBattleActors.Add(battleActor);
            }

            var playerManager = new PlayerBattleActorsManager(playerBattleActors);
            return playerManager;
        }
    }
}