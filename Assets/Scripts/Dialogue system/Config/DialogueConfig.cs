using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dialogue_system
{
    [CreateAssetMenu(menuName = "Dialogue System/ Config ", fileName = "Dialogue Config", order = 0)]
    public class DialogueConfig: ScriptableObject
    {
        [SerializeField] private List<DialogueWithId> _dialoguesWithIds;

        private Dictionary<string, Dialogue> _dialogueDict = new Dictionary<string, Dialogue>();

        public Dictionary<string, Dialogue> GetDictionary()
        {
            _dialogueDict = _dialoguesWithIds.ToDictionary(x => x.Id,
                x => x.Dialogue);
            
            return _dialogueDict;
        }
        
        [Serializable]
        private struct DialogueWithId
        {
            public string Id;
            public Dialogue Dialogue;
        }
    }
}