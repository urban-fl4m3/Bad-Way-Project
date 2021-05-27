using System.Collections.Generic;
using UnityEngine;

namespace Dialogue_system
{
    [CreateAssetMenu(menuName = "Dialogue System/ Answer", fileName = "Answer", order = 0)]
    public class Answer: ScriptableObject

    {
    public List<AnswerButton> AnswerButtons;

    [System.Serializable]
    public struct AnswerButton
    {
        public string AnswerText;
        public int KeyToDialogue;
        public bool HideAfterReading;
        public bool IsHide;
    }
    }
}