using UnityEngine.Events;

namespace Reset
{
    public abstract class BaseReset: IReset
    {
        public void Load()
        {
            OnLoad();
        }
        protected abstract void OnLoad();
    }
}