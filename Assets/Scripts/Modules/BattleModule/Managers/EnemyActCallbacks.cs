using Modules.GridModule;

namespace Modules.BattleModule.Managers
{
    public class EnemyActCallbacks : IActCallbacks
    {
        private readonly GridController _grid;

        public EnemyActCallbacks(GridController grid)
        {
            _grid = grid;
        }
        
        public void Act()
        {
            
        }
    }
}