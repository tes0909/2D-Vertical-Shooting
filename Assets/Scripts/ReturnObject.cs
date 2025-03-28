using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnObject : MonoBehaviour
{
    public ObjectManager.PoolType poolType;

    public void ReturnObj()
    {
        ObjectManager.Instance.ReturnObject(poolType, gameObject);
    }
}
