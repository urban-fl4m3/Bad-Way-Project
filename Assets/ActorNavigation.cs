using System;
using System.Collections;
using Common;
using Modules.ActorModule.Components;
using UnityEngine;
using UnityEngine.AI;

public class ActorNavigation : MonoBehaviour,IActorComponent
{
    public EventHandler DestinationReach;
    public EventHandler WentToDestination;

    [SerializeField] private NavMeshAgent _navMeshAgent;

    public void Initialize(TypeContainer container)
    {
        container.Add<ActorNavigation>(this);
    }
    
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
