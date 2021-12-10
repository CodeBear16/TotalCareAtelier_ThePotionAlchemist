using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Devil")
        {
            if (PlayerBehaviour.instance.time >= 5)
            {
                GameManager.instance.MonsterUnhappy++;
            }
            else
            {
                MoveCTRLDemo.instance.GetHit();
            }
        }
    }
}
