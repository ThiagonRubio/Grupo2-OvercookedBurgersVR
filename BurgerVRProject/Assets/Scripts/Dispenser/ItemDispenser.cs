using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDispenser : MonoBehaviour
{
    [SerializeField] private GameObject itemToSpawn;
    [SerializeField] private Transform spawnPosition;

    public void SpawnItem()
    {
        //Falta toda la lógica de pool
        Instantiate(itemToSpawn, spawnPosition.position, spawnPosition.rotation);
    }
}
