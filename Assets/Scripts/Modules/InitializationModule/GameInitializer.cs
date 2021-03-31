using System;
using Modules.ActorModule;
using Modules.BattleModule.Factories;
using Modules.BattleModule.Levels.Providers;
using Modules.BattleModule.Stats;
using Modules.GridModule;
using Modules.PlayerModule;
using Modules.PlayerModule.Actors;
using Modules.TickModule;
using UnityEngine;

namespace Modules.InitializationModule
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private LevelDataProvider _levelData;
        [SerializeField] private AvailableBattleStatsProvider _statsProvider;
        [SerializeField] private AvailableActorsProvider _actorsProvider;

        private GridController _grid;
        
        private void Start()
        {
            var player = GetPlayer();
            var tick = GetTickManager();
            
            var battleSceneFactory = new BattleSceneFactory(tick, _levelData, _statsProvider,
                player.ActorsCollection, _actorsProvider);
            
            var battleScene = battleSceneFactory.CreateBattleScene();
            battleScene.StartBattle();
        }

        private static Player GetPlayer()
        {
            var playerSwatGuyData = new PlayerActorData(0, 1, new [] {0, 0, 0, 0, 0});
            var playerActorsCollection = new PlayerActorsCollection();
            playerActorsCollection.AddActorData(playerSwatGuyData);
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
