using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDestination : MonoBehaviour
{
    public bool isOccupied = false;

    public void Occupy()
    {
        isOccupied = true;
    }

    public void Leave()
    {
        isOccupied = false;
    }
}
