using Modules.BattleModule.Managers;

namespace Modules.BattleModule
{
    public class BattleScene
    {
        private readonly BattleActorManager _playerActorManager;
        private readonly BattleActorManager _enemyActorManager;

        public BattleScene(BattleActorManager playerActorManager, BattleActorManager enemyActorManager)
        {
            _playerActorManager = playerActorManager;
            _enemyActorManager = enemyActorManager;
        }
    }
}