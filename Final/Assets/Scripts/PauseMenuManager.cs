using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public void ClosePause() {
        UIManager.Instance.ClosePause();
    }

    public void OpenOptions() {
        gameObject.SetActive(false);
        UIManager.Instance.OpenOptions(OnOptionsClose);
    }

    public void OnOptionsClose() {
        gameObject.SetActive(true);
    }

    public void BackToMainMenu() {
        UIManager.Instance.GoToMainMenu();
    }
}
