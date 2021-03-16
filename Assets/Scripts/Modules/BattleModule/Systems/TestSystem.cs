using Modules.EcsModule.Systems;
using UnityEngine;

namespace Modules.BattleModule.Systems
{
    public class TestSystem : CustomComponentSystem
    {
        protected override void HandleUpdate()
        {
            base.HandleUpdate();
            
            Debug.Log("Lol");
        }
    }
}