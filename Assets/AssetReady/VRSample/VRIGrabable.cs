using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface VRIGrabable
{
    public Transform Grab(Transform grabber);
    public void Release();
}
