using System;
using System.Collections;
using Common;
using Modules.ActorModule.Components;
using Modules.GridModule.Cells;
using UnityEngine;
using UnityEngine.AI;

public class ActorNavigation : MonoBehaviour,IActorComponent
{
    public EventHandler DestinationReach;
    public EventHandler WentToDistination;

    [SerializeField] private NavMeshAgent _navMeshAgent;

    private Cell _nowCell;

    public void Initialize(TypeContainer container)
    {
        container.Add<ActorNavigation>(this);
    }
    
    public void SetNextCell(Cell cell)
    {
        _nowCell = cell;
        _navMeshAgent.destination = cell.CellComponent.transform.position;
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
