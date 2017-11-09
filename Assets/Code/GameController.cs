﻿using UnityEngine;

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

        InteractionUI.SetSpeaker(interactingGirl.name);
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
        if (interactingGirl.HasMoreMessages()) {
            InteractionUI.SetMessage(interactingGirl.GetDialogue());
        }
    }
}