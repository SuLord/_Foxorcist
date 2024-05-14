using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUp : MonoBehaviour
{
    [SerializeField] float speed = 5;
    private PlayerController playerController;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move Left

        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player picks up Power Up

        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            playerController.powerUpParticles.transform.position = gameObject.transform.position;
            playerController.powerUpParticles.Play();
            gameManager.currentLives++;
            gameManager.UpdateLives();
        }
    }
}
