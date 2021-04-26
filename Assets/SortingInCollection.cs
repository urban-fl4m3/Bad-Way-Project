using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UI;
using UnityEngine;

public class SortingInCollection : MonoBehaviour
{ 
    public  RectTransform[] _actorParameters;
    

    public void AddCollection(List<RectTransform> rectTransforms)
    {
        _actorParameters = new RectTransform[rectTransforms.Count];

        int count = 0;
        for (var i = 0; i < rectTransforms.Count; i++)
        {
            count = i;
            _actorParameters[count] = rectTransforms[count];
        }
    }
    public void UpdateSorting()
    {
        if(_actorParameters.Length==0)
            return;
        
        for (var i = 0; i < _actorParameters.Length-1; i++)
        {
            if (_actorParameters[i + 1].transform.localPosition.z > _actorParameters[i].transform.localPosition.z)
            {
                var actorPar = _actorParameters[i];
                _actorParameters[i] = _actorParameters[i + 1];
                _actorParameters[i + 1] = actorPar;
                _actorParameters[i].transform.SetSiblingIndex(i);
                _actorParameters[i+1].transform.SetSiblingIndex(i+1);
            }
        }
    }
}
