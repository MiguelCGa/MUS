using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    string gameScene = "Main";
    [SerializeField]
    string mainMenuScene = "MainMenu";

    [SerializeField]
    string pauseInputAxis = "Pause";

    public static GameManager Instance { get; private set; }
    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetButtonDown(pauseInputAxis)) {
            PauseGame();
        }
    }

    bool IsGameSceneOn() {
        return SceneManager.GetActiveScene().name == gameScene;
    }

    public void StartGame() {
        SceneManager.LoadScene(gameScene);
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene(mainMenuScene);
        CharactersManager.Instance.ShutDown();
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void PauseGame() {
        if (IsGameSceneOn()) {
            Time.timeScale = 0.0f;
            UIManager.Instance.OpenPause(OnResumeGame);
        }
    }

    public void OnResumeGame() {
        Time.timeScale = 1.0f;
    }
    
    public void ResumeGame() {
        if (IsGameSceneOn()) {
            UIManager.Instance.ClosePause();
        }
    }
}
