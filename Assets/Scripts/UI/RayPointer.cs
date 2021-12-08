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

    public GameObject potion;
    public GameObject monster;

    private void Update()
    {
        Debug.DrawRay(rayOrigin.position, rayOrigin.forward, Color.yellow, 0.1f);

        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, rayDistance))
        {
            //Outline temp = hit.collider.TryGetComponent<Outline>(out Outline outline);
            if (hit.collider.CompareTag("Monster"))
            {
                if (hit.collider.GetComponent<Outline>().OutlineWidth != 5)
                    hit.collider.GetComponent<Outline>().OutlineWidth = 5;
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.One))
                {
                    Debug.Log(hit.collider.name + "¾à¹° °Ç³Û");
                    hit.collider.GetComponent<MonsterState>().TakePotion(hit.collider.gameObject);
                }
            }
            else
            {
                if (hit.collider.GetComponent<Outline>().OutlineWidth != 0)
                    hit.collider.GetComponent<Outline>().OutlineWidth = 0;
            }
        }
    }
}