using Common;
using UnityEngine;

namespace Modules.ActorModule.Components
{
    public class EnemyIdentifier : MonoBehaviour, IActorComponent

    {
        public void Initialize(TypeContainer container)
        {
            container.Add<ActorCollisionComponent>(this); 
        }
    }
}