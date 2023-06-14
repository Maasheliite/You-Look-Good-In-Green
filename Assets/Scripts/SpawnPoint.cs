using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public EnemyController[] enemyPrefabs;
    public float spawnAreaRadius;
    public float enabledRadius;

    public float spawnInterval = 1f;
    public int maxMonsterInArea = 3;

    private Transform hero;
    private List<GameObject> currentEnemyList = new List<GameObject>();
    void Start()
    { 
    
        hero = FindAnyObjectByType<PlayerController>().transform;

        StartCoroutine(this.SpawnMonster());
    }

    IEnumerator SpawnMonster()
    {
        while (true)
        {
            if (currentEnemyList.Count < 3 && (transform.position - hero.position).magnitude <= enabledRadius)
            {// this might actually not work. i dont know what happens to the element in the list if the enemy gets killed.

                //pick a random prefab from the list.
                EnemyController randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

                //calculate the random position around this gameobject with the spawn area radius
                Vector2 randomPositionInRadiusAroundGameObject = transform.position + (Vector3)(Random.insideUnitCircle * spawnAreaRadius);

                //creates new Enemy GameObject from the prefab at the random position with a default rotation.
                GameObject newEnemy = Instantiate(randomEnemyPrefab.gameObject, randomPositionInRadiusAroundGameObject, Quaternion.identity);

                currentEnemyList.Add(newEnemy);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
   

 
    void Update()
    {

    }
}
