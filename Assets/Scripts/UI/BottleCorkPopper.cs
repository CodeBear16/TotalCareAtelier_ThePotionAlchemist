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
        //    Debug.Log("����");
        //    cork.SetActive(false);
        //    pour.SetActive(true);
        //    source.PlayOneShot(popClip);
        //}
        if (grabbable.isGrabbed)
        {
            if (cork.activeSelf)
            {
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    cork.SetActive(false);
                    pour.SetActive(true);
                    source.PlayOneShot(popClip);
                    OVRInput.SetControllerVibration(0.5f, 0.5f, 5.5f, OVRInput.Controller.RTouch);
                }
                else if (OVRInput.GetDown(OVRInput.Button.Three))
                {
                    cork.SetActive(false);
                    pour.SetActive(true);
                    source.PlayOneShot(popClip);
                    OVRInput.SetControllerVibration(0.5f, 0.5f, 5.5f, OVRInput.Controller.LTouch);
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
