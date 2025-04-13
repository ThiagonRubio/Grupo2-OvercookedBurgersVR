using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerManager : MonoBehaviour
{
    public Transform initialAttachPoint;
    public bool preparingBurger = false;
    public bool burgerReady = false;
    private Transform currentAttachPoint;
    private Dictionary<IngredientType, bool> attachedIngredients = new Dictionary<IngredientType, bool>();
    void Start()
    {
        currentAttachPoint = initialAttachPoint;
    }

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
                item.Attach(transform, initialAttachPoint);
                preparingBurger = true;
                attachedIngredients[item.ingredientType] = true;
                currentAttachPoint = item.attachPoint;
            }
        }
        else if (!burgerReady)
        {
            if (item.ingredientType == IngredientType.PanSuperior)
            {
                item.Attach(transform, currentAttachPoint);
                burgerReady = true;
                attachedIngredients[item.ingredientType] = true;
                currentAttachPoint = item.attachPoint;
            }
            else
            {
                item.Attach(transform, currentAttachPoint);
                attachedIngredients[item.ingredientType] = true;
                currentAttachPoint = item.attachPoint;
            }
        }
    }
}