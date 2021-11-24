using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabable
{
    public Transform Grab(Transform grabber);
    public void Release();
}
