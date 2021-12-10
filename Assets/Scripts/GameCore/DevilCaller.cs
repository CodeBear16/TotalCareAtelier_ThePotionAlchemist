using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilCaller : Singleton<DevilCaller>
{
    public GameObject child;

    public void OffDevil()
    {
        child.SetActive(false);
    }

    public void OnDevil()
    {
        child.SetActive(true);
    }
}
