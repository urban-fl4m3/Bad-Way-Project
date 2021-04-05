using Modules.TickModule;
using UnityEngine;

namespace Modules.BattleModule.Managers
{
    public class MouseUpdate: ITickUpdate
    {
        private readonly int _i;
        public bool Enabled { get; set; }
        public MouseUpdate(int i)
        {
            _i = i;
        }
        public void Tick()
        {
            if (Input.GetMouseButtonDown(_i))
            {
                
            }
        }
    }
}