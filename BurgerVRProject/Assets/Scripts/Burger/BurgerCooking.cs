using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerCooking : MonoBehaviour
{
    [SerializeField] float cookingTimeRequired = 5f;
    [SerializeField] float burnTimeAfterCooked = 3f;

    private float currentCookingTime = 0f;
    private bool isInCookingZone = false;
    private bool isCooked = false;
    private bool isBurnt = false;
    
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
                Debug.Log($"{gameObject.name} is cooked!");
            }

            if (isCooked && currentCookingTime >= cookingTimeRequired + burnTimeAfterCooked)
            {
                isBurnt = true;
                Debug.Log($"{gameObject.name} is burnt!");
            }
        }
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
