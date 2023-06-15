using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public List<GameObject> prefabs;
    public float spawnAreaRadius;

    public float spawnInterval = 1f;
    public int maxObjectsInArea = 3;

    private List<GameObject> currentObjectList = new List<GameObject>();
    void Start()
    {
        StartCoroutine(this.SpawnObject());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1);

        UnityEditor.Handles.color = Color.red;
        Vector3 direction = new Vector3(0, 0, 1);
        UnityEditor.Handles.DrawWireDisc(transform.position, direction, spawnAreaRadius);
    }
    IEnumerator SpawnObject()
    {
        while (true)
        {
            if (currentObjectList.Count < maxObjectsInArea)
            {
                GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Count)];

                Vector2 randomPositionInRadiusAroundGameObject = transform.position + (Vector3)(Random.insideUnitCircle * spawnAreaRadius);

                GameObject newbject = Instantiate(randomPrefab.gameObject, randomPositionInRadiusAroundGameObject, Quaternion.identity);

                currentObjectList.Add(newbject);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
   
    void FixedUpdate()
    {
        List<GameObject> objectsToRemove = new List<GameObject>();

        foreach (GameObject obj in currentObjectList)
        {
            if (!obj.activeSelf)
            {
                objectsToRemove.Add(obj);
            }
        }

        foreach (GameObject obj in objectsToRemove)
        {
            currentObjectList.Remove(obj);
        }
    }
}
