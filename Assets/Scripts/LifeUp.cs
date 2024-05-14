using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUp : MonoBehaviour
{
    public float speed = 5;
    private float leftBound = -15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move Left

        transform.Translate(Vector3.left * Time.deltaTime * speed);


        // Destroy out of bounds
        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
