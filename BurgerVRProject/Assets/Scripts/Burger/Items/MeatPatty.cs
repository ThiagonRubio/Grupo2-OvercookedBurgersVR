using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatPatty : BurgerItem
{
    //Necesario para anular el pooleo cuando esté siendo utilizado
    private SpawnableObject _spawnableObject;
    
    private void Start()
    {
        if(_spawnableObject == null) _spawnableObject = GetComponent<SpawnableObject>();
    }

    public override void Attach(Transform parent, Vector3 pos, Quaternion rot)
    {
        base.Attach(parent, pos, rot);
        _spawnableObject.IsAvailable = false;
    }

    private void OnDisable()
    {
        _spawnableObject.IsAvailable = true;
    }

    //Acá también iría la lógica de cocinar la hamburguesa
}
