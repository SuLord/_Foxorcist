using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBonds : MonoBehaviour
{
    private float leftBound = -15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy out of bounds
        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
