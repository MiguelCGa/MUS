using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    class DynamicMenu {
        GameObject prefab;
        GameObject instance;
        Action closeFunction;
        public DynamicMenu(GameObject menuPrefab) {
            prefab = menuPrefab;
            instance = null;
            closeFunction = null;
        }
        public void Open(Action onClose) {
            if (prefab != null && instance == null) {
                instance = Instantiate(prefab);
                closeFunction = onClose;
            }
        }
        public void Close() {
            if (prefab != null && instance != null) {
                Destroy(instance);
                instance = null;
                if (closeFunction != null) {
                    closeFunction.Invoke();
                    closeFunction = null;
                }
            }
        }
    }

    static GameObject instance = null;
    public static UIManager Instance = null;

    [SerializeField]
    string gameScene = "Main";
    [SerializeField]
    string mainMenuScene = "MainMenu";

    DynamicMenu options = null;
    DynamicMenu pause = null;
    [SerializeField] GameObject optionsPrefab;
    [SerializeField] GameObject pausePrefab;

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

        options = new DynamicMenu(optionsPrefab);
        pause = new DynamicMenu(pausePrefab);
    }

    public void StartGame() {
        SceneManager.LoadScene(gameScene);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void OpenOptions(Action onOptionsClose = null) {
        options.Open(onOptionsClose);
    }

    public void CloseOptions() {
        options.Close();
    }

    public void OpenPause(Action onPauseClose = null) {
        pause.Open(onPauseClose);
    }

    public void ClosePause() {
        pause.Close();
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene(mainMenuScene);
    }
}
