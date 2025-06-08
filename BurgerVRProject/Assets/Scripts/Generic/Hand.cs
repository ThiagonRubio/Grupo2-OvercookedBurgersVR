using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Hand : MonoBehaviour, IUpdatable
{
    [SerializeField] private GameObject handPrefab;
    [SerializeField] private bool hideHandOnSelect = false;
    
    public InputDeviceCharacteristics inputDeviceCharacteristics;

    private InputDevice _targetDevice;
    private Animator _handAnimator;
    private SkinnedMeshRenderer _handMesh;
    private GameObject spawnedHand;

    private void Awake()
    {
        InitializeHands();
        CustomUpdateManager.Instance.Register(this);
    }
    private void OnDestroy()
    {
        CustomUpdateManager.Instance.Unregister(this);
    }

    public void OnUpdate()
    {
        if (_targetDevice.isValid)
        {
            UpdateHand();
        }
        else
        {
            InitializeHands();
        }
    }

    private void InitializeHands()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, devices);

        if (devices.Count > 0)
        {
            _targetDevice = devices[0];

            spawnedHand = Instantiate(handPrefab, transform);
            _handAnimator = spawnedHand.GetComponent<Animator>();
            _handMesh = spawnedHand.GetComponentInChildren<SkinnedMeshRenderer>();
        }
    }

    private void UpdateHand()
    {
        if (_targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            _handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            _handAnimator.SetFloat("Trigger", 0);
        }

        if (_targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            _handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            _handAnimator.SetFloat("Grip", 0);
        }
    }

    public void HideHandOnSelect()
    {
        if (hideHandOnSelect)
        {
            _handMesh.enabled = !_handMesh.enabled;
            spawnedHand.SetActive(!spawnedHand.activeSelf);
        }
    }
}
