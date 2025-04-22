using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using UnityEngine.XR.Interaction.Toolkit;

public class BurgerManager : MonoBehaviour
{
    public Transform initialAttachPoint;
    private BurgerItem lastAttachedItem;
    public bool preparingBurger = false;
    public bool burgerReady = false;
    public float pilaHeight;
    private Dictionary<IngredientType, bool> attachedIngredients = new Dictionary<IngredientType, bool>();

    void OnCollisionEnter(Collision collision)
    {
        BurgerItem item = collision.gameObject.GetComponent<BurgerItem>();
        if (item != null)
            HandleCollision(item);
    }

    public void HandleCollision(BurgerItem item)
    {
        if (item == null) return;
        var grab = item.GetComponent<XRGrabInteractable>();
        if (grab != null && grab.isSelected) return;
        if (attachedIngredients.ContainsKey(item.ingredientType)) return;

        MeshRenderer mr = item.GetComponent<MeshRenderer>();
        if (mr == null) mr = item.GetComponentInChildren<MeshRenderer>();
        if (mr == null)
            return;

        float alto = mr.bounds.size.y;

        float yOffset = item.ingredientType == IngredientType.PanSuperior
        ? alto * 0.15f
        : alto * 0.25f;


        Vector3 pos = new Vector3(
            initialAttachPoint.position.x,
            pilaHeight + yOffset,
            initialAttachPoint.position.z
        );

        item.Attach(transform, pos, initialAttachPoint.rotation);
        pilaHeight += alto;
        attachedIngredients[item.ingredientType] = true;
        lastAttachedItem = item;

        if (!preparingBurger && item.ingredientType == IngredientType.PanInferior)
        {
            preparingBurger = true;
            var g = GetComponent<XRGrabInteractable>();
            if (g != null) g.enabled = false;
            var t = GetComponent<XRGeneralGrabTransformer>();
            if (t != null) t.enabled = false;
            var rb = GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;
        }
        else if (!burgerReady && item.ingredientType == IngredientType.PanSuperior)
        {
            burgerReady = true;
            var g = GetComponent<XRGrabInteractable>();
            if (g != null) g.enabled = true;
            var t = GetComponent<XRGeneralGrabTransformer>();
            if (t != null) t.enabled = true;
            var rb = GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;
        }
    }

    private void OnEnable()
    {
        preparingBurger = false;
        burgerReady = false;
        lastAttachedItem = null;
        pilaHeight = initialAttachPoint.position.y;
        attachedIngredients.Clear();
    }
}