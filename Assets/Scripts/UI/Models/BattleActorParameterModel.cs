using System;
using System.Collections.Generic;
using Common;
using Modules.ActorModule.Concrete;
using Modules.BattleModule;
using Modules.BattleModule.Managers;
using Modules.CameraModule;
using Modules.GridModule;
using Modules.GridModule.Cells;
using UnityEngine;

namespace UI.Models
{
    public class BattleActorParameterModel: IModel
    {
        public readonly List<BattleActor> BattleActors;
        public readonly CameraController CameraController;
        public EventHandler<Cell> CellSelected;
        public EventHandler CellDeselected;
        public DynamicValue<BattleActor> ActorAttack;

        public BattleActorParameterModel(List<BattleActor> battleActors, CameraController cameraController,
            GridController grid, PlayerActManager _playerActManager)
        {
            BattleActors = battleActors;
            CameraController = cameraController;
            grid.CellSelected += OnCellSelected;
            grid.CellDeselected += OnCellDeselected;
            ActorAttack = _playerActManager.ActorAttack;
        }

        private void OnCellSelected(object sender, Cell e)
        {
            CellSelected?.Invoke(this, e);
        }

        private void OnCellDeselected(object sender, EventArgs e)
        {
            CellDeselected?.Invoke(this, e);
        }
    }
}