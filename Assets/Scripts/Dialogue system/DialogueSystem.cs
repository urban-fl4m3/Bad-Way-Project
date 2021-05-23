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
        ReadDialogue("000001");
    }

    public void AddDialogue(string key, Dialogue dialogue)
    {
        _dialogues.Add(key,dialogue);
    }

    public void ReadDialogue(string key)
    {
        _nowRead = _dialogues[key];
        _nowState = 0;
        ShowNextMessage();
    }

    public void ShowNextMessage()
    {
        if (_nowState==_nowRead.Replica.Count)
        {
            Debug.Log("return");
            return;
        }
        dialogueBubble.ShowMessage(_nowRead.Replica[_nowState]);
        _nowState++;
    }
}
