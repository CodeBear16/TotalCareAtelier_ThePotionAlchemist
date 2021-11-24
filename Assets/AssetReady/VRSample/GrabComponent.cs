using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabComponent : MonoBehaviour, IGrabable
{
    public Transform Grab(Transform grabber)
    {
        transform.parent = grabber.transform;
        if (GetComponent<Rigidbody>() != null)
            GetComponent<Rigidbody>().isKinematic = true;
        return transform;
    }

    public void Release()
    {
        if (GetComponent<Rigidbody>() != null)
            GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
    }
}
