using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour, IPoolable
{
    public GameObject GameObject => this.gameObject;
    public bool IsAvailable { get { return isAvailable; } set { isAvailable = value; } }

    public ItemDispenser poolParent;
    private bool isAvailable = true;
    
    public virtual void OnPoolableObjectEnable()
    {
        if (poolParent == null) poolParent = transform.parent.GetComponent<ItemDispenser>(); 
        transform.position = poolParent.ItemSpawnPoint.position; //Se ejecutaba antes del start y daba error, por eso geteo el componente en el método
        gameObject.SetActive(true);
    }

    public virtual void OnPoolableObjectDisable()
    {
        transform.SetParent(poolParent.transform);
        gameObject.SetActive(false);
    }
}
