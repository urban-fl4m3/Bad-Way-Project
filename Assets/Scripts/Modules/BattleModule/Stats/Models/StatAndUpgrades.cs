using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.BattleModule.Stats.Models
{
    [Serializable]
    public struct StatAndUpgrades
    {
        [SerializeField] public int Value;
        [SerializeField] public List<PrimaryUpgrades> UpgradeList;
    }
}