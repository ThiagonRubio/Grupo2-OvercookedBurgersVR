using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using UnityEngine.XR.Interaction.Toolkit;
public enum IngredientType { PanInferior, Paty, Queso, Lechuga, Tomate, PanSuperior }

public class BurgerItem : MonoBehaviour
{
    public IngredientType ingredientType;

    public float itemHeight;
    [SerializeField] private float heightCorrection;

    private void Start()
    {
        if (GetComponent<MeshRenderer>())
        {
            itemHeight = GetComponent<MeshRenderer>().bounds.size.y;
        }
        else
        {
            itemHeight = GetComponentInChildren<MeshRenderer>().bounds.size.y;
        }

        itemHeight += heightCorrection;
    }

    public void Attach(Transform parent, Vector3 pos, Quaternion rot)
    {
        //Coloca el item en la bandeja
        transform.parent = parent; 
        transform.position = pos;
        transform.rotation = rot;
        
        //Desactiva sus interacciones y físicas
        XRGrabInteractable grab = GetComponent<XRGrabInteractable>();
        if (grab != null)
            grab.enabled = false;
        XRGeneralGrabTransformer transformer = GetComponent<XRGeneralGrabTransformer>();
        if (transformer != null)
            transformer.enabled = false;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true;
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.isTrigger = true;
    }
}