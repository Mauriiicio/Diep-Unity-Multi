using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class PlayerControl : MonoBehaviour
{
    public float Speed = 5f;
    public float HealthMax = 100f;
    public Weapons currentWeapon;
    public GameObject SpawnbBullet;
    public GameObject Bullet;

    public Image img_helthFill;
    public Text txt_namePlayer;

    float CurrentHealth;

    private Rigidbody2D BodyPlayer;
    private Vector2 moveVelocity;
    private float nextFire = 0;

    PhotonView photon;

    void Awake()
    {
        BodyPlayer = GetComponent<Rigidbody2D>();
        photon = GetComponent<PhotonView>();
        txt_namePlayer.text = photon.Owner.NickName;
        Health(HealthMax);
    }
    private void Update()
    {
        if (photon.IsMine)
        {
            Rotation();
        }
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -3.20f, 3.20f), Mathf.Clamp(transform.position.y, -2.35f, 2.35f));
        
    }
    
    void FixedUpdate()
    {
        if (photon.IsMine)
        {
            shoot();
            Movement();
            
        }
    }


    void shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time >= nextFire)
            {
                currentWeapon.shoot();
                nextFire = Time.time + 1 / currentWeapon.firerate;
            }
            
        }
    }

    [PunRPC]
    void shootRPC()
    {
           Instantiate(Bullet, SpawnbBullet.transform.position, SpawnbBullet.transform.rotation);
    }


    public void damage(float damagevalue)
    {
        photon.RPC("damagePhoton",RpcTarget.AllBuffered, damagevalue);
    }
    [PunRPC]
    void damagePhoton(float damagevalue)
    {
        Health(damagevalue);

        if(CurrentHealth <= 0)
        {

            Application.Quit();
        }
    }

    void Health(float healthValue)
    {
        CurrentHealth += healthValue;
        img_helthFill.fillAmount = CurrentHealth/100f;
    }

    [PunRPC]
    void GameOver()
    {
        if (photon.IsMine)
        {
            Destroy(this.gameObject);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
    }

    void Rotation()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + (-90);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);
    }


    void Movement()
    {
        var InputsMove = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = InputsMove.normalized * Speed;
        BodyPlayer.MovePosition(BodyPlayer.position + moveVelocity * Time.fixedDeltaTime);
    }
}
