using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPianoActions
{
    private Controls controls;
    public static InputReader Instance { get; private set; }

    public Action<Notes> onKeyPressed { get; set; }
    public Action<Notes> onKeyReleased { get; set; }

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
        if(context.performed)
        {
            onKeyPressed?.Invoke(Notes.Do);
        }
        if (context.canceled)
        {
            onKeyReleased?.Invoke(Notes.Do);
        }
    }

    public void OnDo1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onKeyPressed?.Invoke(Notes.DoUp);
        }
        if (context.canceled)
        {
            onKeyReleased?.Invoke(Notes.DoUp);
        }
    }

    public void OnDoSust(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onKeyPressed?.Invoke(Notes.DoSust);
        }
        if (context.canceled)
        {
            onKeyReleased?.Invoke(Notes.DoSust);
        }
    }

    public void OnFa(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onKeyPressed?.Invoke(Notes.Fa);
        }
        if (context.canceled)
        {
            onKeyReleased?.Invoke(Notes.Fa);
        }
    }

    public void OnFaSust(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onKeyPressed?.Invoke(Notes.FaSust);
        }
        if (context.canceled)
        {
            onKeyReleased?.Invoke(Notes.FaSust);
        }
    }

    public void OnLa(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onKeyPressed?.Invoke(Notes.La);
        }
        if (context.canceled)
        {
            onKeyReleased?.Invoke(Notes.La);
        }
    }

    public void OnLaSust(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onKeyPressed?.Invoke(Notes.LaSust);
        }
        if (context.canceled)
        {
            onKeyReleased?.Invoke(Notes.LaSust);
        }
    }

    public void OnMi(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onKeyPressed?.Invoke(Notes.Mi);
        }
        if (context.canceled)
        {
            onKeyReleased?.Invoke(Notes.Mi);
        }
    }

    public void OnRe(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onKeyPressed?.Invoke(Notes.Re);
        }
        if (context.canceled)
        {
            onKeyReleased?.Invoke(Notes.Re);
        }
    }

    public void OnReSust(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onKeyPressed?.Invoke(Notes.ReSust);
        }
        if (context.canceled)
        {
            onKeyReleased?.Invoke(Notes.ReSust);
        }
    }

    public void OnSi(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onKeyPressed?.Invoke(Notes.Si);
        }
        if (context.canceled)
        {
            onKeyReleased?.Invoke(Notes.Si);
        }
    }

    public void OnSol(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onKeyPressed?.Invoke(Notes.Sol);
        }
        if (context.canceled)
        {
            onKeyReleased?.Invoke(Notes.Sol);
        }
    }

    public void OnSolSust(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onKeyPressed?.Invoke(Notes.SolSust);
        }
        if (context.canceled)
        {
            onKeyReleased?.Invoke(Notes.SolSust);
        }
    }
}
