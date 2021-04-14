using System.Collections.Generic;
using Modules.GridModule;
using Modules.TickModule;

namespace Modules.BattleModule.Managers
{
    public class EnemyActManager : BattleActManager
    {
        public EnemyActManager(GridController grid, List<BattleActor> actors, ITickManager tickManager) 
            : base(grid, actors, tickManager)
        {
        }

        protected override void OnActStart()
        {
            
        }

        protected override void OnActEnd()
        {
            
        }
    }
}