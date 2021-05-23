using System.Collections.Generic;
using UnityEngine;

namespace Dialogue_system
{
    [CreateAssetMenu(menuName = "Dialogue System/ Dialogue", fileName = "Dialogue", order = 0)]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] public List<Replica> Replica;
    }
}