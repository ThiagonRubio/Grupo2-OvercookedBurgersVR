using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : SpawnableObject
{
    private void OnTriggerEnter(Collider other)
    {
        OnPoolableObjectDisable();
    }
}
