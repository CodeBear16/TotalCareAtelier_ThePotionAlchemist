using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RayPointer : MonoBehaviour
{
    public Transform pointer;
    private RaycastHit hit;
    private float distance = 50f;
    private LayerMask layer;

    private OVRGrabber hand;
    public GameObject potion;
    public GameObject monster;
    private Outline tempLine;

    private void Start()
    {
        layer = 1 << LayerMask.NameToLayer("Monster");
        hand = GetComponent<OVRGrabber>();
    }

    private void Update()
    {
        Debug.DrawRay(pointer.position, pointer.forward, Color.yellow, 0.1f);

        potion = TakeGrabbed();

        if (Physics.Raycast(pointer.position, pointer.forward, out hit, distance, layer))
        {
            monster = hit.collider.gameObject;
            tempLine = monster.GetComponent<Outline>();

            ShowOutline(tempLine);

            if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Three))
            {
                GivePotion(hand.grabbedObject, monster.GetComponent<MonsterState>());
            }
        }
        else
        {
            HideOutline(tempLine);
            tempLine = null;
            monster = null;
            potion = null;
        }
    }

    private GameObject TakeGrabbed()
    {
        if (hand.grabbedObject != null)
        {
            if (hand.grabbedObject.CompareTag("Potion"))
                return hand.grabbedObject.gameObject;
            else return null;
        }
        else return null;
    }

    private void GivePotion(OVRGrabbable potion, MonsterState monster)
    {
        if (potion != null)
        {
            hand.ForceRelease(potion);
            //potion.GrabEnd(Vector3.zero, Vector3.zero);
            potion.GetComponent<Rigidbody>().velocity = Vector3.zero;
            potion.GetComponent<Rigidbody>().useGravity = false;
            monster.TakePotion(potion.gameObject);
        }
        else return;
    }

    private void ShowOutline(Outline line)
    {
        if (line != null)
            line.OutlineWidth = 5;
    }

    private void HideOutline(Outline line)
    {
        if (line != null)
            line.OutlineWidth = 0;
    }
}