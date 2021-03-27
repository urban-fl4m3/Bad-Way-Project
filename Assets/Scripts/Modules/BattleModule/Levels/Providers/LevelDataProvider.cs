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

        [SerializeField] private List<Vector2Int> _placementCells;
        [SerializeField] private List<EnemyLevelActor> _enemyActorsData;
        
        public GridDataModel GridData => _levelGridData;
        public IEnumerable<EnemyLevelActor> EnemyActorsData => _enemyActorsData;
        public List<Vector2Int> PlacementCells => _placementCells;
    }
}