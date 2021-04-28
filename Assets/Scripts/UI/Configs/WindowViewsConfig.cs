using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Configs
{
    [CreateAssetMenu(fileName = "New Window Views Config", menuName = "UI/Views Config")]
    public class WindowViewsConfig : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private List<ViewWithId> _views;
        [SerializeField] private GameObject _canvas;
        
        private Dictionary<string, GameObject> _viewsDict = new Dictionary<string, GameObject>();
        
        public void OnBeforeSerialize()
        {
            
        }

        public void OnAfterDeserialize()
        {
            _viewsDict = _views.ToDictionary(x => x.Id, x => x.View);
        }

        public GameObject LoadView(string id)
        {
            return _viewsDict[id];
        }

        public GameObject LoadCanvas()
        {
            return _canvas;
        }
        
        [Serializable]
        private struct ViewWithId
        {
            public GameObject View;
            public string Id;
        }
    }
}