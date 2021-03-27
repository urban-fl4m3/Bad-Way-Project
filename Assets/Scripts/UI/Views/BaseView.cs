using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class BaseView : MonoBehaviour, IView
    {
        [SerializeField] private Button[] _closeButtons;

        private readonly Dictionary<string, BaseView> _nestedViews = new Dictionary<string, BaseView>();
        
        public void Initialize(ICustomModel model)
        {
            Process(model);
            AddCloseButtonEvents();
        }

        private void OnDestroy()
        {
            Clear();
        }

        protected TView AddInnerView<TView>(TView view, ICustomModel model)
            where TView : BaseView
        {
            var viewId = view.GetInstanceID().ToString();
            TView nested;
            
            if (_nestedViews.ContainsKey(viewId))
            {
                nested = (TView)_nestedViews[viewId];
                nested.gameObject.SetActive(true);
            }
            else
            {
                nested = Instantiate(view, transform);
                _nestedViews.Add(viewId, nested);
            }
            
            nested.Initialize(model);
            return nested;
        }

        // protected TView RemoveNestedView<TView>(TView view)
        //     where TView : BaseView
        // {
        //     var viewId = view.GetInstanceID().ToString();
        //
        //     if (_nestedViews.ContainsKey(viewId))
        //     {
        //         var nested = _nestedViews[viewId];
        //         nested.CloseView();
        //     }
        // }

        protected virtual void Process(ICustomModel model)
        {
            
        }

        protected virtual void Clear()
        {
            _nestedViews.Clear();
            
            foreach (var button in _closeButtons)
            {
                button.onClick.RemoveListener(CloseView);
            }
        }

        private void AddCloseButtonEvents()
        {
            foreach (var button in _closeButtons)
            {
                button.onClick.AddListener(CloseView);
            }
        }

        public void CloseView()
        {
            foreach (var view in _nestedViews)
            {
                view.Value.CloseView();
            }
            
            Clear();
            gameObject.SetActive(false);
        }
    }
}