using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using UnityEngine.XR.Interaction.Toolkit;
public enum IngredientType { PanInferior, Paty, Queso, Lechuga, Tomate, PanSuperior }

public class BurgerItem : MonoBehaviour
{
    public IngredientType ingredientType;
    public void Attach(Transform parent, Vector3 pos, Quaternion rot)
    {
        transform.parent = parent;
        transform.position = pos;
        transform.rotation = rot;
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

    void OnCollisionEnter(Collision collision)
    {
        if (transform.parent == null) return;
        BurgerManager manager = transform.parent.GetComponent<BurgerManager>();
        if (manager == null) return;
        BurgerItem otherItem = collision.gameObject.GetComponent<BurgerItem>();
        if (otherItem != null && otherItem.transform.parent == null)
        {
            manager.HandleCollision(otherItem);
        }
    }
}