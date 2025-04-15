using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRButton : MonoBehaviour
{
    [SerializeField] private float deadTime = 1.0f;

    private bool _deadTimeActive = false;

    [SerializeField] private UnityEvent onPressed, onReleased;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Button") && !_deadTimeActive)
        {
            onPressed?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Button") && !_deadTimeActive)
        {
            onReleased?.Invoke();
            StartCoroutine(WaitForDeadTime());
        }
    }

    IEnumerator WaitForDeadTime()
    {
        _deadTimeActive = true;
        yield return new WaitForSeconds(deadTime);
        _deadTimeActive = false;
    }
}
