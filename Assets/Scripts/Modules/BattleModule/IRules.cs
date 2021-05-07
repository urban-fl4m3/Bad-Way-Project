using System;

namespace Modules.BattleModule
{
    public interface IRules
    {
        event EventHandler<Rules> RulesComplete;
        void CheckRules();
    }
}