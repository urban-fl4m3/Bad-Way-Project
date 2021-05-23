using System.Collections.Generic;
using UnityEngine;

namespace Dialogue_system
{
    [System.Serializable]
    public struct Replica
    {
        public string Name;
        [TextArea]
        public string Text;
        public List<AnswerButton> AnswerButtons;

        public Replica(string name, string text, List<AnswerButton> answerButton)
        {
            Name = name;
            Text = text;
            AnswerButtons = answerButton;
        }
        
    }
    
    [System.Serializable]
    public struct AnswerButton
    {
        public string AnswerText;
        public string KeyToDialogue;
    }
}