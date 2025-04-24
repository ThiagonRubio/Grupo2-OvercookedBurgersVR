using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TestCubePoolableObject : MonoBehaviour, IPoolable
{
    // Esto es un objeto pooleable random para meter y probar los pools, por si no se dieron cuenta.
    public GameObject GameObject => this.gameObject;
    public bool CanBePooled { get { return canBePooled; } set { canBePooled = value; } }

    private bool canBePooled = true;
    private Vector3 spawnPoint = new Vector3(0, 0, 0); 
    // Totalmente arbitrario porque si - despues implementan la logica que se les canta la cola DENTRO DEL OBJETO

    //-----------------------METHODS----------------------------------

    private void OnBecameInvisible()
    {
        OnPoolableObjectDisable();
    }

    public void OnPoolableObjectEnable()
    {
        // Porque para ver bien el ejemplo, las cajas tienen gravedad y si no les resteamos la velocidad, la prox que los activamos
        // conservan la vel de caida que tenian antes. Para que vean la clase de logica que no se te puede pasar por alto cuando usas pools.
        // PS: por el amor de dios cachean el component en un awake todo esto es prueba rapida nomas.
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        gameObject.transform.localPosition = spawnPoint;
        gameObject.SetActive(true);
    }
    public void OnPoolableObjectDisable()
    {
        gameObject.SetActive(false);
    }

}
