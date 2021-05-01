using Common;
using Modules.BattleModule.Managers;
using UnityEngine;

namespace Modules.ActorModule.Components
{
    public class EnemyAI : MonoBehaviour, IActorComponent
    {
        public void Initialize(TypeContainer container)
        {
            container.Add<EnemyAI>(this); 
        }
    }
}