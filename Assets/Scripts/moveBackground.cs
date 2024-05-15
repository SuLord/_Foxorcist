using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    private float speed = 10;
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x * transform.localScale.x / 2;
    }
    void Update()
    {
        BackgroundRepeat();
    }
    private void BackgroundRepeat()
    {
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
        if (gameManager.isGameActive)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
    }
}
