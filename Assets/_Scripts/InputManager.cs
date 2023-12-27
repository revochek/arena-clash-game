using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{   
    public static InputManager instance;

    private PlayerInput _heroInput;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        _heroInput = new PlayerInput();
        _heroInput.Enable();
    }

    public Vector2 GetMoveDirection()
    {
        return _heroInput.Player.Move.ReadValue<Vector2>();
    }

    public bool IsBasicAttackWasPressedThisFrame()
    {
        return _heroInput.Player.Shoot.WasPressedThisFrame();
    }

    public bool IsUltimateAttackWasPressedThisFrame()
    {
        return _heroInput.Player.Ultimate.WasPressedThisFrame();
    }
}
