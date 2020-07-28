using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupcakeController : MonoBehaviour
{

    [SerializeField] GameObject catchersHolder;
    [SerializeField] float speed = 0.1f;
    [SerializeField] float maxBoundary = 3f;

    CupcakesSpawner cupcakeSpawner;
    Transform target;
    Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        cupcakeSpawner = GameObject.FindWithTag("CupcakeSpawner").GetComponent<CupcakesSpawner>();
        animator = GetComponentInChildren<Animator>();

        switch (cupcakeSpawner.GetSpawnType())
        {
            case SpawnType.Regular:
                SetTarget(UnityEngine.Random.Range(0, catchersHolder.transform.childCount));
                break;
            case SpawnType.Circular:
                SetTarget(cupcakeSpawner.GetRunningCircularIndex());
                break;
        }
    }

    void Update()
    {
        transform.position  = Vector2.MoveTowards(transform.position, target.position * maxBoundary, speed);

        if (IsOutOfBoundaries())
        {
            Destroy(gameObject);
        }
    }

    private bool IsOutOfBoundaries()
    {
        return Vector2.Distance(transform.position, target.position) > maxBoundary;
    }

    public void Die()
    {
        animator.SetTrigger("die");  
    }

    private void SetTarget(int index)
    {  
        target = catchersHolder.transform.GetChild(index).transform;
    } 
}
