using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusesSpawner : MonoBehaviour
{

    [SerializeField] Transform startPosition;
    [SerializeField] Transform endPosition;
    [SerializeField] float speed = 0.1f;
    [SerializeField] ParachuteController parachute;
    [SerializeField] float spawnTime = 10f;

    float startTime = 0;
   
    Vector3 currentTarget;
  
       
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = startPosition.position;
        currentTarget = endPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (startTime > spawnTime)
        {
            InstantiateParachute();
        }
        UpdateTimers();
        
    }

    private void InstantiateParachute()
    {
        Instantiate(parachute, transform.position, Quaternion.identity);
        startTime = 0;
    }

    private void UpdateTimers()
    {
        startTime += Time.deltaTime;
    }

    private void Move()
    {

        transform.position = Vector2.MoveTowards(transform.position, currentTarget, speed);

        if (transform.position.x == startPosition.position.x)
        {
            currentTarget = endPosition.position;
        }
        else if (transform.position.x == endPosition.position.x)
        {
            currentTarget = startPosition.position ;
        }
    }
}
