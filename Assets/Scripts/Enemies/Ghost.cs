using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    public float sinSpeed;
    public float amplitude = 40;

    void Update()
    {
        MoveLeft();
        sinWaveOne();
    }

    private void sinWaveOne()
    {
        transform.position += transform.up * Mathf.Sin(Time.time * sinSpeed) / amplitude; // Move in sin wave
    }
}
