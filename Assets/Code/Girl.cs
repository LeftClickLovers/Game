using UnityEngine;

public class Girl : MonoBehaviour {
    public TextAsset Source;

    [System.Serializable]
    class SourceAnswer {
        public string text;
        public string type;
    }

    [System.Serializable]
    class SourceDialogue {
        public int reputation;
        public string question;
        public string[] context;
        public SourceAnswer[] answers;
    }

    [System.Serializable]
    class SourceGirl {
        public string name;
        public SourceDialogue[] dialogue;
    }

    enum AnswerType {
        None,
        Negative,
        Neutral,
        Positve
    }

    class Answer {
        public string text;
        public AnswerType type;
    }

    class Dialogue {
        public int reputation;
        public string question;
        public string[] context;
        public Answer[] answers;
    }

    public string Name;

    Dialogue[] dialogue;
    int dialogueIndex;
    int contextIndex;
    int reputation;

    public void Awake() {
        var sourceGirl = JsonUtility.FromJson<SourceGirl>(Source.text);
        name = sourceGirl.name;

        dialogue = new Dialogue[sourceGirl.dialogue.Length];
        for (var i = 0; i < dialogue.Length; i++) {
            var source = sourceGirl.dialogue[i];
            var dest = dialogue[i];

            dest.reputation = source.reputation;
            dest.question = source.question;

            dest.context = new string[source.context.Length];
            for (var j = 0; j < dest.context.Length; j++) {
                dest.context[j] = source.context[j];
            }

            dest.answers = new Answer[source.answers.Length];
            for (var j = 0; j < dest.answers.Length; j++) {
                var sourceAnswer = source.answers[j];
                var destAnswer = dest.answers[j];

                destAnswer.text = sourceAnswer.text;
                if (sourceAnswer.type == "negative") {
                    destAnswer.type = AnswerType.Negative;
                }
                else if (sourceAnswer.type == "neutral") {
                    destAnswer.type = AnswerType.Neutral;
                }
                else if (sourceAnswer.type == "positive") {
                    destAnswer.type = AnswerType.Positve;
                }
                else {
                    Debug.LogError("Invalid dialogue answer type specified");
                }
            }
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