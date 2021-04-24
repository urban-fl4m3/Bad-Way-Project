namespace Modules.BattleModule.Stats.EventArgs
{
    public class StatChangedEventArgs<TStat> : System.EventArgs
    {
        public readonly TStat Stat;
        public readonly int Amount;
        
        public StatChangedEventArgs(TStat stat, int amount)
        {
            Stat = stat;
            Amount = amount;
        }
    }
}