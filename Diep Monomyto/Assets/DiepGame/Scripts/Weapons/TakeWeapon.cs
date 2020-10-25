using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeWeapon : MonoBehaviour
{
    public Weapons weapon;

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            target.GetComponent<PlayerControl>().currentWeapon = weapon;
            Destroy(gameObject);
        }
    }
}
