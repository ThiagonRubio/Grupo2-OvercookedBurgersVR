using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour, IPoolable
{
    public GameObject GameObject => this.gameObject;
    
    

    public void OnPoolableObjectEnable()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        gameObject.SetActive(true);
    }

    public void OnPoolableObjectDisable()
    {
        gameObject.SetActive(false);
    }
}
