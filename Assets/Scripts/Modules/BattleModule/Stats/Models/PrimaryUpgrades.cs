using System;
using Modules.BattleModule.Stats.Helpers;
using UnityEngine;

namespace Modules.BattleModule.Stats.Models
{
    [Serializable]
    public struct PrimaryUpgrades
    {
        [SerializeField] public PrimaryStat Stat;
        [SerializeField] public float UpgradeValue;
    }
}