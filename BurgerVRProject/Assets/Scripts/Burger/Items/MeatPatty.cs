using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatPatty : BurgerItem, IUpdatable
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
    [SerializeField] private AudioClip whileBeingCookedClip;

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

    // TODO: Repensar - por ahora lo optimizo un poco haciendo que se salga de la lista de updatables cuando termine de cocinarse
    public void OnUpdate()
    {
        if (isBurnt)
            return;

        if (isInCookingZone)
        {
            currentCookingTime += Time.deltaTime;

            if (!isCooked && currentCookingTime >= cookingTimeRequired)
            {
                TriggerSuccessfulCooking();
            }

            if (isCooked && currentCookingTime >= cookingTimeRequired + burnTimeAfterCooked)
            {
                TriggerSuccessfulCooking();
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
    private void TriggerSuccessfulCooking()
    {
        isCooked = true;
        canBeUsed = true;
        rend.material.SetColor("_BaseColor", new Color(0.6f, 0.3f, 0.1f));

        cachedAudioSource.Stop();
        cachedAudioSource.clip = onCookingSuccessfulClip;
        cachedAudioSource.loop = false;
        cachedAudioSource.PlayOneShot(onCookingSuccessfulClip);

        CustomUpdateManager.Instance.Unregister(this);
    }
    private void TriggerUnsuccessfulCooking()
    {
        isBurnt = true;
        canBeUsed = false;
        rend.material.SetColor("_BaseColor", Color.black);

        cachedAudioSource.Stop();
        cachedAudioSource.clip = onCookingSuccessfulClip;
        cachedAudioSource.loop = false;
        cachedAudioSource.PlayOneShot(onCookingUnsuccessfulClip);

        CustomUpdateManager.Instance.Unregister(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CookingZone"))
        {
            isInCookingZone = true;

            cachedAudioSource.clip = whileBeingCookedClip;
            cachedAudioSource.loop = true;
            cachedAudioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CookingZone"))
        {
            isInCookingZone = false;
            cachedAudioSource.Stop();
        }
    }
}
