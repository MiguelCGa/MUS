using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SoundManager : MonoBehaviour
{
    private FMOD.Studio.EventInstance piano;

    void Start()
    {
        piano = RuntimeManager.CreateInstance("event:/Piano");
    }
}
