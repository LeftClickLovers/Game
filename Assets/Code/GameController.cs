using UnityEngine;

public class GameController : MonoBehaviour {
    public InteractionUI InteractionUI;

    Player interactingPlayer;
    Girl interactingGirl;

    public void BeginInteraction(Player player, Girl girl) {
        InteractionUI.BeginInteraction();

        interactingPlayer = player;
        interactingGirl = girl;

        interactingPlayer.BeginInteraction();
        interactingGirl.BeginInteraction();

        InteractionUI.SetSpeaker(interactingGirl.Name);
        InteractionUI.SetMessage(interactingGirl.GetDialogue());
    }

    public void EndInteraction() {
        InteractionUI.EndInteraction();

        interactingPlayer.EndInteraction();
        interactingGirl.EndInteraction();

        interactingPlayer = null;
        interactingGirl = null;
    }

    public void ShowNextMessage() {
        if (interactingGirl.HasMoreDialogue()) {
            InteractionUI.SetMessage(interactingGirl.GetDialogue());
        }
    }
}