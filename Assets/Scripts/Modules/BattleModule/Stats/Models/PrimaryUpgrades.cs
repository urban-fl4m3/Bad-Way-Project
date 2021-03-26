using System;
using UnityEngine;

namespace Modules.BattleModule.Stats.Models
{
    [Serializable]
    public struct PrimaryUpgrades
    {
        [SerializeField] public MainStat Stat;
        [SerializeField] public float UpgradeValue;
    }
}