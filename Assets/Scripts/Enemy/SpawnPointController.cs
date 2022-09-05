using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnPointController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float startSpawnDelay;
    public float spawnDelay;

    

    public float standartHealth;
    float timeBuffer;
    public UnityEvent spawnEnemies; 
    // Start is called before the first frame update
    void Start()
    {
        spawnEnemies.AddListener(SpawnEnemies);
        timeBuffer = startSpawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBuffer<=0)
        {
            timeBuffer = spawnDelay;
            if(spawnEnemies!= null)spawnEnemies.Invoke();
        }
        timeBuffer-= Time.deltaTime;
    }



    void SpawnEnemies()
    {
        GameObject enemy = Instantiate(enemyPrefab,gameObject.transform.position, Quaternion.identity);
        enemy.GetComponent<EnemyController>().health = standartHealth;
    }
}
