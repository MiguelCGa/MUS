using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuManager : MonoBehaviour
{
    public void CloseOptions() {
        UIManager.Instance.CloseOptions();
    }

    public void ChangeVolume(float newVolume)
    {
        RuntimeManager.StudioSystem.setParameterByName("BackgroundVolume", newVolume);
    }
}
