using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{


    public GameObject ghostPrefab;
    public GameObject greenGhostPrefab;
    public GameObject harpyPrefab;
    public GameObject lifeUpPrefab;

    private float harpySpawn = 5.8f;
    private float spawnTimeHarpy;
    public float spawnTimeGhost;

    private GameManager gameManager;

    public float initialSpawnrateGhost = 7f;
    public float minSpawnrateGhost = 1f;
    public float initialSpawnrateHarpy = 10f;
    public float minSpawnrateHarpy = 3f;
    public float difficultyScale = 2f;
    private float gameTimerStart = 0;
    private float ghostOffset;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {

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

            // Wait a set time
            yield return new WaitForSeconds(spawnTimeGhost);

            // Spawn Enemy
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
            //Loop back to wait
        }
    }

    IEnumerator SpawnHarpyRoutine()
    {
        while (harpyPrefab == true && gameManager.isGameActive)
        {
            spawnTimeHarpy = initialSpawnrateHarpy - ((Time.time - gameTimerStart) * difficultyScale);
            spawnTimeHarpy = Mathf.Max(spawnTimeHarpy, minSpawnrateHarpy);

            // Wait a set time
            yield return new WaitForSeconds(spawnTimeHarpy);

            // Spawn Enemy

            if (gameManager.isGameActive)
            {
                Instantiate(harpyPrefab, new Vector3(16f, harpySpawn, -1f), harpyPrefab.transform.rotation);
               
            }
            //Loop back to wait
        }
    }

    IEnumerator SpawnLifeUpRoutine()
    {
        while (lifeUpPrefab == true && gameManager.isGameActive )
        {
            // Wait a set time
            yield return new WaitForSeconds(12f);

            // Spawn Enemy
            float spawnRange = Random.Range(1f, 5.8f);

            if (gameManager.isGameActive && gameManager.currentLives < 3)
            {
                Instantiate(lifeUpPrefab, new Vector3(16f, spawnRange, -1f), lifeUpPrefab.transform.rotation);
            }
            //Loop back to wait
        }
    }
}
