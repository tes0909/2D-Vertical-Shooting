using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonDestroy<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();

        if (instance != this) return;

        if (transform.parent != null)
        {
            transform.SetParent(null);
        }
        DontDestroyOnLoad(gameObject);
    }
}
