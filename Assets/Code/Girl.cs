using UnityEngine;

public class Girl : MonoBehaviour {
    public string Name;

    struct Dialogue {
        string[] context;
        string question;
        string[] answers;
    }

    Dialogue[] dialogue;
    int dialogueIndex;

    public void Awake() {
        dialogue = new Dialogue[1];
        
        dialogue[0].context = new string[] {

        };
        
        {
            "Here is some information.",
            "I am now giving you some context.",
            "What would you say in response?"
        };
    }

    public void BeginInteraction() {
        dialogueIndex = 0;
    }

    public void EndInteraction() {

    }

    public bool HasMoreMessages() {
        return dialogueIndex < dialogue.Length;
    }

    public string GetDialogue() {
        if (!HasMoreMessages()) {
            Debug.LogError("There is no more dialogue!");
            return "";
        }

        return dialogue[dialogueIndex++];
    }
}