using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCaller : Singleton<SpawnerCaller>
{
    public GameObject child;

    public void OffSpawner()
    {
        child.SetActive(false);
    }

    public void OnSpawner()
    {
        child.SetActive(true);
    }
}
