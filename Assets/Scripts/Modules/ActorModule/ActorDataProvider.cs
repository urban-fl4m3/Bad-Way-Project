using UnityEngine;

namespace Modules.ActorModule
{
    [CreateAssetMenu(fileName = "New Actor Data Provider", menuName = "Actor/Data Provider")]
    public class ActorDataProvider : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private Actor _actor;

        public int Id => _id;
        public Actor Actor => _actor;
    }
}