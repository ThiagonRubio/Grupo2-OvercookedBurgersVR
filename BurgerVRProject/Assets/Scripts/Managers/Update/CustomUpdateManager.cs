using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomUpdateManager : MonoBehaviour
{
    public static CustomUpdateManager Instance
    {
        get 
        {
            if (instance == null)
            {
                GameObject lazilyCreated = new GameObject("CustomUpdateManager");
                instance = lazilyCreated.AddComponent<CustomUpdateManager>();
            }
            return instance; 
        }
    }
    private static CustomUpdateManager instance;

    private readonly List<IUpdatable> updatables = new List<IUpdatable>();

    //---------------------------------//

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    private void Update()
    {
        foreach (IUpdatable updatable in updatables)
        {
            updatable.OnUpdate();
        }
    }

    //---------------------------------//

    public void Register(object obj)
    {
        if (obj is IUpdatable updatable)
        {
            updatables.Add(updatable);
        }
    }

    public void Unregister(object obj)
    {
        if (obj is IUpdatable updatable)
        {
            updatables.Remove(updatable);
        }
    }
}
