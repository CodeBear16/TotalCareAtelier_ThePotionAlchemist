using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Potion : MonoBehaviour
{
    public string symptom;
    public string potionName;
    public AudioClip breakSound;
    private AudioSource source;
    public GameObject particle;

    private void OnEnable()
    {
        source = GetComponent<AudioSource>();
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            source.PlayOneShot(breakSound);
            particle.SetActive(true);
            Destroy(gameObject, 2);
        }
        if (collision.gameObject.CompareTag("Monster"))
        {
            if (GetComponent<OVRGrabbable>().isGrabbed == false)
                collision.gameObject.GetComponent<MonsterState>().TakePotion(gameObject);
        }
    }
}
