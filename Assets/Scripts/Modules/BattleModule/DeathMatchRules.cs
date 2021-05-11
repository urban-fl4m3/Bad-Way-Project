using System;

namespace Modules.BattleModule
{
    public class DeathMatchRules: IRules
    {
        public event EventHandler<Rules> RulesComplete;
        
        private readonly BattleScene _battleScene;

        public DeathMatchRules(BattleScene battleScene)
        {
            _battleScene = battleScene;
        }

        public void CheckRules()
        {
            var playerActor = _battleScene.PlayerActManager.Actors;
            var enemyActor = _battleScene.EnemyActManager.Actors;

            if (playerActor.Count < 1)
            {
                RulesComplete?.Invoke(this, Rules.PlayerLose);
                return;
            }

            if (enemyActor.Count < 1)
            {
                RulesComplete?.Invoke(this, Rules.PlayerWin);
            }
        }
    }
}