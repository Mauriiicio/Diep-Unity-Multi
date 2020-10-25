using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ControllerBullet : MonoBehaviour
{
    public float Speed = 1f;
    public float DestroyBullet = 5f;
    public float damagebullet = 10f;
    


    private Vector2 dir;

    Rigidbody2D BodyBullet;
    float bulletCount;

    // Start is called before the first frame update
    void Start()
    {

        BodyBullet = GetComponent<Rigidbody2D>();

        dir = GameObject.Find("SpawnBullet").transform.position;
        transform.position = GameObject.Find("CannonSpawn").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, dir, Speed * Time.deltaTime);
        if (bulletCount >= DestroyBullet)
        {
            Destroy(this.gameObject);
        }
        bulletCount += Time.deltaTime;

    }
    
    [PunRPC]
    void destroybullet()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player" && target.GetComponent<PlayerControl>()&& target.GetComponent<PhotonView>().IsMine)
        {
            target.GetComponent<PlayerControl>().damage(-damagebullet);
            this.GetComponent<PhotonView>().RPC("destroybullet", RpcTarget.AllViaServer);
        }

        if (target.tag == "Box")
        {
            Destroy(target.gameObject);
            this.GetComponent<PhotonView>().RPC("destroybullet", RpcTarget.AllViaServer);
            
        }
    }
}
