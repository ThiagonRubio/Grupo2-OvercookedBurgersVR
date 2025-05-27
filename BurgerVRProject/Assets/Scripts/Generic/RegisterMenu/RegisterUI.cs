using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RegisterUI : MonoBehaviour
{
    private Animator _registerUIAnimator;
    public static event Action OnRegisterUIToggled;
    private bool hasToggled = false;

    void Start()
    {
        _registerUIAnimator = GetComponent<Animator>();
    }

    public void ToggleMenu()
    {
        _registerUIAnimator.SetTrigger("ButtonPressed");
        if (!hasToggled)
        {
            OnRegisterUIToggled?.Invoke();
            hasToggled = true;
        }
    }
}