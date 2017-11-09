using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour {
    public Canvas Canvas;
    public Text SpeakerText;
    public Text MessageText;
    public Text AnswerPrefab;
    public Transform AnswersGroup;

    Girl interactingWith;
    Girl.Dialogue currentDialogue;

    enum State {
        Idle,
        Context,
        Question
    }

    State state;
    int contextIndex;
    int answerIndex;

    public static InteractionController GetInstance() {
        return GameObject.FindWithTag("InteractionController").GetComponent<InteractionController>();
    }

    public void BeginInteraction(Girl girl) {
        if (state != State.Idle) {
            Debug.LogError("Failed to begin interaction, the interaction state is not idle.");
            return;
        }

        interactingWith = girl;
        currentDialogue = interactingWith.GetDialogue();
        
        state = State.Context;
        contextIndex = 0;
        
        SpeakerText.text = interactingWith.Name + ":";
        MessageText.text = currentDialogue.Context[contextIndex++];

        foreach (var child in AnswersGroup) {
            Destroy(((Transform) child).gameObject);
        }

        Canvas.gameObject.SetActive(true);
    }

    public void EndInteraction() {
        if (state == State.Idle) {
            Debug.LogError("Failed to end interaction, the interaction state is idle.");
            return;
        }

        Canvas.gameObject.SetActive(false);
        state = State.Idle;
    }

    void Awake() {
        Canvas.gameObject.SetActive(false);
        state = State.Idle;
    }

    void Update() {
        switch (state) {
            case State.Idle: {
                break;
            }
            case State.Context: {
                if (Input.GetKeyDown(KeyCode.Space)) {
                    if (contextIndex < currentDialogue.Context.Length) {
                        MessageText.text = currentDialogue.Context[contextIndex++];
                    }
                    else {
                        MessageText.text = currentDialogue.Question;

                        foreach (var answer in currentDialogue.Answers) {
                            var answerObject = Instantiate(AnswerPrefab, AnswersGroup);
                            
                            var answerText = answerObject.GetComponent<Text>();
                            answerText.text = answer.Text;
                        }
                        
                        state = State.Question;
                        answerIndex = 0;

                        AnswersGroup.GetChild(answerIndex).GetComponent<Text>().color = Color.yellow;
                    }
                }

                break;
            }
            case State.Question: {
                int oldAnswerIndex = answerIndex;

                if (Input.GetKeyDown(KeyCode.A)) {
                    if (answerIndex > 0) {
                        answerIndex--;
                    }
                    else {
                        answerIndex = currentDialogue.Answers.Length - 1;
                    }
                }
                
                if (Input.GetKeyDown(KeyCode.D)) {
                    if (answerIndex < currentDialogue.Answers.Length - 1) {
                        answerIndex++;
                    }
                    else {
                        answerIndex = 0;
                    }
                }

                if (oldAnswerIndex != answerIndex) {
                    Text oldAnswerText = AnswersGroup.GetChild(oldAnswerIndex).GetComponent<Text>();
                    oldAnswerText.color = Color.white;

                    Text newAnswerText = AnswersGroup.GetChild(answerIndex).GetComponent<Text>();
                    newAnswerText.color = Color.yellow;
                }

                if (Input.GetKeyDown(KeyCode.Space)) {
                    var player = Player.GetInstance();
                    player.EndInteraction();

                    EndInteraction();
                }

                break;
            }
        }
    }
}