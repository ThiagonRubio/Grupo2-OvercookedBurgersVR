using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WristUI : MonoBehaviour
{
    public InputActionAsset inputActions;
    private AudioSource cachedAudioSource;

    private Animator _wristUIAnimator;
    private InputAction _menu;

    private void Awake()
    {
        cachedAudioSource = GetComponent<AudioSource>();

        _wristUIAnimator = GetComponent<Animator>();
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
        _wristUIAnimator.SetTrigger("ButtonPressed");
        cachedAudioSource.Play();
    }
}
