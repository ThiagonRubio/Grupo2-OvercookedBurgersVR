using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour, IPoolable
{
    public GameObject GameObject => this.gameObject;
    public bool CanBePooled { get { return canBePooled; } set { canBePooled = value; } }

    public ItemDispenser poolParent;
    private bool canBePooled = true;

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
