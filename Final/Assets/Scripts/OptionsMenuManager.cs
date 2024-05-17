using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuManager : MonoBehaviour
{
    public void CloseOptions() {
        UIManager.Instance.CloseOptions();
    }
}
