using System;
using Modules.BattleModule.Managers;
using Modules.GridModule;
using UnityEngine;

namespace Modules.BattleModule
{
    public class BattleScene
    {
        public readonly GridController Grid;
        public readonly BattleActManager PlayerActManager;
        public readonly BattleActManager EnemyActManager;
        public CameraController CameraController;
        
        public BattleScene(GridController grid, 
            BattleActManager playerActManager, BattleActManager enemyActManager, CameraController cameraController)
        {
            Grid = grid;
            PlayerActManager = playerActManager;
            PlayerActManager.SetScene(this);
            EnemyActManager = enemyActManager;
            CameraController = cameraController;
            EnemyActManager.SetScene(this);

            var rules = new DeathMatchRules(this);
            rules.RulesCompleted += RulesOnRulesCompleted;
        }

        private void RulesOnRulesCompleted(object sender, EventArgs e)
        {
            
        }

        public void StartBattle()
        {
            //Начало хода, ходят все юниты игрока последовательно
            PlayerActManager.ActStart();
            
            //Как только игрок подвигал всеми юнитами делаем это
           // PlayerActManager.ActEnd();
          //  EnemyActManager.ActStart();
            
            //Как только враг походил всеми юнитами делаем это и репит с 24 строчки
         //   EnemyActManager.ActEnd();
        }
    }

    public interface IRules
    {
        event EventHandler RulesCompleted;
    }

    public class DeathMatchRules : IRules
    {
        private readonly BattleScene _scene;

        public DeathMatchRules(BattleScene scene)
        {
            _scene = scene;
        //    scene.OnUnitKill += CheckForAliveUnits();
        }

        private void CheckForAliveUnits()
        {
            // if (_scene.PlayerUnits.Count < 1)
            // {
            //     RulesCompleted?.Invoke(this, Rules.PlayerLose);
            // }
            // else if (_scene.EnemyUnits.Count < 1)
            // {
            //     RulesCompleted?.Invoke(this, Rules.PlayerWin);
            // }
        }

        public event EventHandler RulesCompleted;
    }
}