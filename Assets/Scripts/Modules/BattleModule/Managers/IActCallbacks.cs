namespace Modules.BattleModule.Managers
{
    public interface IActCallbacks
    {
        void ActStart();
        void ActEnd();

        void SetScene(BattleScene scene);
    }
}