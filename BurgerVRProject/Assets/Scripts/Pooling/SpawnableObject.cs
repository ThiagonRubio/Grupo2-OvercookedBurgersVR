using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour, IPoolable
{
    public GameObject GameObject => this.gameObject;
    public Transform poolParent;
    
    private void Start()
    {
        poolParent = transform.parent;
    }

    public virtual void OnPoolableObjectEnable()
    {
        gameObject.SetActive(true);
    }

    public virtual void OnPoolableObjectDisable()
    {
        transform.SetParent(poolParent);
        gameObject.SetActive(false);
    }
}
