using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class EnemySpawn : MonoBehaviour
{

    public GameObject[] enemies;
    public Vector2 spawnWait;
    
    public int enemySpwanMax;
    public float enemySpwanMin;
    public float TimetoSpwan;
    public float TimetoSpwanMin;
    public Boundary bound;


    private bool gameover = false;
    private int enemyCount = 1;



    void Start()
    {
        StartCoroutine(SpwanEnemy());
    }

    
    void Update()
    {
        
    }
    IEnumerator SpwanEnemy()
    {
        yield return new WaitForSeconds(1);
        while (!gameover)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                GameObject enemy = enemies[Random.Range(0, enemies.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(bound.xMin, bound.xMax), bound.yMin, 0);
                Instantiate(enemy, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(spawnWait.x, spawnWait.y));
            }

            enemyCount++;
            if (enemyCount >= enemySpwanMax)
                enemyCount = enemySpwanMax;

            spawnWait.x -= 0.1f;
            spawnWait.y -= 0.1f;

            if (spawnWait.y <= enemySpwanMin)
                spawnWait.y = enemySpwanMin;

            if (spawnWait.x <= enemySpwanMin)
                spawnWait.x = enemySpwanMin;
            yield return new WaitForSeconds(TimetoSpwan);
            TimetoSpwan -= 0.1f;
            if (TimetoSpwan <= TimetoSpwanMin)
                TimetoSpwan = TimetoSpwanMin;
        }
    }
}
