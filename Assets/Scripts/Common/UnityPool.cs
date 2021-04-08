using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class UnityPool<TMonoBehaviour> : List<TMonoBehaviour>
        where TMonoBehaviour : MonoBehaviour
    {
        private readonly TMonoBehaviour _prefab;
        private Transform _parent;
        private int _activeLength;
        
        public UnityPool(TMonoBehaviour prefab)
        {
            _prefab = prefab;
        }

        public UnityPool(TMonoBehaviour prefab, int prewarm, Transform parent = null)
        {
            _prefab = prefab;
            ToParent(parent);
            Warm(prewarm);
        }

        public int Warm(int count)
        {
            var warmedCount = 0;
            
            for (var i = Count; i < count; i++)
            {
                warmedCount++;
                Instantiate();
            }

            return warmedCount;
        }

        public void ToParent(Transform parent)
        {
            _parent = parent;
        }

        public TMonoBehaviour Instantiate()
        {
            if (_activeLength < Count)
            {
                this[_activeLength].gameObject.SetActive(true);
            }
            else
            {
                var instance = Object.Instantiate(_prefab, _parent);
                instance.gameObject.SetActive(true);
                Add(instance);
            }
            
            _activeLength++;

            return this[_activeLength - 1];
        }

        public void Resize(int newSize)
        {
            var iterationsLeft = newSize;
            
            for (var i = 0; i < Count; i++)
            {
                if (i < newSize)
                {
                    iterationsLeft--;
                }
                
                this[i].gameObject.SetActive(i < newSize);
            }

            for (var i = 0; i < iterationsLeft; i++)
            {
                Instantiate();
            }
        }
    }
}