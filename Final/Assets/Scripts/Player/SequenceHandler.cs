using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceHandler : MonoBehaviour
{
    Sequence currentSequence = null;    
    public static SequenceHandler Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start() {
        InputReader.Instance.onKeyPressed += PressKey;
        InputReader.Instance.onKeyReleased += ReleaseKey;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSequence != null) {
            if (currentSequence.IsCompleted()) {
                currentSequence = CharactersManager.Instance.BefriendCurrentCharacter();
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
