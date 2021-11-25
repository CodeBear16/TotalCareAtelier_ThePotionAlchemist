using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionPool : MonoBehaviour
{
    public static List<Potion> potionPool;
    const int maxSize = 100;

    void Start()
    {
        potionPool = new List<Potion>();

    }
}
