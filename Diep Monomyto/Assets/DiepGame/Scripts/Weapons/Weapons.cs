using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
 public class Weapons : ScriptableObject
{
    
    public GameObject BulletsPrefab;
    public float firerate = 1;
    public int damage = 20;

    public void shoot()
    {
       PhotonNetwork.Instantiate(BulletsPrefab.name, GameObject.Find("SpawnBullet").transform.position, Quaternion.identity, 0);
    }

}