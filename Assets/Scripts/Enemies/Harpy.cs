using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Harpy : Enemy
{
    public GameObject bullet;

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
