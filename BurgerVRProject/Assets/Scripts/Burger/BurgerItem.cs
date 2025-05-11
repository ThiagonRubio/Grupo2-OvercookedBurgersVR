using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using UnityEngine.XR.Interaction.Toolkit;

// TODO: Español
public enum IngredientType { PanInferior, Paty, Queso, Lechuga, Tomate, PanSuperior }

public class BurgerItem : SpawnableObject
{
    public IngredientType ingredientType;

    public float itemHeight;
    [SerializeField] private float heightCorrection;

    private XRGrabInteractable _grabInteractable;
    private XRGeneralGrabTransformer _grabTransformer;
    private Rigidbody _rigidbody;
    private Collider _collider;

    public bool canBeUsed;
    
    public virtual void Start()
    {
        canBeUsed = true;
        
        if (GetComponent<MeshRenderer>())
        {
            itemHeight = GetComponent<MeshRenderer>().bounds.size.y;
        }
        else
        {
            itemHeight = GetComponentInChildren<MeshRenderer>().bounds.size.y;
        }

        itemHeight += heightCorrection;
        
        _grabInteractable = GetComponent<XRGrabInteractable>();
        _grabTransformer = GetComponent<XRGeneralGrabTransformer>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public virtual void Attach(Transform parent, Vector3 pos, Quaternion rot)
    {
        //Coloca el item en la bandeja
        transform.parent = parent; 
        transform.position = pos;
        transform.rotation = rot;
        
        //Desactiva sus interacciones y físicas
        if (_grabInteractable != null)
            _grabInteractable.enabled = false;
        if (_grabTransformer != null)
            _grabTransformer.enabled = false;
        if (_rigidbody != null)
            _rigidbody.isKinematic = true;
        if (_collider != null)
            _collider.isTrigger = true;
    }

    public virtual void Detach()
    {
        if (_grabInteractable != null)
            _grabInteractable.enabled = true;
        if (_grabTransformer != null)
            _grabTransformer.enabled = true;
        if (_rigidbody != null)
            _rigidbody.isKinematic = false;
        if (_collider != null)
            _collider.isTrigger = false;
    }
}