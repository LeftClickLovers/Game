using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour {
    public Text SpeakerText;
    public Text MessageText;

    public void SetSpeaker(string speaker) {
        SpeakerText.text = speaker + ":";
    }

    public void SetMessage(string message) {
        MessageText.text = message;
    }

    public void BeginInteraction() {
        gameObject.SetActive(true);
    }

    public void EndInteraction() {
        gameObject.SetActive(false);
    }

    public void Awake() {
        gameObject.SetActive(false);
    }
}