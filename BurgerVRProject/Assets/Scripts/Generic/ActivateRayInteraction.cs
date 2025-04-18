using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateRayInteraction : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputAction _toggleRayControl;
    
    [SerializeField] private GameObject leftRayController;
    [SerializeField] private GameObject rightRayController;

    private void Start()
    {
        _toggleRayControl = inputActions.FindActionMap("XRI LeftHand Interaction").FindAction("ActivateRayInteraction");
        _toggleRayControl.Enable();
        _toggleRayControl.performed += ToggleRayControl;
    }

    private void OnDestroy()
    {
        _toggleRayControl.performed -= ToggleRayControl;
    }

    private void ToggleRayControl(InputAction.CallbackContext context)
    {
        leftRayController.SetActive(!leftRayController.activeSelf);
        rightRayController.SetActive(!rightRayController.activeSelf);
    }
}
