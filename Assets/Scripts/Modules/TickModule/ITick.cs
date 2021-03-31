namespace Modules.TickModule
{
    public interface ITick
    {
        bool Enabled { get; set; }
        void Tick();
    }
    
    public interface ITickFixedUpdate : ITick
    {
        
    }
    
    public interface ITickLateUpdate : ITick
    {
        
    }
    
    public interface ITickUpdate : ITick
    {
        
    }
}