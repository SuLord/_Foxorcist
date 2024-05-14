using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HarpyBullet : MonoBehaviour
{
    public float speed = 10f;
    private float xRange;
    public Transform target;
    public bool aim;

    // Start is called before the first frame update
    void Start()
    {
        xRange = Random.Range(3, -7);

        target = GameObject.FindGameObjectWithTag("Offset").transform;

        aim = true;
    }

    // Update is called once per frame
    void Update()
    {
         if(target != null && aim)
          {
               transform.LookAt(target);
          }

        if (transform.parent.position.x < xRange)
        {
            
            transform.position += transform.forward * speed * Time.deltaTime;
            aim = false;
        }
    }
}
