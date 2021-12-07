using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleCorkPopper : MonoBehaviour
{
    public GameObject cork;
    public GameObject pour;

    public AudioClip popClip;
    public AudioClip putClip;

    private AudioSource source;
    private OVRGrabbable grabbable;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        grabbable = GetComponent<OVRGrabbable>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Keypad0))
        //{
        //    Debug.Log("´©¸§");
        //    cork.SetActive(false);
        //    pour.SetActive(true);
        //    source.PlayOneShot(popClip);
        //}
        if (grabbable.isGrabbed)
        {
            if (cork.activeSelf)
            {
                if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Three))
                {
                    cork.SetActive(false);
                    pour.SetActive(true);
                    source.PlayOneShot(popClip);
                }
            }
        }
        else
        {
            if (cork.activeSelf == false)
            {
                cork.SetActive(true);
                pour.SetActive(false);
                source.PlayOneShot(putClip);
            }
        }
    }
}
