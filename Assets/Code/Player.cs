using UnityEngine;

public class Player : MonoBehaviour {
    public float MoveSpeed = 5;

    enum State {
        Free,
        Interacting
    }

    State state;

    public void BeginInteraction() {
        state = State.Interacting;
    }

    public void EndInteraction() {
        state = State.Free;
    }

    public bool IsInteracting() {
        return state == State.Interacting;
    }

    public void Awake() {
        state = State.Free;
    }

    public void Update() {
        switch (state) {
            case State.Free: {
                var ddp = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
                transform.position = transform.position + (ddp * MoveSpeed * Time.deltaTime);

                break;
            }
            case State.Interacting: {
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    var gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
                    gameController.EndInteraction();
                }

                if (Input.GetKeyDown(KeyCode.Space)) {
                    var gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
                    gameController.ShowNextMessage();
                }

                break;
            }
        }        
    }
}