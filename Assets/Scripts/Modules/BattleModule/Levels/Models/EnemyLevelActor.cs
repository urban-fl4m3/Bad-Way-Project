using System;
using Modules.ActorModule;
using UnityEngine;

namespace Modules.BattleModule.Levels.Models
{
    [Serializable]
    public struct EnemyLevelActor
    {
        public int Level;
        public ActorDataProvider ActorData;
        public Vector2Int Cell;
    }
}