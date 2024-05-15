using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HarpyBullet : Enemy //INHERITANCE
{
    [SerializeField] float bulletSpeed = 10f;
    private float xRange;
    public Transform target;
    [SerializeField] bool aim;
    void Start()
    {
        xRange = Random.Range(3, -7);
        target = GameObject.FindGameObjectWithTag("Offset").transform;
        aim = true;
    }
    void Update()
    {
         if(target != null && aim)
          {
               transform.LookAt(target);
          }
        if (transform.parent.position.x < xRange)
        {
            transform.position += transform.forward * bulletSpeed * Time.deltaTime;
            aim = false;
        }
    }
}
