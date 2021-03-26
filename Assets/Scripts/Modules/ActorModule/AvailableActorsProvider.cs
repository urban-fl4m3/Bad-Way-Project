using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace Modules.ActorModule
{
    [CreateAssetMenu(fileName = "New Available Actor Data Provider", menuName = "Actor/Available Actors Data Provider")]
    public class AvailableActorsProvider : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private ActorDataProvider[] _availableActors;

        private Dictionary<int, Actor> _availableActorsDict = new SerializedDictionary<int, Actor>();
        
        public void OnBeforeSerialize()
        {
            
        }

        public void OnAfterDeserialize()
        {
            _availableActorsDict = _availableActors
                .ToDictionary(x => x.Id, x => x.Actor);
        }

        public Actor GetActorById(int id)
        {
            return _availableActorsDict[id];
        }
    }
}