using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class SoundManager : MonoBehaviour
{
    private Dictionary<Notes,EventInstance> playingNotes;
    void Start()
    {
        playingNotes = new Dictionary<Notes,EventInstance>();
    }

    public void PlayNote(Notes note)
    {
        //notes[(int)note].start();
        ReleaseNote(note);

        playingNotes.Add(note,RuntimeManager.CreateInstance("event:/"+note.ToString()));
        playingNotes[note].start();
    }
    public void ReleaseNote(Notes note)
    {
        EventInstance tmp = new EventInstance();
        if (playingNotes.TryGetValue(note, out tmp))
        {
            tmp.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            tmp.release();
            playingNotes.Remove(note);
        }
    }

    public void UpdateIntensity(Characters character)
    {
        RuntimeManager.StudioSystem.setParameterByName(character.ToString() + "Vol",1);
    }

}
