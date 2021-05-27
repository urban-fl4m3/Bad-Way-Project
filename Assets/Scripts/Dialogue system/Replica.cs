using UnityEngine;

namespace Dialogue_system
{
    [System.Serializable]
    public struct Replica
    {
        public string Name;
        [TextArea]
        public string Text;
        public int nextReplica;
        public Answer Answer;
    }
}