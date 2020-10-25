using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBox : MonoBehaviour
{

    public GameObject box;
    public float posX, posY;
    public GameObject[] waepons;

    void Start()
    {
        StartCoroutine(SpawnWeapons());
    }
    

    public IEnumerator SpawnWeapons()
    {
        yield return new WaitForSeconds(5);
        Vector2 pointSpawn = new Vector2(Random.Range(-posX, posX), Random.Range(-posY, posY));
        if(GameObject.FindGameObjectsWithTag("Box").Length < 8)
        Instantiate(box, pointSpawn, Quaternion.identity);
        if (GameObject.FindGameObjectsWithTag("Waepon").Length < 8)
        Instantiate(waepons[Random.Range(0, waepons.Length)],pointSpawn,Quaternion.identity);

        StartCoroutine(SpawnWeapons());
    }
}
