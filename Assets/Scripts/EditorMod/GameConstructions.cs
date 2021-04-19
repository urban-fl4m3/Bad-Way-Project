using System;
using System.Collections;
using System.Collections.Generic;
using Modules.BattleModule.Levels.Providers;
using UnityEngine;

public class GameConstructions: MonoBehaviour
{
    public List<Transform> _objectOnGrid = new List<Transform>();
    private List<Transform> _actualBuilding;
    
    public List<Transform> ActualBuilding => _actualBuilding;

    private void Awake()
    {
        var centringObj = GameObject.FindObjectsOfType<CenteringObjectInEditScene>();

        foreach (var e in centringObj)
        {
            _objectOnGrid.Add(e.transform);
        }
    }

    public void BuildingInGrid(LevelDataProvider levelDataProvider)
    {
        _actualBuilding = new List<Transform>();
        
        var column = levelDataProvider.GridData.Columns;
        var row = levelDataProvider.GridData.Rows;
        
        var limX= new Vector2Int(0,column*2);
        var limY = new Vector2Int(0, row * 2);
        
        foreach (var building in _objectOnGrid)
        {
            var position = building.position;
            if (position.x >= limX.x && position.x <= limX.y && position.y >= limY.x && position.y <= limY.y)
            {
                _actualBuilding.Add(building);
            }
        }
        
    }
    
}
