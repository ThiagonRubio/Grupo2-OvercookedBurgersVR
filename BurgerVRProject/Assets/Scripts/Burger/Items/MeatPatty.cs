using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatPatty : BurgerItem, IUpdatable
{
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
    }

    public void OnUpdate()
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
            }

            if (isCooked && currentCookingTime >= cookingTimeRequired + burnTimeAfterCooked)
            {
                isBurnt = true;
                canBeUsed = false;
                rend.material.SetColor("_BaseColor", Color.black);
            }
        }
    }

    public override void OnPoolableObjectEnable()
    {
        base.OnPoolableObjectEnable();
        CustomUpdateManager.Instance.Register(this);
    }
    public override void OnPoolableObjectDisable()
    {
        Detach();

        IsAvailable = true;

        if (rend != null)
            rend.material.SetColor("_BaseColor", Color.white);

        currentCookingTime = 0f;
        isInCookingZone = false;
        isCooked = false;
        isBurnt = false;
        canBeUsed = false;
        CustomUpdateManager.Instance.Unregister(this);
        base.OnPoolableObjectDisable();
    }

    public override void Attach(Transform parent, Vector3 pos, Quaternion rot)
    {
        base.Attach(parent, pos, rot);
        IsAvailable = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CookingZone"))
        {
            isInCookingZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CookingZone"))
        {
            isInCookingZone = false;
        }
    }
}
