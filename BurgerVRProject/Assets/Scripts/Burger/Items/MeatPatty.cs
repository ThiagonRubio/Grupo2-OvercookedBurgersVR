using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatPatty : BurgerItem
{
    //Necesario para anular el pooleo cuando esté siendo utilizado
    private SpawnableObject _spawnableObject;

    private Renderer rend;

    [SerializeField] float cookingTimeRequired = 5f;
    [SerializeField] float burnTimeAfterCooked = 3f;

    private float currentCookingTime = 0f;
    private bool isInCookingZone = false;
    [SerializeField] private bool isCooked = false;
    [SerializeField] private bool isBurnt = false;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public override void Start()
    {
        base.Start();
        canBeUsed = false;
        if (_spawnableObject == null) _spawnableObject = GetComponent<SpawnableObject>();
    }

    void Update()
    {
        if (isBurnt)
            return;

        if (isInCookingZone)
        {
            currentCookingTime += Time.deltaTime;

            if (!isCooked && currentCookingTime >= cookingTimeRequired)
            {
                isCooked = true;
                canBeUsed = true;
                rend.material.SetColor("_BaseColor", new Color(0.6f, 0.3f, 0.1f));
                Debug.Log($"{gameObject.name} is cooked!");
            }

            if (isCooked && currentCookingTime >= cookingTimeRequired + burnTimeAfterCooked)
            {
                isBurnt = true;
                canBeUsed = false;
                rend.material.SetColor("_BaseColor", Color.black);
                Debug.Log($"{gameObject.name} is burnt!");
            }
        }
    }

    public override void Attach(Transform parent, Vector3 pos, Quaternion rot)
    {
        base.Attach(parent, pos, rot);
        _spawnableObject.IsAvailable = false;
    }

    private void OnDisable()
    {
        Detach();
        
        if(_spawnableObject != null)
            _spawnableObject.IsAvailable = true;
        
        if(rend!= null)
            rend.material.SetColor("_BaseColor", Color.white);
        
        currentCookingTime = 0f;
        isInCookingZone = false; 
        isCooked = false;
        isBurnt = false;
        canBeUsed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CookingZone"))
        {
            isInCookingZone = true;
            Debug.Log($"{gameObject.name} has entered the CookingZone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CookingZone"))
        {
            isInCookingZone = false;
            Debug.Log($"{gameObject.name} has exit the CookingZone");
        }
    }
}
