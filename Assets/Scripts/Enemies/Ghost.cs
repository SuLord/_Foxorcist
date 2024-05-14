using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5;
    public float sinSpeed;
    public float amplitude = 40;
    public int pointValue;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        // Move left in a sin wave

        transform.Translate(Vector3.left * Time.deltaTime * speed);
        transform.position += transform.up * Mathf.Sin(Time.time * sinSpeed) / amplitude;
    }
}
