using System;
using Modules.GridModule;
using Modules.TickModule;
using UnityEngine;

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

        public void ActStart()
        {
            _grid.CellSelected += HandleUnitSelection;

            var mouseUpdate = new MouseUpdate(0);
            _tickManager.AddTick(this, mouseUpdate);
        }

        public void ActEnd()
        {
            
        }

        private void HandleUnitSelection(object sender, EventArgs e)
        {
            Debug.Log("Wow");
            
        }
    }
}