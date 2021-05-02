using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Modules.ActorModule.Components
{
    public class ActorNavigationComponent : BaseActorComponent<ActorNavigationComponent>
    {
        public EventHandler DestinationReach;
        public EventHandler WentToDestination;

        [SerializeField] private NavMeshAgent _navMeshAgent;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;

        public void SetNextDestination(Vector3 destination)
        {
            _navMeshAgent.destination = destination;
            _navMeshAgent.avoidancePriority = 99;
            StartCoroutine(WaitForDestinationReach());
        }

        private IEnumerator WaitForDestinationReach()
        {
            while (_navMeshAgent.remainingDistance >_navMeshAgent.stoppingDistance || _navMeshAgent.pathPending)
            {
                yield return null;
            }
        
            _navMeshAgent.ResetPath();
            _navMeshAgent.avoidancePriority = 50;
            DestinationReach?.Invoke(this,EventArgs.Empty);
        }
    }
}
