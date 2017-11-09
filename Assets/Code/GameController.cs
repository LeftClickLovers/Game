using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController GetInstance() {
        return GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
}