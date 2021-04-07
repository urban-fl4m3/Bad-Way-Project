using System;
using System.Collections.Generic;

namespace Common
{
    public class TypeContainer
    {
        private Dictionary<Type, object> _map;

        public TypeContainer()
        {
            _map = new Dictionary<Type, object>();
        }

        public void Add<T>(object instance)
        {
            _map.Add(typeof(T), instance);
        }
        
        public object Resolve(Type type)
        {
            if (_map.ContainsKey(type))
                return _map[type];
            
            return null;
        }

        public T Resolve<T>() where T : class
        {
            if (_map.ContainsKey(typeof(T)))
                return _map[typeof(T)] as T;

            return null;
        }

        public IReadOnlyDictionary<Type, object> Map => _map;
    }
}