using System.Collections.Generic;

namespace Modules.BattleModule.Managers
{
    public class EnemyBattleActorsManager : BattleActorManager
    {

        //Add AI brain for manager. Let brain decide how active actor should act.
        public EnemyBattleActorsManager(List<BattleActor> actors) : base(actors)
        {
            
        }
    }
}
