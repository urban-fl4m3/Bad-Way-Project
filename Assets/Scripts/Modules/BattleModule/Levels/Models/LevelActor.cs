using System;
using UnityEngine;

namespace Modules.BattleModule.Levels.Models
{
    [Serializable]
    public struct LevelActor
    {
        public int Id;
        public Vector2Int Cell;
    }
}