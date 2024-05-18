using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : MonoBehaviour
{
    AudioSource melody;

    [SerializeField][Range(0.0f, 1.0f)] float noteSizeMargin = 0.5f;
    [SerializeField][Range(0.0f, 1.0f)] float silenceMargin = 0.5f;
    [SerializeField] Notes[] partitureNotes;
    [SerializeField] float[] partitureDurations;
    int currentNoteIndex = 0;
    float timePressed = 0.0f;
    Notes pressedNote = Notes.NONE;
    Notes currentNote {
        get {
            return partitureNotes[currentNoteIndex];
        }
    }
    float currentNoteDuration {
        get {
            return partitureDurations[currentNoteIndex];
        }
    }
    bool inSilence {
        get {
            return currentNoteIndex > 0 && pressedNote == Notes.NONE;
        }
    }
    float silenceTime = 0.0f;


    void Start () {
        melody = GetComponent<AudioSource>();
    }
    private void HandleSilence() {
        if (inSilence) {
            silenceTime += Time.deltaTime;
            if (silenceTime > silenceMargin) {
                Reset();
            }
        }
        else if (silenceTime > 0.0f) {
            silenceTime = 0.0f;
        }
    }
    private void HandlePressedNote() {
        if (pressedNote == currentNote) {
            timePressed += Time.deltaTime;
            if (timePressed > currentNoteDuration + noteSizeMargin) {
                Reset();
            }
        }
    }

    void Update() {
        HandleSilence();
        HandlePressedNote();
    }

    private void Reset() {
        currentNoteIndex = 0;
    }

    private bool NoteTimeSuccess() {
        return Mathf.Abs(timePressed - currentNoteDuration) < noteSizeMargin;
    }

    public void OnKeyPressed(Notes note) {
        pressedNote = note;
        timePressed = 0.0f;
    }
    public void OnKeyReleased(Notes note) {
        pressedNote = Notes.NONE;
        if (NoteTimeSuccess()) {
            ++currentNoteIndex;
        }
        else {
            Reset();
        }
    }

    public bool IsCompleted() {
        return currentNoteIndex == partitureNotes.Length - 1 && NoteTimeSuccess();
    }

    public void PlayMelody() {
        melody.Play();
    }
}
