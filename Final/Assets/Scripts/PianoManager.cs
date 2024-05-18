using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoManager : MonoBehaviour
{
    private SoundManager soundManager;
    void Start()
    {
        soundManager = GetComponent<SoundManager>();

        InputReader.Instance.onKeyPressed += PlayKey;
    }

    private void PlayKey(Notes note)
    {
        soundManager.PlayNote(note);
    }
    private void ReleaseKey(Notes note)
    {
        //soundManager.PlayNote(note);
    }
}
