using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace Modules.ActorModule
{
    [CreateAssetMenu(fileName = "New Available Actor Data Provider", menuName = "Actor/Available Actors Data Provider")]
    public class AvailableActorsProvider : ScriptableObject
    {
        [SerializeField] private List<ActorDataProvider> _availableActors= new List<ActorDataProvider>();

        private Dictionary<int, Actor> _availableActorsDict;
        
      
        public Actor GetActorById(int id)
        {
            if (_availableActorsDict == null)
            {
                _availableActorsDict = _availableActors
                    .ToDictionary(x => x.Id, x => x.Actor);

            }
            return _availableActorsDict[id];
        }
    }
}