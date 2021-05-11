using Modules.BattleModule;
using Reset;

namespace UI.Models
{
    public class BattleResetModel: IModel
    {
        public readonly IReset BattleReset;
        public readonly IRules DeathMatchRules;
        
        public BattleResetModel(IReset battleReset, IRules deathMatchRules)
        {
            BattleReset = battleReset;
            DeathMatchRules = deathMatchRules;
        }
    }
}