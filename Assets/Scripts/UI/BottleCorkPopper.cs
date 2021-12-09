using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BottleCorkPopper : MonoBehaviour
{
    public GameObject cork;
    public GameObject pour;

    [Tooltip("0: 마개 뽑는 소리\n1: 마개 꽂는 소리")]
    public AudioClip[] corkClips;

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
        //    Debug.Log("누름");
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
                    CorkWork(true);
                    OVRInput.SetControllerVibration(0.5f, 0.5f, 0.5f, OVRInput.Controller.RTouch);
                }
                else if (OVRInput.GetDown(OVRInput.Button.Three))
                {
                    CorkWork(true);
                    OVRInput.SetControllerVibration(0.5f, 0.5f, 0.5f, OVRInput.Controller.LTouch);
                }
            }
        }
        else
        {
            if (cork.activeSelf == false)
                CorkWork(false);
        }
    }

    private void CorkWork(bool isActive)
    {
        cork.SetActive(!isActive);
        pour.SetActive(isActive);
        source.PlayOneShot(corkClips[Convert.ToInt32(!isActive)]);
    }
}
