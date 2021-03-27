using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.PlayerModule.Actors
{
    [Serializable]
    public class PlayerActorData
    {
        [SerializeField] private int _id;
        [SerializeField] private int _level;
        [SerializeField] private int[] _upgrades;
        
        public int Id
        {
            get => _id;
            private set => _id = value;
        }

        public int Level
        {
            get => _level;
            private set => _level = value;
        }

        public IReadOnlyCollection<int> Upgrades => _upgrades;
        
        public PlayerActorData(int id, int level, int[] upgrades)
        {
            Id = id;
            Level = level;
            _upgrades = upgrades;
        }
    }
}