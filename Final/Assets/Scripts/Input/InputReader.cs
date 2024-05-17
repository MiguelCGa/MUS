using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPianoActions
{
    private Controls controls;
    public static InputReader Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        controls = new Controls();
        controls.Piano.Enable();
        controls.Piano.SetCallbacks(this);
    }


    public void OnDo(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnDo1(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnDoSust(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnFa(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnFaSust(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnLa(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnLaSust(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnMi(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnRe(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnReSust(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSi(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSol(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSolSust(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
