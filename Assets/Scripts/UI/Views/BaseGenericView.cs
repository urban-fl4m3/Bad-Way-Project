namespace UI.Views
{
    public abstract class BaseGenericView<TModel> : BaseView
        where TModel : ICustomModel
    {
        protected TModel Model;
        
        protected override void Process(ICustomModel model)
        {
            Model = (TModel) model;
            OnInitialize(Model);
            
            base.Process(model);
        }

        protected abstract void OnInitialize(TModel model);
    }
}