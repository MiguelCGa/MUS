using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class CharactersManager : MonoBehaviour
{
    [SerializeField] EventReference victorySound;
    [SerializeField] float timeFromVictoryToMelody = 1.5f;
    float timeElapsedFromVictory = 0.0f;
    [SerializeField] PianoManager piano;
    [SerializeField] GameObject[] characters;
    List<GameObject> strangers = new List<GameObject>();
    List<GameObject> friends = new List<GameObject>();
    bool victoriing = false;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (var character in characters) {
            strangers.Add(character);
        }
        strangers.First().SetActive(true);
        PlayCurrentMelody();
    }

    void Update() {
        if (victoriing) {
            timeElapsedFromVictory += Time.deltaTime;
            if (VictorySoundFinished()) {
                victoriing = false;
                strangers.First().GetComponent<Sequence>().PlayMelody();
            }
        }
    }

    public Sequence BefriendCurrentCharacter() {
        if (strangers.Count <= 0) {
            return null;
        }
        
        friends.Add(strangers.First());
        strangers.RemoveAt(0);
        friends.Last().GetComponent<Sequence>().enabled = false;
        piano.IncreaseCharacter();

        if (strangers.Count <= 0) {
            return null;
        }
        
        strangers.First().SetActive(true);
        PlayVictorySound();
        return strangers.First().GetComponent<Sequence>();
    }

    public void PlayCurrentMelody() {
        if (strangers.Count > 0) {
            strangers.First().GetComponent<Sequence>().PlayMelody();
        }
    }

    public Sequence getCurrentCharacter() {
        if (strangers.Count > 0)
            return strangers.First().GetComponent<Sequence>();
        return null;
    }

    private void PlayVictorySound() {
        RuntimeManager.PlayOneShot(victorySound);
        victoriing = true;
        timeElapsedFromVictory = 0.0f;
    }

    private bool VictorySoundFinished() {
        return timeFromVictoryToMelody <= timeElapsedFromVictory;
    }
}
