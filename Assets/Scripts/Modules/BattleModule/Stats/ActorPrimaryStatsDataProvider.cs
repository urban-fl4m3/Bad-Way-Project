using System.Collections.Generic;
using Modules.BattleModule.Stats.Helpers;
using Modules.BattleModule.Stats.Providers;
using UnityEngine;

namespace Modules.BattleModule.Stats
{
    [CreateAssetMenu(fileName = "New Battle Actor Data Provider", menuName = "Stats/Actor Battle Primary Stats")]
    public class ActorPrimaryStatsDataProvider : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private int _id;
        [SerializeField] private PrimaryStatsProvider _primaryStatsProvider;

        private readonly Dictionary<PrimaryStat, int> _primaryStats = new Dictionary<PrimaryStat, int>();
        
        public int ID => _id;
        public IReadOnlyDictionary<PrimaryStat, int> PrimaryStats => _primaryStats;

        public void OnBeforeSerialize()
        {
            
        }

        public void OnAfterDeserialize()
        {
            _primaryStats.Clear();
            
            var values = _primaryStatsProvider.PrimaryStats;
            
            for (var i = 0; i < values.Length; i++)
            {
                var stat = (PrimaryStat)i;
                var value = values[i];
                _primaryStats.Add(stat, value);
            }
        }
    }
}