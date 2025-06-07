using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMethods : MonoBehaviour
{
    private GameObject _gameObject;

    public virtual void CustomAwake() { }
    public virtual void CustomStart() { }
    public virtual void CustomUpdate() { }
    public virtual void CustomFixedUpdate() { }
    public virtual void CustomSetActvie(bool value) { _gameObject.SetActive(value); }
    public virtual bool CustomIsActive()
    {
        return _gameObject.activeInHierarchy;
    }
    public virtual void CustomSetParent(Transform parent)
    {
        _gameObject.transform.SetParent(parent);
    }
    public virtual GameObject CustomGameObject()
    {
        return _gameObject;
    }

}
