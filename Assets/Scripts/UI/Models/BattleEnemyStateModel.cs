using System;
using System.Collections.Generic;
using Modules.BattleModule;

namespace UI.Models
{
    public class BattleEnemyStateModel: IModel

    {
        public List<BattleActor> _enemies;
       

       public  BattleEnemyStateModel(List<BattleActor> enemies)
       {
           _enemies = enemies;

       }
    }
}