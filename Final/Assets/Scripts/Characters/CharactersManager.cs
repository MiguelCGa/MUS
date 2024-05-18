using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharactersManager : MonoBehaviour
{
    [SerializeField] GameObject[] characters;
    List<GameObject> strangers = new List<GameObject>();
    List<GameObject> friends = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var character in characters) {
            strangers.Add(character);
        }
        strangers.First().SetActive(true);
        PlayCurrentMelody();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sequence BefriendCurrentCharacter() {
        if (strangers.Count <= 0) {
            return null;
        }
        
        friends.Add(strangers.First());
        strangers.RemoveAt(0);
        friends.Last().GetComponent<Sequence>().enabled = false;

        if (strangers.Count <= 0) {
            return null;
        }
        
        strangers.First().SetActive(true);
        Sequence nextSequence = strangers.First().GetComponent<Sequence>();
        nextSequence.PlayMelody();
        return nextSequence;
    }

    public void PlayCurrentMelody() {
        if (strangers.Count > 0) {
            strangers.First().GetComponent<Sequence>().PlayMelody();
        }
    }
}
