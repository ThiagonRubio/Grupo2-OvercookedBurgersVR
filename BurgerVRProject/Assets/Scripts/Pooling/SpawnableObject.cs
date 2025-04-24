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

    private void Start()
    {
        poolParent = transform.parent.GetComponent<ItemDispenser>();
    }

    public virtual void OnPoolableObjectEnable()
    {
        transform.position = poolParent.ItemSpawnPoint.transform.position;
        gameObject.SetActive(true);
    }

    public virtual void OnPoolableObjectDisable()
    {
        transform.SetParent(poolParent.transform);
        gameObject.SetActive(false);
    }
}
