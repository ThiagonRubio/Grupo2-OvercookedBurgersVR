using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    GameObject GameObject { get; }
    bool IsAvailable { get; set; }
    void OnPoolableObjectEnable();
    void OnPoolableObjectDisable();
    void SetPoolablePositionAndRotation(Transform newTransform);
}
