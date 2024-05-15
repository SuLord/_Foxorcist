using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUp : MonoBehaviour
{
    [SerializeField] float speed = 5;
    private PlayerController playerController;
    private GameManager gameManager;
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed); // Move Left
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Player picks up Power Up
        {
            Destroy(gameObject);
            playerController.powerUpParticles.transform.position = gameObject.transform.position;
            playerController.powerUpParticles.Play();
            gameManager.currentLives++;
            gameManager.UpdateLives();
        }
    }
}
