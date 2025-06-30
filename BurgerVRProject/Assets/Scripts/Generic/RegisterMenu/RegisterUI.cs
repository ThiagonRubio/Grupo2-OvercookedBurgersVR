using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RegisterUI : MonoBehaviour
{
    private Animator _registerUIAnimator;
    private AudioSource cachedAudioSource;
    public static event Action OnRegisterUIToggled;
    private bool hasToggled = false;

    void Awake()
    {
        _registerUIAnimator = GetComponent<Animator>();
        cachedAudioSource = GetComponent<AudioSource>();
    }

    public void ToggleMenu()
    {
        _registerUIAnimator.SetTrigger("ButtonPressed");
        cachedAudioSource.Play();

        if (!hasToggled)
        {
            OnRegisterUIToggled?.Invoke();
            hasToggled = true;
        }
    }
}