using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadEnding : MonoBehaviour
{
    public GameObject hero;
    Animator animator;

    private void Start()
    {
        animator = hero.GetComponent<Animator>();
    }

    public void BadEndingEvent()
    {
        PlayerBehaviour.instance.gameObject.SetActive(false);
        FindObjectOfType<DoorOpen>().Open();
        hero.SetActive(true);
        animator.SetBool("Attack", true);
    }
}
