using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject ghostPrefab;
    [SerializeField] GameObject greenGhostPrefab;
    [SerializeField] GameObject harpyPrefab;
    [SerializeField] GameObject lifeUpPrefab;

    [SerializeField] Vector3[] spawnPoints = new Vector3[3];

    private float harpySpawn = 5.8f;
    private float spawnTimeHarpy;
    [SerializeField] float spawnTimeGhost;

    private GameManager gameManager;

    [SerializeField] float initialSpawnrateGhost = 7f;
    [SerializeField] float minSpawnrateGhost = 1f;
    [SerializeField] float initialSpawnrateHarpy = 10f;
    [SerializeField] float minSpawnrateHarpy = 3f;
    [SerializeField] float difficultyScale = 2f;
    private float gameTimerStart = 0;
    private float ghostOffset;
    void Start()
    {
        spawnTimeGhost = initialSpawnrateGhost;
        spawnTimeHarpy = initialSpawnrateHarpy;
        gameTimerStart = Time.time;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        StartCoroutine(SpawnGhostRoutine());
        StartCoroutine(SpawnHarpyRoutine());
        StartCoroutine(SpawnLifeUpRoutine());
    }
    IEnumerator SpawnGhostRoutine()
    {
        while (ghostPrefab == true && gameManager.isGameActive)
        {
            Vector3 spawnPos = Vector3.zero;
            Vector3 secondSpawnPos = Vector3.zero;

            spawnTimeGhost = initialSpawnrateGhost - ((Time.time - gameTimerStart) * difficultyScale);
            spawnTimeGhost = Mathf.Max(spawnTimeGhost, minSpawnrateGhost);
            ghostOffset = Random.Range(5, 20);

            yield return new WaitForSeconds(spawnTimeGhost);

            spawnPos.y = Random.Range(-1f, 3f);
            secondSpawnPos.y = Random.Range(-1f, 3f);

            if (gameManager.isGameActive)
            {
                Instantiate(ghostPrefab, new Vector3(16f, spawnPos.y, -1f), ghostPrefab.transform.rotation);
            }
            if (gameManager.isGameActive && spawnTimeGhost < 6f)
            {
                Instantiate(greenGhostPrefab, new Vector3(16f, spawnPos.y, -1f), greenGhostPrefab.transform.rotation);
                GameObject myInstance = Instantiate (greenGhostPrefab, new Vector3(16f + ghostOffset, secondSpawnPos.y, -1f), greenGhostPrefab.transform.rotation);
                myInstance.GetComponent<Ghost>().sinSpeed = Random.Range(19f, 20);
            }
        }
    }
    IEnumerator SpawnHarpyRoutine()
    {
        while (harpyPrefab == true && gameManager.isGameActive)
        {
            spawnTimeHarpy = initialSpawnrateHarpy - ((Time.time - gameTimerStart) * difficultyScale);
            spawnTimeHarpy = Mathf.Max(spawnTimeHarpy, minSpawnrateHarpy);

            yield return new WaitForSeconds(spawnTimeHarpy);

            if (gameManager.isGameActive)
            {
                Instantiate(harpyPrefab, new Vector3(16f, harpySpawn, -1f), harpyPrefab.transform.rotation);
               
            }
        }
    }
    IEnumerator SpawnLifeUpRoutine()
    {
        while (lifeUpPrefab == true && gameManager.isGameActive )
        {
            yield return new WaitForSeconds(20f);

            int spawn = Random.Range(0, spawnPoints.Length);

            if (gameManager.isGameActive && gameManager.currentLives < 3)
            {
                Instantiate(lifeUpPrefab, spawnPoints[spawn], Quaternion.identity);
            }
        }
    }
}
