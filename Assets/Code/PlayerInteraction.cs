using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    public void OnTriggerStay2D(Collider2D them) {
        if (Input.GetKeyDown(KeyCode.I)) {
            var girl = them.GetComponent<Girl>();
            if (girl) {
                var player = GetComponentInParent<Player>();
                if (!player.IsInteracting()) {
                    var gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
                    gameController.BeginInteraction(player, girl);
                }
            }
        }
    }
}