using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceHandler : MonoBehaviour
{
    CharactersManager charactersManager;
    Sequence currentSequence = null;    
    // Start is called before the first frame update
    void Start() {
        charactersManager = GetComponent<CharactersManager>();
        currentSequence = charactersManager.getCurrentCharacter();
        InputReader.Instance.onKeyPressed += PressKey;
        InputReader.Instance.onKeyReleased += ReleaseKey;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSequence != null) {
            if (currentSequence.IsCompleted()) {
                currentSequence = charactersManager.BefriendCurrentCharacter();
            }
        }
    }

    private void PressKey(Notes note) {
        if (currentSequence != null) {
            currentSequence.OnKeyPressed(note);
        }
    }

    private void ReleaseKey(Notes note) {
        if (currentSequence != null) {
            currentSequence.OnKeyReleased(note);
        }
    }
}
