using UI.Components;
using UnityEngine;

namespace UI
{
    public class EnemyStateView : MonoBehaviour
    {
        [SerializeField] private EnemyWindowView enemyWindowView;
        public EnemyWindowView EnemyWindowView => enemyWindowView;
    }
}
