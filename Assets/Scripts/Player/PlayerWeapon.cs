using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : PlayerBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Devil")
        {
            if (time >= 5) GameManager.instance.MonsterUnhappy++;
            else
            {
                MoveCTRLDemo.instance.Die();
                other.gameObject.SetActive(false);
            }
        }
    }
}
