using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour, IPoolable
{
    public GameObject GameObject => this.gameObject;

    public virtual void OnPoolableObjectEnable()
    {
        gameObject.SetActive(true);
    }

    public virtual void OnPoolableObjectDisable()
    {
        gameObject.SetActive(false);
    }
}
