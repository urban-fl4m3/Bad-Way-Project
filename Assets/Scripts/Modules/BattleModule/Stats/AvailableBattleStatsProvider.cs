using System.Collections.Generic;
using System.Linq;
using Modules.BattleModule.Stats.Helpers;
using UnityEngine;

namespace Modules.BattleModule.Stats
{
    [CreateAssetMenu(fileName = "New Available Battle Stats Provider", menuName = "Stats/Available Battle Stats Provider")]
    public class AvailableBattleStatsProvider : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private ActorPrimaryStatsDataProvider[] _actorsPrimaryStats;
        [SerializeField] private SecondaryStatsDataProvider _secondaryStatsDataProvider;

        public Dictionary<int, IReadOnlyDictionary<PrimaryStat, int>> IdentifiedActorsStats { get; private set; }

        public SecondaryStatsDataProvider SecondaryStatsDataProvider => _secondaryStatsDataProvider;

        public void OnBeforeSerialize()
        {
            
        }

        public void OnAfterDeserialize()
        {   
            IdentifiedActorsStats = _actorsPrimaryStats
                .ToDictionary(x => x.ID, x => x.PrimaryStats);
        }
    }
}