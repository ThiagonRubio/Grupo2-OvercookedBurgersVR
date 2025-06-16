using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BringObject : MonoBehaviour
{
    [SerializeField] private string toolTag;
    private GameObject _tool;

    private void Start()
    {
        _tool = GameObject.FindGameObjectWithTag(toolTag);
    }

    public void BringToolToPlayer(Transform playerHand)
    {
        _tool.transform.position = playerHand.transform.position;
    }
}
