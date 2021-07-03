using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject cannon;
    public SpawnRate enemySpawnRate = SpawnRate.NORMAL;
    public float normalSpawnRate = 10f;
    public float highSpawnRate = 5f;
    public float extremeSpawnRate = 2f;
    private float nextSpawnTime = -1;
    public enum SpawnRate{
        NONE,
        NORMAL,
        HIGH,
        EXTREME
    }

    void Update()
    {
        if(enemySpawnRate != SpawnRate.NONE && GameController.instance.gameStarted) spawnEnemy();
    }
    void spawnEnemy(){
        float usedSpawnRate = normalSpawnRate;
        if(enemySpawnRate == SpawnRate.HIGH) usedSpawnRate = highSpawnRate;
        if(enemySpawnRate == SpawnRate.EXTREME) usedSpawnRate = extremeSpawnRate;
        if(nextSpawnTime<Time.time || nextSpawnTime == -1){
            foreach(Transform child in transform){
                var enemy = Instantiate(cannon,child.position,Quaternion.identity);
                enemy.GetComponent<CannonEnemy>().setDirection(child.gameObject.GetComponent<SpawnDirection>().dir);
            }
            nextSpawnTime = Time.time+usedSpawnRate;
        }        
    }
    public void setEnemySpawnRate(SpawnRate newSpawnRate){
        nextSpawnTime = Time.time;
        enemySpawnRate = newSpawnRate;
    }
}
