using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsDestroyed : MonoBehaviour
{
    //ABSTRACTION
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
        if (other.CompareTag("Player") && playerController.rotating) // Player flips and destroys
        {
            Destroy(gameObject);
            print("Flipped Enemy");
            playerController.hitParticles.transform.position = gameObject.transform.position;
            playerController.hitParticles.Play();
            gameManager.UpdateScore(pointValue);
        }
        if (other.CompareTag("Player") && playerController.goingUp) // Player jumps and destroys
        {
            Destroy(gameObject);
            print("Jumped Enemy");
            playerController.hitParticles.transform.position = gameObject.transform.position;
            playerController.hitParticles.Play();
            gameManager.UpdateScore(pointValue);
        }
    }
}
