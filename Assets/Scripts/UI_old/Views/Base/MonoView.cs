using UI.Views.Interfaces;

namespace UI.Views.Base
{
    public sealed class MonoView
    {
        private readonly ICompositeView _compositeView;
        private readonly IViewCallbacks _viewCallbacks;

        public MonoView(ICompositeView compositeView, IViewCallbacks viewCallbacks)
        {
            _compositeView = compositeView;
            _viewCallbacks = viewCallbacks;
        }
    }
}