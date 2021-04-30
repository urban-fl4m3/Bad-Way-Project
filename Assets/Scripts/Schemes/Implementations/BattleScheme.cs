using System.Collections.Generic;
using Modules.BattleModule;
using Modules.BattleModule.Factories;
using Modules.BattleModule.Managers;
using Modules.CameraModule;
using UI.Factories;
using UI.Models;
using UnityEngine;

namespace Schemes.Implementations
{
    public class BattleScheme : BaseScheme
    {
        private readonly BattleScene _battleScene;
        private readonly WindowFactory _windowFactory;
        private readonly BattleSceneFactory _battleSceneFactory;
        private readonly CameraController _camera;
        
        
        public BattleScheme(WindowFactory windowFactory, BattleScene battleScene, BattleSceneFactory battleSceneFactory,
            Component camera)
        {
            _battleScene = battleScene;
            _battleSceneFactory = battleSceneFactory;
            _windowFactory = windowFactory;
            _battleSceneFactory = battleSceneFactory;
            _camera = camera.GetComponent<CameraController>();
        }
        
        protected override void OnExecute()
        {
            var playerActManager = _battleScene.PlayerActManager;
            var enemyActManager = _battleScene.EnemyActManager;
            var playerManager = playerActManager as PlayerActManager;

            var allActor = new List<BattleActor>();
            allActor.AddRange(playerActManager.Actors);
            allActor.AddRange(enemyActManager.Actors);
            
            var battleActorParameterModel = new BattleActorParameterModel(allActor, _camera);
            var battleEnemyStateModel = new BattleEnemyStateModel(enemyActManager.Actors as List<BattleActor>);
            var battlePlayerControlViewModel = new BattlePlayerControlViewModel(
                playerManager.HandleMovementClicked,
                playerManager.HandleAttackClicked,
                playerManager.HandleSelectActor,
                _battleSceneFactory.AvailableActorsProvider.AvailableActors,
                playerManager.PlayerDoingAct);
            
           _windowFactory.AddWindow("ActorStatus", battleActorParameterModel);
           _windowFactory.AddWindow("EnemyStatus", battleEnemyStateModel);
           _windowFactory.AddWindow("PlayerView", battlePlayerControlViewModel);
            
        }

        protected override void OnComplete()
        {
            base.OnComplete();
            
        }
    }
}