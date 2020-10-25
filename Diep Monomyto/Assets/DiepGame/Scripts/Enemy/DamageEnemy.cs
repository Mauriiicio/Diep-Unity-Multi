using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DamageEnemy : MonoBehaviour
{
    public float damageEnemy = 10f;
   
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            target.GetComponent<PlayerControl>().damage(-damageEnemy);
        }
    }
}
