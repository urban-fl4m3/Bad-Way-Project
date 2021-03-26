using Modules.BattleModule.Stats;
using UnityEngine;

namespace Modules.BattleModule
{
    [CreateAssetMenu(fileName = "New Battle Actor Data Provider", menuName = "Actor/Battle Data Provider")]
    public class BattleActorDataProvider : ScriptableObject
    {
        [SerializeField] private int _id;

        [SerializeField] private PrimaryStatsProvider primaryStatsProvider;
    }
}