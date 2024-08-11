using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        ~DynamicMenu() {
            closeFunction?.Invoke();
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

    DynamicMenu options = null;
    DynamicMenu pause = null;
    [SerializeField] GameObject optionsPrefab = null;
    [SerializeField] GameObject pausePrefab = null;

    public static UIManager Instance { get; private set; }
    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        options = new DynamicMenu(optionsPrefab);
        pause = new DynamicMenu(pausePrefab);
    }

    public void StartGame() {
        GameManager.Instance.StartGame();
    }

    public void QuitGame() {
        FMODUnity.RuntimeManager.GetBus("Bus:/").stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        GameManager.Instance.QuitGame();
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
        GameManager.Instance.GoToMainMenu();
    }
}
