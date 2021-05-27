using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue_system
{
    public class DialogueBubble: MonoBehaviour
    {
        [SerializeField] private Text speakerName;
        [SerializeField] private Text messageText;
        [SerializeField] private List<Button> answerButtons;
        [SerializeField] private Button continueButton;
        public EventHandler<int> NextMessage;
        public void ShowMessage(Replica replica)
        {
            var Name = replica.Name;
            var message = replica.Text;
            var answer = new List<Answer.AnswerButton>();
            var i = 0;
            
            continueButton.onClick.AddListener(()=>{NextMessage?.Invoke(this,replica.nextReplica);});
            if (replica.Answer != null)
            {
                answer = replica.Answer.AnswerButtons;
                continueButton.onClick.RemoveAllListeners();
            }
            
            speakerName.text = Name;
            messageText.text = message;
            
            foreach (var button in answerButtons)
            {
                if (i >= answer.Count)
                {
                    button.gameObject.SetActive(false);
                    i++;
                }
                else
                {
                    var answerButton = answer[i];
                    
                    button.onClick.RemoveAllListeners();
                    button.gameObject.SetActive(!answerButton.IsHide);
                    button.GetComponentInChildren<Text>().text = answerButton.AnswerText;
                    button.onClick.AddListener(() =>
                    {
                        NextMessage?.Invoke(this,answerButton.KeyToDialogue);
                        answerButton.IsHide = answerButton.HideAfterReading;
                        Debug.Log(answerButton.IsHide);
                    });
                    
                    
                    i++;
                }
            }
        }
    }
}