using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyHealth : MonoBehaviour
{
    private float health = 2;
    
    void Update()
    {
        if (health < 1)
            Destroy(gameObject);
    }


    [PunRPC]
    void destroybullet()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Bullet")
        {
            health--;
            GameObject.FindGameObjectWithTag("Bullet").GetComponent<PhotonView>().RPC("destroybullet", RpcTarget.AllViaServer);
        }
    }
}
