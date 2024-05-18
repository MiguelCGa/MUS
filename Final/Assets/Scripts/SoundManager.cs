using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    private List<EventInstance> notes;
    private List<EventInstance> playingNotes;

    void Start()
    {
        notes = new List<EventInstance>();
        foreach (string note in Enum.GetNames(typeof(Notes))) {
            notes.Add(RuntimeManager.CreateInstance("event:/" + note));
        }
    }

    public void PlayNote(Notes note)
    {
        notes[(int)note].start();
    }
    public void ReleaseNote(Notes note)
    {
        notes[(int)note].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
