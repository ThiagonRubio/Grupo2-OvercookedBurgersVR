using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerCooking : MonoBehaviour
{
    [SerializeField] float cookingTimeRequired = 5f;
    private float currentCookingTime = 0f;
    private bool isInCookingZone = false;
    
    void Update()
    {
        if (isInCookingZone)
        {
            currentCookingTime += Time.deltaTime;

            if (currentCookingTime >= cookingTimeRequired)
            {
                isInCookingZone = false;
                //Agregar que se queme la hamburguesa si se queda mas tiempo
            }
        }
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
