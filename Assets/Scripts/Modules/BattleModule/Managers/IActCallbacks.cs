using System.Diagnostics;
using UnityEngine;

namespace Modules.BattleModule.Managers
{
    public interface IActCallbacks
    {
        void ActStart();
        void ActEnd();
    }

}