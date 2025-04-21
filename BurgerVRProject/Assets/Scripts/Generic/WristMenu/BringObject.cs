using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringObject : MonoBehaviour
{
    //Este script es placeholder por el momento
    [SerializeField] private GameObject tool;
    
    public void BringToolToPlayer(Transform playerHand)
    {
        tool.transform.position = playerHand.transform.position;
    }
}
