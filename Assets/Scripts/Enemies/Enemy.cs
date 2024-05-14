using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 5;

    void Update()
    {
        MoveLeft();
    }

    protected void MoveLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World); // Move left
    }
}
