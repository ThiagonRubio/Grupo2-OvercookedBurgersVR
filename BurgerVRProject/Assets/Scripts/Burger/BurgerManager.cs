using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using UnityEngine.XR.Interaction.Toolkit;

public class BurgerManager : MonoBehaviour
{
    //Necesario para anular el pooleo cuando esté siendo utilizada
    private SpawnableObject _spawnableObject;
    
    //Attach variables
    public Transform initialAttachPoint;
    public float stackHeight;
    // private Dictionary<IngredientType, bool> _attachedIngredients = new Dictionary<IngredientType, bool>();
    public Dictionary<BurgerItem, IngredientType> AttachedIngredients = new Dictionary<BurgerItem, IngredientType>();
    
    //Bools
    public bool preparingBurger = false;
    public bool burgerReady = false;

    private void Start()
    {
        if(_spawnableObject == null) _spawnableObject = GetComponent<SpawnableObject>();
    }

    void OnCollisionEnter(Collision collision)
    {
        BurgerItem item = collision.gameObject.GetComponent<BurgerItem>();
        if (item != null)
            HandleCollision(item);
    }

    public void HandleCollision(BurgerItem item)
    {
        //Chequeo, si la hamburguesa no se está preparando el primer item tiene que ser si o si un pan inferior, sino no debe calcular nada más
        if (!preparingBurger && item.ingredientType != IngredientType.PanInferior) return;
        
        //Si el jugador no soltó el item no calculo nada más
        var grab = item.GetComponent<XRGrabInteractable>();
        if (grab != null && grab.isSelected) return; //Capaz es poco óptimo que esto esté acá
        
        //Si el item no está listo para usarse no calculo nada más.
        if (!item.canBeUsed) return;
        
        //Crea el vector de posición al que se va a asignar el item
        Vector3 pos = new Vector3(
            initialAttachPoint.position.x,
            initialAttachPoint.position.y + stackHeight,
            initialAttachPoint.position.z
        );

        //Le paso la data para que el item gestione su posición
        item.Attach(transform, pos, initialAttachPoint.rotation);
        
        //Actualizo la altura actual de la hamburguesa
        stackHeight += item.itemHeight;
        
        //Trackeo de los items que están en la hamburguesa
        AttachedIngredients.Add(item, item.ingredientType); //Todavía no se hace nada con esto pero sería parte del chequeo de la entrega del pedido
        
        if (!preparingBurger && item.ingredientType == IngredientType.PanInferior)
        {
            //La preparación de la hamburguesa empieza siempre por un par inferior
            preparingBurger = true;
            _spawnableObject.IsAvailable = false;
        }
        else if (!burgerReady && item.ingredientType == IngredientType.PanSuperior)
        {
            //La preparación de la hamburguesa termina siempre por un pan superior
            burgerReady = true;
        }
    }

    private void OnDisable()
    {
        if(_spawnableObject != null)
            _spawnableObject.IsAvailable = true;
    }

    private void OnEnable()
    {
        //Reseteo de variables
        stackHeight = 0;
        preparingBurger = false;
        burgerReady = false;
        AttachedIngredients.Clear(); //Limpia la lista 
    }
}