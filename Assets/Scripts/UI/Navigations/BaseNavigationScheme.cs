using System;
using UI.Helpers;
using UI.Managers;

namespace UI.Navigations
{
    public abstract class BaseViewNavigation
    {
        private Action<Navigation> _onBehaviourComplete;
        
        public abstract Navigation SchemeType { get; }

        protected readonly ViewManager _viewManager;

        protected BaseViewNavigation(ViewManager viewManager)
        {
            _viewManager = viewManager;
        }
        

        public void Execute(Navigation parentBehaviour)
        {
            Behave();
        }

        protected abstract void Behave();
        
        public virtual void Finish()
        {
            _onBehaviourComplete = null;
        }

        public void Init(Action<Navigation> onBehaviourCompleteAction)
        {
            _onBehaviourComplete = onBehaviourCompleteAction;
        }

        protected void CompleteBehaviour(Navigation nextType)
        {
            _onBehaviourComplete?.Invoke(nextType);
        }
    }
}