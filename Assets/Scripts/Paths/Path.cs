using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{

    [SerializeField] float lineDrawSpeed = 6f;
    [SerializeField] GameObject catchersHolder;
    [SerializeField] float pathWidth = 0.45f;

    private LineRenderer lineRenderer;
    private float counter;
    private float dist;
    private float timeToLive = 7f;
    private int numOfVertices;
    private PathType pathType;
    private int currentVeticeIndex = 0;
    private int nextVerticeIndex;
    private bool destinationIndexHasFound = false;
    private bool isStartPositionSet;

    Vector3 origin;
    Vector3 destiniation;
    Vector3 startPosition;


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = pathWidth;
        lineRenderer.endWidth = pathWidth;

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

        if (!isStartPositionSet)
        {
            startPosition = origin;
            isStartPositionSet = true;
        }
        
        //print("origin " + origin);
        //print("dest " + destiniation);

        if (!destinationIndexHasFound && pathType == PathType.Circular)
        {
            SetCircularDestination();
        }

        //TODO - need to do only once
        calculateDistance();

        lineRenderer.SetPosition(currentVeticeIndex, origin);
        if (currentVeticeIndex + 1 == numOfVertices)
        {
            lineRenderer.SetPosition(0 ,destiniation);
        }
        else
        {
            lineRenderer.SetPosition(currentVeticeIndex + 1, destiniation);
        }
       

        DrawPath(currentVeticeIndex + 1);
        
    }

    private void SetCircularDestination()
    {
        nextVerticeIndex =  FindDestinationVerticeIndex();
        destiniation = catchersHolder.transform.GetChild(nextVerticeIndex).position;
        destinationIndexHasFound = true;
    }

    public int FindDestinationVerticeIndex()
    {
        //Find the index of the origin vetice
        for (int i = 0; i < numOfVertices; i++)
        {
            if (Vector2.Distance(catchersHolder.transform.GetChild(i).position, origin) < 0.1)
            {
                if (i + 1 == numOfVertices)
                {
                    return 0;
                }
                else
                {
                    return i + 1; 
                }
               
            }
        }

        //Vertice not found.
        return -1;
    }

    private void calculateDistance()
    {
         dist = Vector2.Distance(origin, destiniation);
    }

    public void DrawPath(int position)
    {

        if (position == numOfVertices)
        {
            return;
        }

        if (counter < dist)
        {
            Vector3 pointA = origin;
            Vector3 pointB = destiniation;

            counter += 0.3f / lineDrawSpeed;

            float x = Mathf.Lerp(0, dist, counter);

            //Get the unit vector in the desired direction, 
            //multiply by the desired length and add the starting point
            Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;
            SetPositions(position,pointAlongLine);
          
        }
        else if (pathType == PathType.Circular)
        {

            counter = 0;
            currentVeticeIndex++;
            nextVerticeIndex++;
            if (nextVerticeIndex == numOfVertices)
            {
                nextVerticeIndex = 0;
            }

            origin = destiniation;
            destiniation = catchersHolder.transform.GetChild(nextVerticeIndex).position;
            calculateDistance();
        }
    }

    private void SetPositions(int position,Vector3 pointAlongLine)
    {
        for (int i = position; i < numOfVertices; i++)
        {
            lineRenderer.SetPosition(i, pointAlongLine);
        }
    }


    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetDestiniation(Vector3 destiniation)
    {
        this.destiniation = destiniation;
    }

    public Vector3 GetOrigin()
    {
        return this.origin;
    }

    public Vector3 GetStartPosition()
    {
        return this.startPosition;
    }

    public Vector3 GetDestination()
    {
        return this.destiniation;
    }

    public void SetPathType(PathType pathType)
    {
        this.pathType = pathType;
    }

    public PathType GetPathType()
    {
        return pathType;
    }
}
