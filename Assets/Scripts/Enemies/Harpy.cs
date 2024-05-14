using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpy : Enemy
{
    public GameObject bullet;
    private PlayerController playerController;
    private GameManager gameManager;
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        Instantiate(bullet, transform, worldPositionStays: false);
    }
    // Update is called once per frame
    void Update()
    {
        MoveLeft();
    }
}
