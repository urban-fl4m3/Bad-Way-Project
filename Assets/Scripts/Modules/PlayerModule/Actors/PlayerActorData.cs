using System.Collections.Generic;

namespace Modules.PlayerModule.Actors
{
    public class PlayerActorData
    {
        public readonly int Id;
        
        public int Level { get; private set; }

        public IReadOnlyCollection<int> Upgrades => _upgrades;
        private readonly int[] _upgrades;

        public PlayerActorData(int id, int level, int[] upgrades)
        {
            Id = id;
            Level = level;
            _upgrades = upgrades;
        }
    }
}