using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsDestroyed : MonoBehaviour
{
    [SerializeField] int pointValue;
    private PlayerController playerController;
    private GameManager gameManager;
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // Player flips and destroys

        if (other.CompareTag("Player") && playerController.rotating)
        {
            Destroy(gameObject);
            print("Flipped Enemy");
            playerController.hitParticles.transform.position = gameObject.transform.position;
            playerController.hitParticles.Play();
            gameManager.UpdateScore(pointValue);
        }

        // Player jumps and destroys

        if (other.CompareTag("Player") && playerController.goingUp)
        {
            Destroy(gameObject);
            print("Jumped Enemy");
            playerController.hitParticles.transform.position = gameObject.transform.position;
            playerController.hitParticles.Play();
            gameManager.UpdateScore(pointValue);
        }
    }
}
