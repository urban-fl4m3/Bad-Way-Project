using System.Collections.Generic;
using Modules.BattleModule.Levels.Models;
using Modules.GridModule.Models;
using UnityEngine;

namespace Modules.BattleModule.Levels.Providers
{
    [CreateAssetMenu(fileName = "New Level Data", menuName = "Levels/Data Provider")]
    public class LevelDataProvider : ScriptableObject
    {
        [SerializeField] private GridDataModel _levelGridData;
        [SerializeField] private List<LevelActor> _actors;
        
        public GridDataModel GridData => _levelGridData;
        public IEnumerable<LevelActor> Actors => _actors;
    }
}