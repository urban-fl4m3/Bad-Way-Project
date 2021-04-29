namespace Schemes
{
    public abstract class BaseScheme : IBaseScheme
    {
        public void Execute()
        {
            OnExecute();
        }

        public void Complete()
        {
            OnComplete();   
        }

        protected abstract void OnExecute();

        protected virtual void OnComplete()
        {
            
        }
    }
}