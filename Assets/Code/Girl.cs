using UnityEngine;
using UnityEngine.UI;

public class Girl : MonoBehaviour {
    public enum AnswerType {
        None,
        Negative,
        Neutral,
        Positve
    }

    public class Answer {
        public string Text;
        public AnswerType Type;
    }

    public class Dialogue {
        public int RequiredReputation;
        public string Question;
        public string[] Context;
        public Answer[] Answers;
    }

    public string Name;
    public Text NameText;
    public int Reputation;
    
    Dialogue[] dialogue;

    public Dialogue GetDialogue() {
        if (dialogue == null) {
            Debug.LogError("Failed to get dialogue, there doesn't seem to be any dialogue available.");
            return null;
        }

        var selected = dialogue[0];
        foreach (var d in dialogue) {
            if (Reputation >= d.RequiredReputation && d.RequiredReputation >= selected.RequiredReputation) {
                selected = d;
            }
        }

        return selected;
    }

    void Awake() {
        // @todo: Do these operations in an editor panel.
        NameText.text = Name;

        dialogue = new Dialogue[] {
            new Dialogue {
                RequiredReputation = 0,
                Question = "Does he know about the baby?",
                Context = new string[] {
                    "I never meant to come between you and him.",
                    "If only I'd just gone over when she called.",
                    "If you get me his phone, I might reconsider.",
                    "Try focussing more on your life and less on mine!",
                    "You were meant to be watching him!"
                },
                Answers = new Answer[] {
                    new Answer {
                        Text = "Yes.",
                        Type = AnswerType.Negative
                    },
                    new Answer {
                        Text = "No.",
                        Type = AnswerType.Positve
                    },
                    new Answer {
                        Text = "I don't know.",
                        Type = AnswerType.Neutral
                    }
                }
            }
        };
    }
}