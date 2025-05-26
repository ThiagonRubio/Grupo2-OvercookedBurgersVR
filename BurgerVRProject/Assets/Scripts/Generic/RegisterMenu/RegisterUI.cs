using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterUI : MonoBehaviour
{
    private Animator _registerUIAnimator;
    void Start()
    {
        _registerUIAnimator = GetComponent<Animator>();
        _registerUIAnimator.SetTrigger("ButtonPressed");

    }

    public void ToggleMenu()
    {
        _registerUIAnimator.SetTrigger("ButtonPressed");
    }
}
