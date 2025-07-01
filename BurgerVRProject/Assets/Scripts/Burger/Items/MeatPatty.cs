using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatPatty : BurgerItem
{
    private Renderer rend;

    [SerializeField] float cookingTimeRequired = 5f;
    [SerializeField] float burnTimeAfterCooked = 3f;

    private AudioSource cachedAudioSource;
    private float currentCookingTime = 0f;
    private bool isInCookingZone = false;
    [SerializeField] private bool isCooked = false;
    [SerializeField] private bool isBurnt = false;
    [SerializeField] private AudioClip onCookingSuccessfulClip;
    [SerializeField] private AudioClip onCookingUnsuccessfulClip;

    private Coroutine cookingCoroutine;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        cachedAudioSource = GetComponent<AudioSource>();
    }

    public override void Start()
    {
        base.Start();
        canBeUsed = false;
    }

    private IEnumerator CookRoutine()
    {
        currentCookingTime = 0f;

        while (!isBurnt)
        {
            currentCookingTime += Time.deltaTime;

            if (!isCooked && currentCookingTime >= cookingTimeRequired)
            {
                TriggerSuccessfulCooking();
            }

            if (isCooked && currentCookingTime >= cookingTimeRequired + burnTimeAfterCooked)
            {
                TriggerUnsuccessfulCooking();
                yield break;
            }

            yield return null;
        }
    }

    public override void OnPoolableObjectEnable()
    {
        base.OnPoolableObjectEnable();
    }
    public override void OnPoolableObjectDisable()
    {
        Detach();

        if (cookingCoroutine != null)
        {
            StopCoroutine(cookingCoroutine);
            cookingCoroutine = null;
        }

        IsAvailable = true;

        if (rend != null)
            rend.material.SetColor("_BaseColor", Color.white);

        currentCookingTime = 0f;
        isInCookingZone = false;
        isCooked = false;
        isBurnt = false;
        canBeUsed = false;
        base.OnPoolableObjectDisable();
    }
   
    private void TriggerSuccessfulCooking()
    {
        isCooked = true;
        canBeUsed = true;
        IsAvailable = false;
        rend.material.SetColor("_BaseColor", new Color(0.6f, 0.3f, 0.1f));
        cachedAudioSource.PlayOneShot(onCookingSuccessfulClip);
    }
    private void TriggerUnsuccessfulCooking()
    {
        isBurnt = true;
        canBeUsed = false;
        IsAvailable = false;
        rend.material.SetColor("_BaseColor", Color.black);
        cachedAudioSource.PlayOneShot(onCookingUnsuccessfulClip);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CookingZone"))
        {
            isInCookingZone = true;
            if (cookingCoroutine == null)
            {
                cookingCoroutine = StartCoroutine(CookRoutine());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CookingZone"))
        {
            isInCookingZone = false;
            if (cookingCoroutine != null)
            {
                StopCoroutine(cookingCoroutine);
                cookingCoroutine = null;
            }
        }
    }
}
