using System.Collections;
using System.Collections.Generic;
using UI.Components;
using UnityEngine;
using  UnityEngine.UI;
public class BattleEnemyScene : MonoBehaviour
{
    [SerializeField] private EnemyWindow _enemyWindow;
    public EnemyWindow EnemyWindow => _enemyWindow;
}
