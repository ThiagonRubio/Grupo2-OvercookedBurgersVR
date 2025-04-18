using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;

public class InteractionTypeSelector : MonoBehaviour
{
    [SerializeField] private GameObject leftControllerDirect;
    [SerializeField] private GameObject rightControllerDirect;
    [SerializeField] private GameObject leftControllerRay;
    [SerializeField] private GameObject rightControllerRay;
    private bool _isUsingRayInteraction;
    
    //Esto es sólo por el canvas momentáneo
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        _isUsingRayInteraction = true;
        UpdateInteractionType(_isUsingRayInteraction);
        UpdateUIText(_isUsingRayInteraction);
    }

    public void ChangeInteractionType()
    {
        _isUsingRayInteraction = !_isUsingRayInteraction;
        
        UpdateInteractionType(_isUsingRayInteraction);
        UpdateUIText(_isUsingRayInteraction);
    }

    private void UpdateInteractionType(bool isUsingRayInteraction)
    {
        leftControllerRay.SetActive(isUsingRayInteraction);
        rightControllerRay.SetActive(isUsingRayInteraction);
        
        leftControllerDirect.SetActive(!isUsingRayInteraction);
        rightControllerDirect.SetActive(!isUsingRayInteraction);
    }
    
    private void UpdateUIText(bool isUsingRayInteraction)
    {
        if (isUsingRayInteraction)
        {
            text.text = "Ray interaction";
        }
        else
        {
            text.text = "Direct Interaction";
        }
    }
}
