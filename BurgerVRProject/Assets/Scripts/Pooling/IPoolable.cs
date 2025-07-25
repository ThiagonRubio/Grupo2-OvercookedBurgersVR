using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    bool IsAvailable { get; set; }
    void OnPoolableObjectEnable();
    void OnPoolableObjectDisable();
    void SetPoolablePositionAndRotation(Transform newTransform);
}
