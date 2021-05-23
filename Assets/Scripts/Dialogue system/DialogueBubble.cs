using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue_system
{
    public class DialogueBubble: MonoBehaviour
    {
        [SerializeField] private Text speakerName;
        [SerializeField] private Text messageText;
        [SerializeField] private List<Button> answerButton;

        public void ShowMessage(Replica replica)
        {
            var Name = replica.Name;
            var message = replica.Text;
            var answer = replica.AnswerButtons;
            var i = 0;

            speakerName.text = Name;
            messageText.text = message;

            foreach (var button in answerButton)
            {
                if (i >= answer.Count)
                {
                    button.gameObject.SetActive(false);
                    i++;
                }
                else
                {
                    var text = answer[i];
                    button.gameObject.SetActive(true);
                    button.GetComponentInChildren<Text>().text = text.AnswerText;
                    i++;
                }
            }
        }
        
    }
}