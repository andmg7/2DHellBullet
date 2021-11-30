using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // [SerializeField] []
    [SerializeField]
    private GameObject enemyShipPrefab;

    [SerializeField]
    private GameObject[] powerUps;

    private GameManager _gameManager; 

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(EnemySpawner());
        StartCoroutine(PowerUpSpawner());
    }

     public void StartSpawnRoutine()
    {
        StartCoroutine(EnemySpawner());
        StartCoroutine(PowerUpSpawner());
    }

    //create a corutine to spawn  the enemy every 5 seconds

    IEnumerator EnemySpawner()
    {
        while (_gameManager.gameOver == false)
        {
            Instantiate(enemyShipPrefab, new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);

            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator PowerUpSpawner()
    {
        while (_gameManager.gameOver == false)
        {
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(powerUps[randomPowerUp], new Vector3(Random.Range(-7f, 7f), 7f, 0), Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
        
    }

}
