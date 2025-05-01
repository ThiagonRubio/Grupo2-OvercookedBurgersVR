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

    private bool _isSnapTurnActive;
    
    private void Start()
    {
        _toggleSnapTurn = inputActions.FindActionMap("XRI RightHand Interaction").FindAction("ActivateSnapTurn");
        _toggleSnapTurn.Enable();
        _toggleSnapTurn.performed += ToggleTurnType;
        
        UseContinuousTurn();
    }

    private void OnDestroy()
    {
        _toggleSnapTurn.performed -= ToggleTurnType;
    }

    private void ToggleTurnType(InputAction.CallbackContext context)
    {
        if (_isSnapTurnActive)
        {
            UseContinuousTurn();
        }
        else
        {
            UseSnapTurn();
        }
    }

    private void UseSnapTurn()
    {
        if (continuousTurn != null)
        {
            continuousTurn.rightHandTurnAction.action?.Disable();
            continuousTurn.enabled = false;
        }

        if (snapTurn != null)
        {
            snapTurn.enabled = true;
            snapTurn.rightHandSnapTurnAction.action?.Enable();
        }
        _isSnapTurnActive = true;
    }

    private void UseContinuousTurn()
    {
        if (snapTurn != null)
        {
            snapTurn.rightHandSnapTurnAction.action?.Disable();
            snapTurn.enabled = false;
        }
        
        if (continuousTurn != null)
        {
            continuousTurn.enabled = true;
            continuousTurn.rightHandTurnAction.action?.Enable();
        }
        _isSnapTurnActive = false;
    }
}
