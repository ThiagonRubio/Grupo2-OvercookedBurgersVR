using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WristUI : MonoBehaviour
{
    //Este script es placeholder por el momento

    public InputActionAsset inputActions;

    private Canvas _wristUICanvas;
    private InputAction _menu;

    private void Start()
    {
        _wristUICanvas = GetComponent<Canvas>();
        _menu =  inputActions.FindActionMap("XRI LeftHand Interaction").FindAction("ActivateWristMenu");
        _menu.Enable();
        _menu.performed += ToggleMenu;
    }

    private void OnDestroy()
    {
        _menu.performed -= ToggleMenu;
    }

    private void ToggleMenu(InputAction.CallbackContext context)
    {
        _wristUICanvas.enabled = !_wristUICanvas.enabled;
    }
}
