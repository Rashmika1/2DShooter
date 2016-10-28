using UnityEngine;
using System.Collections;

/*
 * Source: Spawner.cs
 * Author:
 * Last Modified by: 
 * Date last Modified: 
 * Program description: Manages the game objects spawning
 */

public class Spawner : MonoBehaviour {
    public GameObject[] gameItensPrefabs;
    public float spawnDelay = 2;
    public float randomFactor = 1;
    public Transform topSpawnPosition;
    public Transform bottonSpawnPosition;

    // Use this for initialization
    void Start () {
        StartSpawn();
	}

    /// <summary>
    /// Stops the spawning
    /// </summary>
    public void StopSpawn() {
        StopCoroutine("SpawnRoutine");
    }

    /// <summary>
    /// Starts the spawn routine
    /// </summary>
    public void StartSpawn() {
        StartCoroutine("SpawnRoutine");
    }
	
    /// <summary>
    /// Corroutine that keeps spawning gameObjects forever
    /// </summary>
    /// <returns></returns>
	IEnumerator SpawnRoutine() {
        while(true) {
            yield return new WaitForSeconds(Random.Range(spawnDelay - randomFactor, spawnDelay + randomFactor));
            Spawn();
        }
    }

    /// <summary>
    /// Spawn a random game object at a random position between the spawn positions
    /// </summary>
    private void Spawn() {
        int enemyIndex = Random.Range(0, gameItensPrefabs.Length);
        GameObject enemyInstance = Instantiate(gameItensPrefabs[enemyIndex]);

        Vector2 enemyPosition = new Vector2(topSpawnPosition.position.x, 
            Random.Range(bottonSpawnPosition.position.y, topSpawnPosition.position.y));

        enemyInstance.transform.position = enemyPosition;
        enemyInstance.SetActive(true);
    }
}
