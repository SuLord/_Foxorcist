using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpy : Enemy //INHERITANCE
{
    [SerializeField] GameObject bullet;
    void Start()
    {
        Instantiate(bullet, transform, worldPositionStays: false);
    }
    // Update is called once per frame
    void Update()
    {
        MoveLeft();
    }
}
