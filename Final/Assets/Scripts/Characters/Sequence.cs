using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class Sequence : MonoBehaviour
{
    [SerializeField] EventReference melody;
    [SerializeField] [Range(0.1f,5)] float noteSizeMargin = 0.5f;
    [SerializeField] [Range(0.1f,5)] float silenceMargin = 0.5f;
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
    bool completed = false;

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

    private void CheckProgress() {
        if (pressedNote == currentNote && NoteTimeSuccess()) {
            ++currentNoteIndex;
        }
        else {
            Reset();
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
        if (pressedNote != Notes.NONE) {
            CheckProgress();
        }
        pressedNote = note;
        timePressed = 0.0f;
    }
    public void OnKeyReleased(Notes note) {
        if (note == pressedNote) {
            if (currentNoteIndex == partitureNotes.Length - 1 && NoteTimeSuccess()) {
                completed = true;
            }
            CheckProgress();
            pressedNote = Notes.NONE;
        }
    }

    public bool IsCompleted() {
        return completed;
    }

    public void PlayMelody() {
        RuntimeManager.PlayOneShot(melody);
    }
}
