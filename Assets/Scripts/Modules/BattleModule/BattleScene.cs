using Modules.BattleModule.Managers;
using Modules.GridModule;

namespace Modules.BattleModule
{
    public class BattleScene
    {
        private readonly GridController _grid;
        private readonly BattleActManager _playerActManager;
        private readonly BattleActManager _enemyActManager;

        public BattleScene(GridController grid, 
            BattleActManager playerActManager, BattleActManager enemyActManager)
        {
            _grid = grid;
            _playerActManager = playerActManager;
            _enemyActManager = enemyActManager;
        }

        public void StartBattle()
        {
            
        }
    }
}