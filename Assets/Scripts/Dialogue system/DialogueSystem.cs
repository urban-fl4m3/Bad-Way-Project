using System.Collections.Generic;
using Dialogue_system;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private DialogueBubble dialogueBubble;
    [SerializeField] private DialogueConfig _dialogueConfig;
    
    private Dictionary<string, Dialogue> _dialogues = new Dictionary<string, Dialogue>();
    private Dialogue _nowRead;
    private int _nowState;

    private void Start()
    {
        _dialogues = _dialogueConfig.GetDictionary();
        dialogueBubble.NextMessage += ShowNextMessage;
        ReadDialogue("000001");
    }

    private void ShowNextMessage(object sender, int e)
    {
        Debug.Log(e);
        _nowState = e;
        if (_nowState==_nowRead.Replica.Count)
        {
            Debug.Log("exitDialogue");
            return;
        }
        dialogueBubble.ShowMessage(_nowRead.Replica[_nowState]);
    }

    public void AddDialogue(string key, Dialogue dialogue)
    {
        _dialogues.Add(key,dialogue);
    }

    public void ReadDialogue(string key)
    {
        _nowRead = _dialogues[key];
        _nowState = 0;
        ShowNextMessage(this,_nowState);
    }
}
