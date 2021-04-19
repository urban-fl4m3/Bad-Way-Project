using System.Collections.Generic;
using Modules.BattleModule.Levels.Providers;
using UnityEngine;

namespace EditorMod
{
    public class GameConstructions: MonoBehaviour
    {
        public List<Transform> ActualBuilding { get; private set; }

        private readonly List<Transform> _objectsOnGrid = new List<Transform>();
        
        private void Awake()
        {
            var centringObj = FindObjectsOfType<CenteringObjectInEditScene>();

            foreach (var e in centringObj)
            {
                _objectsOnGrid.Add(e.transform);
            }
        }

        public void BuildingInGrid(LevelDataProvider levelDataProvider)
        {
            ActualBuilding = new List<Transform>();
        
            var column = levelDataProvider.GridData.Columns;
            var row = levelDataProvider.GridData.Rows;
        
            var limX= new Vector2Int(0,column*2);
            var limY = new Vector2Int(0, row * 2);
        
            foreach (var building in _objectsOnGrid)
            {
                var position = building.position;
                if (position.x >= limX.x && position.x <= limX.y && position.y >= limY.x && position.y <= limY.y)
                {
                    ActualBuilding.Add(building);
                }
            }
        
        }
    
    }
}
