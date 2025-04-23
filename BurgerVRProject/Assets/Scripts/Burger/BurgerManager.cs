using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using UnityEngine.XR.Interaction.Toolkit;

public class BurgerManager : MonoBehaviour
{
    //Attach variables
    public Transform initialAttachPoint;
    public float stackHeight;
    private Dictionary<IngredientType, bool> attachedIngredients = new Dictionary<IngredientType, bool>();
    
    //Bools
    public bool preparingBurger = false;
    public bool burgerReady = false;

    void OnCollisionEnter(Collision collision)
    {
        BurgerItem item = collision.gameObject.GetComponent<BurgerItem>();
        if (item != null)
            HandleCollision(item);
    }

    public void HandleCollision(BurgerItem item)
    {
        if (item == null) return; //No puede pasar porque ya se chequea arriba
        
        //Chequeo, si la hamburguesa no se está preparando el primer item tiene que ser si o si un pan inferior, sino no debe calcular nada más
        if (!preparingBurger && item.ingredientType != IngredientType.PanInferior) return;
        
        //Si el jugador no soltó el item, no calculo nada más
        var grab = item.GetComponent<XRGrabInteractable>();
        if (grab != null && grab.isSelected) return; //Capaz es poco óptimo que esto esté acá
        
        //Si la hamburguesa ya tiene el ingrediente, no calculo nada más
        if (attachedIngredients.ContainsKey(item.ingredientType)) return; //Esto hay que modificarlo porque en un futuro no va a funcionar así
        
        //Crea el vector de posición al que se va a asignar el item
        Vector3 pos = new Vector3(
            initialAttachPoint.position.x,
            initialAttachPoint.position.y + stackHeight,
            initialAttachPoint.position.z
        );

        //Le paso la data para que el item gestione su posición
        item.Attach(transform, pos, initialAttachPoint.rotation);
        stackHeight += item.itemHeight; //Es por acá la idea
        attachedIngredients[item.ingredientType] = true; //Esto no va a funcionar así
        
        if (!preparingBurger && item.ingredientType == IngredientType.PanInferior)
        {
            //La preparación de la hamburguesa empieza siempre por un par inferior
            preparingBurger = true;
            
            //Desactiva sus interacciones y sus físicas dinámicas (Es para que no se rompa, pero si lo podemos evitar sería ideal)
            var g = GetComponent<XRGrabInteractable>();
            if (g != null) g.enabled = false;
            var t = GetComponent<XRGeneralGrabTransformer>();
            if (t != null) t.enabled = false;
            var rb = GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;
        }
        else if (!burgerReady && item.ingredientType == IngredientType.PanSuperior)
        {
            //La preparación de la hamburguesa termina siempre por un pan superior
            burgerReady = true;
            
            //Vuelve a activar sus interacciones y sus físicas dinámicas.
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
        //Reseteo de variables
        stackHeight = 0;
        preparingBurger = false;
        burgerReady = false;
        attachedIngredients.Clear(); //Limpia la lista pero falta desactivar todos los items internos
    }
}