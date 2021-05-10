using Modules.BattleModule;
using Reset;

namespace UI.Models
{
    public class BattleResetModel: IModel
    {
        public IReset BattleReset;
        public IRules DeathMatchRules;
        
        public BattleResetModel(IReset battleReset, IRules deathMatchRules)
        {
            BattleReset = battleReset;
            DeathMatchRules = deathMatchRules;
        }
    }
}