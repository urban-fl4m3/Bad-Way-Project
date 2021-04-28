using UI.Components;
using UnityEngine;

namespace UI
{
    public class BattleEnemyStateView : MonoBehaviour
    {
        [SerializeField] private EnemyWindowView enemyWindowView;
        public EnemyWindowView EnemyWindowView => enemyWindowView;
        
        public void SubscribeEnemy(IEnumerable<BattleActor> enemyActor)
        {
            _enemyActors = new List<BattleActor>();
            foreach (var actor in enemyActor)
            {
                _enemyActors.Add(actor);
                actor.Actor.ActorSelect += OnEnemyActorClick;
                actor.Actor.ActorUnSelect += OnEnemyUnSelect;
                actor.ActorDeath += OnEnemyDeath;
            }
        }

        private void OnEnemyDeath(object sender, BattleActor e)
        {
            e.Actor.ActorSelect -= OnEnemyActorClick;
            e.Actor.ActorUnSelect -= OnEnemyUnSelect;
            e.ActorDeath -= OnEnemyDeath;
        }
        
        private void OnEnemyActorClick(object sender, Actor e)
        {
            foreach (var enemyActor in _enemyActors.Where(enemyActor => enemyActor.Actor == e))
            {
                // _enemyWindow.EnemyWindowView.gameObject.SetActive(true);
                // _enemyWindow.EnemyWindowView.SetHealth(enemyActor.Stats[SecondaryStat.Health],
                //     enemyActor.Stats.MaxHealth, 
                //     enemyActor);
            }
        }

        private void OnEnemyUnSelect(object sender, EventArgs e)
        {
            // _enemyWindow.EnemyWindowView.gameObject.SetActive(false);
        }
    }
}
