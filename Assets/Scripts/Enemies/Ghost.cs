using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy //INHERITANCE
{
    public float sinSpeed;
    public float amplitude = 40;
    void Update()
    {
        MoveLeft();
    }
    protected override void MoveLeft() //POLYMORPHISM
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World); // Move left
        transform.position += transform.up * Mathf.Sin(Time.time * sinSpeed) / amplitude; // Move in sin wave
    }
}
