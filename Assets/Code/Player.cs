using UnityEngine;

public class Player : MonoBehaviour {
    public static Player GetInstance() {
        return GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public float MoveSpeed = 5;

    public enum State {
        None,
        Free,
        Interacting
    }

    State state;
    Girl possibleInteraction;

    public void EndInteraction() {
        state = State.Free;
    }

    void Awake() {
        state = State.Free;
    }

    void Update() {
        switch (state) {
            case State.Free: {
                var ddp = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
                if (ddp.magnitude > 1) {
                    ddp.Normalize();
                }

                transform.position = transform.position + (ddp * MoveSpeed * Time.deltaTime);

                if (possibleInteraction != null && Input.GetKeyDown(KeyCode.E)) {
                    var interactionController = InteractionController.GetInstance();
                    interactionController.BeginInteraction(possibleInteraction);

                    state = State.Interacting;
                }

                break;
            }
            case State.Interacting: {
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    var interactionController = InteractionController.GetInstance();
                    interactionController.EndInteraction();

                    state = State.Free;
                }

                break;
            }
        }        
    }

    void OnTriggerEnter2D(Collider2D them) {
        var girl = them.GetComponent<Girl>();
        if (girl) {
            possibleInteraction = girl;
        }
    }

    void OnTriggerExit2D(Collider2D them) {
        possibleInteraction = null;
    }
}