using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // Pathfinding Comp Ref
    Vector2 startPosition;
    Vector2 roamPosition;

    private void Awake()
    {
        // Get the pathfinding component and set the ref with it
    }

    private void Start()
    {
        startPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Enemy has touched the player! BATTLE TIME");
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return startPosition + GetRandVecDir() * Random.Range(10.0f, 70.0f);
    }

    // Returns a random normalized direction
    private static Vector2 GetRandVecDir()
    {
        return new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
    }
}
