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
    private Dictionary<IngredientType, bool> attachedIngredients = new Dictionary<IngredientType, bool>();

    void OnCollisionEnter(Collision collision)
    {
        BurgerItem item = collision.gameObject.GetComponent<BurgerItem>();
        HandleCollision(item);
    }

    public void HandleCollision(BurgerItem item)
    {
        if (item == null) return;
        var grab = item.GetComponent<XRGrabInteractable>();
        if (grab != null && grab.isSelected) return;
        if (attachedIngredients.ContainsKey(item.ingredientType)) return;

        if (!preparingBurger && item.ingredientType == IngredientType.PanInferior)
        {
            Vector3 newPos = initialAttachPoint.position;
            item.Attach(transform, newPos, initialAttachPoint.rotation);

            preparingBurger = true;
            attachedIngredients[item.ingredientType] = true;
            lastAttachedItem = item;

            var managerGrab = GetComponent<XRGrabInteractable>();
            if (managerGrab != null) managerGrab.enabled = false;
            var transformer = GetComponent<XRGeneralGrabTransformer>();
            if (transformer != null) transformer.enabled = false;
            var rb = GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;

            return;
        }

        if (preparingBurger && !burgerReady)
        {
            Collider prevCol = lastAttachedItem.GetComponent<Collider>();
            Collider newCol = item.GetComponent<Collider>();

            float prevExtY = prevCol.bounds.extents.y;
            float newExtY = newCol.bounds.extents.y;
            Vector3 basePos = lastAttachedItem.transform.position;

            Vector3 attachPos = new Vector3(
                basePos.x,
                basePos.y + prevExtY + newExtY,
                basePos.z
            );

            if (item.ingredientType == IngredientType.PanSuperior)
            {
                item.Attach(transform, attachPos, initialAttachPoint.rotation);
                burgerReady = true;
                attachedIngredients[item.ingredientType] = true;

                var managerGrab2 = GetComponent<XRGrabInteractable>();
                if (managerGrab2 != null) managerGrab2.enabled = true;
                var transformer2 = GetComponent<XRGeneralGrabTransformer>();
                if (transformer2 != null) transformer2.enabled = true;
                var rb2 = GetComponent<Rigidbody>();
                if (rb2 != null) rb2.isKinematic = false;
            }
            else
            {
                item.Attach(transform, attachPos, initialAttachPoint.rotation);
                attachedIngredients[item.ingredientType] = true;
            }

            lastAttachedItem = item;
        }
    }

    private void OnEnable()
    {
        preparingBurger = false;
        burgerReady = false;
        lastAttachedItem = null;
        attachedIngredients.Clear();
    }
}