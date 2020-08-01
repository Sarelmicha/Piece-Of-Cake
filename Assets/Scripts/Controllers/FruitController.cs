using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{

    [SerializeField] GameObject catchersHolder;
    [SerializeField] float speed = 0.1f;
    [SerializeField] float maxBoundary = 3f;

    FruitsSpawner fruitsSpawner;
    Transform target;
    Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        fruitsSpawner = GameObject.FindWithTag("CupcakeSpawner").GetComponent<FruitsSpawner>();
        animator = GetComponentInChildren<Animator>();

        switch (fruitsSpawner.GetSpawnType())
        {
            case SpawnType.Regular:
                SetTarget(UnityEngine.Random.Range(0, catchersHolder.transform.childCount));
                break;
            case SpawnType.Circular:
                SetTarget(fruitsSpawner.GetRunningCircularIndex());
                break;
        }
    }

    void Update()
    {
        transform.position  = Vector2.MoveTowards(transform.position, target.position * maxBoundary, speed * Time.deltaTime);

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
