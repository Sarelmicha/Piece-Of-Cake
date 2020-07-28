using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float counter;
    private float dist;
    private float timeToLive = 3f;
    private int numOfVertices;

    Transform origin;
    Transform destiniation;
    [SerializeField] float lineDrawSpeed = 6f;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.45f;
        lineRenderer.endWidth = 0.45f;

        numOfVertices = lineRenderer.positionCount;

        Destroy(gameObject, timeToLive);
    }

    // Update is called once per frame
    void Update()
    {

        if (origin == null || destiniation == null)
        {
            return;
        }

        //Those 2 line need to happen just once,
        lineRenderer.SetPosition(0, origin.position);
        calculateDistance();
   
        DrawPath();
        
    }

    private void calculateDistance()
    {
         dist = Vector2.Distance(origin.position, destiniation.position);
    }

    public void DrawPath()
    {

        if (counter < dist)
        {
        Vector3 pointA = origin.position;
        Vector3 pointB = destiniation.position;

        counter += 0.1f / lineDrawSpeed;

        float x = Mathf.Lerp(0, dist, counter);

        //Get the unit vector in the desired direction, 
        //multiply by the desired length and add the starting point
        Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;
      
        lineRenderer.SetPosition(1, pointAlongLine);

        }
    }

    public void SetOrigin(Transform origin)
    {
        this.origin = origin;
    }

    public void SetDestiniation(Transform destiniation)
    {
        this.destiniation = destiniation;
    }

    public Transform GetOrigin()
    {
        return this.origin;
    }

    public Transform GetDestination()
    {
        return this.destiniation;
    }
}
