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
    private readonly List<IFixedUpdatable> fixedUpdatables = new List<IFixedUpdatable>();
    private readonly List<ILateUpdatable> lateUpdatables = new List<ILateUpdatable>();

    //---------------------------------//

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
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

    private void FixedUpdate()
    {
        foreach (IFixedUpdatable fixedUpdatable in fixedUpdatables)
        {
            fixedUpdatable.OnFixedUpdate();
        }
    }

    private void LateUpdate()
    {
        foreach (ILateUpdatable lateUpdatable in lateUpdatables)
        {
            lateUpdatable.OnLateUpdate();
        }
    }


    //---------------------------------//

    public void Register(object obj)
    {
        if (obj is IUpdatable updatable)
        {
            updatables.Add(updatable);
        }
        if (obj is IFixedUpdatable fixedUpdatable)
        {
            fixedUpdatables.Add(fixedUpdatable);
        }
        if (obj is ILateUpdatable lateUpdatable)
        {
            lateUpdatables.Add(lateUpdatable);
        }
    }

    public void Unregister(object obj)
    {
        if (obj is IUpdatable updatable)
        {
            updatables.Remove(updatable);
        }
        if (obj is IFixedUpdatable fixedUpdatable)
        {
            fixedUpdatables.Remove(fixedUpdatable);
        }
        if (obj is ILateUpdatable lateUpdatable)
        {
            lateUpdatables.Remove(lateUpdatable);
        }
    }
}
