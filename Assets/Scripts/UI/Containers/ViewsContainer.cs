using System.Collections.Generic;
using System.Linq;
using UI.Helpers;
using UI.Models;
using UI.Views;
using UnityEngine;

namespace UI.Containers
{
    [CreateAssetMenu(fileName = "New Views Container", menuName = "UI/Views Container")]
    public class ViewsContainer : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private List<WindowViewModel> _views;

        private Dictionary<Window, BaseView> _viewsDictionary 
            = new Dictionary<Window, BaseView>();

        public BaseView GetView(Window t)
        {
            return _viewsDictionary.TryGetValue(t, out var view) ? view : null;
        }
        
        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            _viewsDictionary = _views.ToDictionary(x => x.Type,
                x => x.View);
        }
    }
}