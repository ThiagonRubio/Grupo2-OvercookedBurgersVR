using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateSnapTurn : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputAction _toggleSnapTurn;

    [SerializeField] private ActionBasedSnapTurnProvider snapTurn;
    [SerializeField] private ActionBasedContinuousTurnProvider continuousTurn;
    
    private void Start()
    {
        _toggleSnapTurn = inputActions.FindActionMap("XRI RightHand Interaction").FindAction("ActivateSnapTurn");
        _toggleSnapTurn.Enable();
        _toggleSnapTurn.performed += ToggleSnapTurn;
    }

    private void OnDestroy()
    {
        _toggleSnapTurn.performed -= ToggleSnapTurn;
    }

    private void ToggleSnapTurn(InputAction.CallbackContext context)
    {
        snapTurn.enabled = !snapTurn.enabled;
        continuousTurn.enabled = !continuousTurn.enabled;
    }
}
