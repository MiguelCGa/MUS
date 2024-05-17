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

    static GameObject instance = null;
    public static GameManager Instance = null;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = gameObject;
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update() {
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
