using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RayPointer : MonoBehaviour
{
    public Transform rayOrigin;
    private RaycastHit hit;
    public float rayDistance = 50f;

    private OVRGrabber hand;
    public GameObject potion;
    public GameObject currentObject;
    private Outline tempLine;

    private void Start()
    {
        hand = PlayerBehaviour.instance.gameObject.GetComponent<OVRGrabber>();
    }

    private void Update()
    {
        Debug.DrawRay(rayOrigin.position, rayOrigin.forward, Color.yellow, 0.1f);

        if (hand.grabbedObject != null)
        {
            if (hand.grabbedObject.CompareTag("Potion"))
            {
                potion = hand.grabbedObject.gameObject;

                if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, rayDistance))
                {
                    if (currentObject != hit.collider.gameObject)
                        if (currentObject.GetComponent<Outline>() != null)
                            currentObject.GetComponent<Outline>().OutlineWidth = 0;

                    currentObject = hit.collider.gameObject;

                    if (currentObject.GetComponent<Outline>() != null)
                    {
                        tempLine = currentObject.GetComponent<Outline>();

                        if (tempLine.OutlineWidth != 5)
                            tempLine.OutlineWidth = 5;

                        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.One))
                        {
                            Debug.Log(currentObject.name + "¾à¹° °Ç³Û");
                            currentObject.GetComponent<MonsterState>().TakePotion(potion);
                        }
                    }
                }
            }
            else
                potion = null;
        }
    }
}