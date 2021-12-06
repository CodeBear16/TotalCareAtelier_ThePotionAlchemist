using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T instance = null;

    public virtual void Awake()
    {
        if (instance == null)
            instance = (T)this;
        else
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }
}
