using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    public GameObject EnemyShipPrefab;
    public GameObject[] Powerups;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(Spawn());
        StartCoroutine(PowerupsSpawn());
        
    }

    public void StartSpawnRoutine()
    {
        StartCoroutine(Spawn());
        StartCoroutine(PowerupsSpawn());
    }

    public IEnumerator Spawn()
    {

        while (_gameManager.gameOver == false)
        {
            float randomX = Random.Range(-7.0f, 7.0f);
            transform.position = new Vector3(randomX, 7.0f, 0);
            Instantiate(EnemyShipPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
            
        }
      
        
        
    }

    public IEnumerator PowerupsSpawn()
    {
        while (_gameManager.gameOver == false)
        {
            int randomPowerup = Random.Range(0, 3);
            float randomX = Random.Range(-7.0f, 7.0f);
            transform.position = new Vector3(randomX, 7.0f, 0);
            Instantiate(Powerups[randomPowerup], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(6.0f);
        }
        
    }


}
