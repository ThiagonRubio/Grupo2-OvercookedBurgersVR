using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KetchupBottle : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private GameObject ketchupProjectile;
    [SerializeField] private Transform bottlePoint;

    public void Use()
    {
        GameObject spawnedProjectile = Instantiate(ketchupProjectile, bottlePoint.position, bottlePoint.rotation);
        spawnedProjectile.GetComponent<Rigidbody>().velocity = projectileSpeed * bottlePoint.forward;
        Destroy(spawnedProjectile, 5f);
    }
}
