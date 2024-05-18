using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public enum Notes
{
    Do,
    DoSust,
    Re,
    ReSust,
    Mi,
    Fa,
    FaSust,
    Sol,
    SolSust,
    La,
    LaSust,
    Si,
    DoUp
}

public class SoundManager : MonoBehaviour
{
    private List<EventInstance> notes;

    void Start()
    {
        notes = new List<EventInstance>();
        for(int i = 0; i< (int) Notes.DoUp+1 ;  i++)
        {
            notes.Add(RuntimeManager.CreateInstance("event:/"+ ((Notes)i).ToString()));
        }
    }

    public void PlayNote(Notes note)
    {
        notes[(int)note].start();
    }
}
