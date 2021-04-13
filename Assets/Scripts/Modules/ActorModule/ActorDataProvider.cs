using UnityEngine;

namespace Modules.ActorModule
{
    [CreateAssetMenu(fileName = "New Actor Data Provider", menuName = "Actor/Actor Data")]
    public class ActorDataProvider : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private Actor _actor;
        [SerializeField] private string name;
        [SerializeField] private Sprite icon;
        
        public int Id => _id;
        public Actor Actor => _actor;
        public string Name => name;
        public Sprite Icon => icon;
    }
}