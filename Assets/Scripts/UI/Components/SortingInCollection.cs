using System.Collections.Generic;
using UnityEngine;

namespace UI.Components
{
    public class SortingInCollection : MonoBehaviour
    {
        public List<RectTransform> _items =new List<RectTransform>();

        public void FetchCollection(RectTransform item)
        {
            _items.Add(item);
        }



        public void UpdateSorting()
        {
            if (_items.Count == 0)
                return;

            for (var i = 0; i < _items.Count - 1; i++)
            {
                if (_items[i + 1].transform.localPosition.z > _items[i].transform.localPosition.z)
                {
                    var actorPar = _items[i];
                    _items[i] = _items[i + 1];
                    _items[i + 1] = actorPar;
                    _items[i].transform.SetSiblingIndex(i);
                    _items[i + 1].transform.SetSiblingIndex(i + 1);
                }
            }
        }
    }
}
