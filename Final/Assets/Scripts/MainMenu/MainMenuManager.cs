using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame() {
        UIManager.Instance.StartGame();
    }

    public void OpenOptions() {
        gameObject.SetActive(false);
        UIManager.Instance.OpenOptions(OnOptionsClose);
    }

    public void OnOptionsClose() {
        gameObject.SetActive(true);
    }

    public void QuitGame() {
        UIManager.Instance.QuitGame();
    }
    
}
