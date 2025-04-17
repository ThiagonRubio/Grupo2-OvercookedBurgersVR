using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationController: MonoBehaviour
{

    public enum ControllerType
    {
        RightHand,
        LeftHand
    }

    public ControllerType targetController;
    public InputActionAsset inputAction;
    public XRRayInteractor rayInteractor;
    public TeleportationProvider teleportationProvider;
    
    private InputAction _thumbstickClick;
    
    private bool _isTeleportActive = false;

    void Start()
    {
        rayInteractor.enabled = false;

        _thumbstickClick = inputAction.FindActionMap("XRI " + targetController.ToString() + " Locomotion").FindAction("ThumbstickClick");
        _thumbstickClick.Enable();
        _thumbstickClick.started += OnThumbstickClickStarted;
        _thumbstickClick.canceled += OnThumbstickClickReleased;
    }

    private void OnDestroy()
    {
        _thumbstickClick.started -= OnThumbstickClickStarted;
        _thumbstickClick.canceled -= OnThumbstickClickReleased;
    }

    private void OnThumbstickClickStarted(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = true;
        _isTeleportActive = true;
    }

    private void OnThumbstickClickReleased(InputAction.CallbackContext context)
    {
        if (!_isTeleportActive || !rayInteractor.enabled)
        {
            return;
        }

        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            var request = new TeleportRequest
            {
                destinationPosition = hit.point,
                matchOrientation = MatchOrientation.None,
            };

            teleportationProvider.QueueTeleportRequest(request);
        }

        rayInteractor.enabled = false;
        _isTeleportActive = false;
    }
}