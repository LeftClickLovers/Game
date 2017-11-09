using UnityEngine;

public class Girl : MonoBehaviour {
    public string Name;

    enum AnswerType {
        Negative,
        Neutral,
        Positve
    }

    struct Answer {
        public AnswerType type;
        public string text;
    }

    struct Dialogue {
        public string[] context;
        public string question;
        public string[] answers;
    }

    Dialogue[] dialogue;
    int dialogueIndex;
    int contextIndex;

    ResourceRequest dialogueResourceRequest;
    bool isLoadingDialogue;

    public void Awake() {
        dialogueResourceRequest = Resources.LoadAsync("Dialogue/Carly", typeof(TextAsset));
        isLoadingDialogue = true;
    }

    public void Update() {
        if (isLoadingDialogue && dialogueResourceRequest.isDone) {
            isLoadingDialogue = false;

            var textAsset = dialogueResourceRequest.asset as TextAsset;
            Debug.Log(textAsset.text);

            // @todo: Free the source text here? Does Unity manage this kind of thing for me?
            // Object.Destroy(textAsset);
        }
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

        return dialogue[dialogueIndex].context[contextIndex++];
    }
}