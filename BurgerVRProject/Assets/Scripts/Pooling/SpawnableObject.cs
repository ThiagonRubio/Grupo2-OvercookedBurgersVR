using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class SpawnableObject : MonoBehaviour, IPoolable
{
    public bool IsAvailable { get { return isAvailable; } set { isAvailable = value; } }

    private Transform parentTransform;
    private bool isAvailable = true;

    public virtual void OnPoolableObjectEnable()
    {
        if (parentTransform == null) parentTransform = transform.parent.transform;
        gameObject.SetActive(true);
    }

    public virtual void OnPoolableObjectDisable()
    {
        transform.SetParent(parentTransform); //Necesario para los items cuyo parent cambia en runtime
        gameObject.SetActive(false);
    }

    public void SetPoolablePositionAndRotation(Transform newTransform)
    {
        transform.SetPositionAndRotation(newTransform.position, newTransform.rotation); 
    }
    
    //Tiene que tener acceso a su pool para asignarla de parent
    //El item dispenser le tiene que decir su nueva posición al spawnear a través del método de la interfaz
}
