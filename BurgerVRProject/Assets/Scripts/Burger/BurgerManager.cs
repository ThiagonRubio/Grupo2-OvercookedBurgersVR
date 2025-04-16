using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerManager : MonoBehaviour
{
    public Transform initialAttachPoint;
    public bool preparingBurger = false;
    public bool burgerReady = false;
    private float currentYOffset = 0f;
    private Dictionary<IngredientType, bool> attachedIngredients = new Dictionary<IngredientType, bool>();

    void OnCollisionEnter(Collision collision)
    {
        BurgerItem item = collision.gameObject.GetComponent<BurgerItem>();
        HandleCollision(item);
    }

    public void HandleCollision(BurgerItem item)
    {
        if (item == null) return;
        if (attachedIngredients.ContainsKey(item.ingredientType)) return;
        if (!preparingBurger)
        {
            if (item.ingredientType == IngredientType.PanInferior)
            {
                currentYOffset += 0.02f;
                Vector3 newPos = new Vector3(initialAttachPoint.position.x, initialAttachPoint.position.y + currentYOffset, initialAttachPoint.position.z);
                item.Attach(transform, newPos, initialAttachPoint.rotation);
                preparingBurger = true;
                attachedIngredients[item.ingredientType] = true;
            }
        }
        else if (!burgerReady)
        {
            if (item.ingredientType == IngredientType.PanSuperior)
            {
                currentYOffset += 0.04f;
                Vector3 newPos = new Vector3(initialAttachPoint.position.x, initialAttachPoint.position.y + currentYOffset, initialAttachPoint.position.z);
                item.Attach(transform, newPos, initialAttachPoint.rotation);
                burgerReady = true;
                attachedIngredients[item.ingredientType] = true;
            }
            else
            {
                currentYOffset += 0.02f;
                Vector3 newPos = new Vector3(initialAttachPoint.position.x, initialAttachPoint.position.y + currentYOffset, initialAttachPoint.position.z);
                item.Attach(transform, newPos, initialAttachPoint.rotation);
                attachedIngredients[item.ingredientType] = true;
            }
        }
    }
}