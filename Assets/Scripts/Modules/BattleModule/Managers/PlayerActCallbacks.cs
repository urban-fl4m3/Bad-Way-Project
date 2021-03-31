﻿using Modules.GridModule;
using Modules.TickModule;

namespace Modules.BattleModule.Managers
{
    public class PlayerActCallbacks : IActCallbacks
    { 
        private readonly GridController _grid;
        private readonly ITickManager _tickManager;

        public PlayerActCallbacks(GridController grid, ITickManager tickManager)
        {
            _grid = grid;
            _tickManager = tickManager;
        }

        public void Act()
        {
            
        }
    }
}