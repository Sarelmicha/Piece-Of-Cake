using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularPath : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float counter;
    private float dist;
    private float timeToLive = 3f;
    private int numOfVertices;
    private int currentVerticeIndex;
    private bool originHasSet = false;
    

    Transform origin;
    Transform destiniation;
    [SerializeField] float lineDrawSpeed = 6f;
    [SerializeField] GameObject catchersHolder;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.45f;
        lineRenderer.endWidth = 0.45f;

        numOfVertices = catchersHolder.transform.childCount;
        calculateDistance();
        

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

        if (!originHasSet)
        {
            FindOriginVerticeIndex();
            originHasSet = true;
        }
       

        DrawPath();

    }

    private void FindOriginVerticeIndex()
    {
        for (int i = 0; i < numOfVertices; i++)
        {
            if (Vector2.Distance(catchersHolder.transform.GetChild(i).position, origin.position) < 0.1)
            {
                currentVerticeIndex = i;
                break;
            }
        }
    }

    private void calculateDistance()
    {

        for (int i = 0; i < numOfVertices - 1; i++)
        {
            dist += Vector2.Distance(catchersHolder.transform.GetChild(i).position, catchersHolder.transform.GetChild(i + 1).position);
        }
    }

    public void DrawPath()
    {

        if (counter < dist)
        {
            Vector3 pointA = catchersHolder.transform.GetChild(currentVerticeIndex).position ;
            Vector3 pointB;

            if (currentVerticeIndex + 1 == catchersHolder.transform.childCount)
            {
                pointB = catchersHolder.transform.GetChild(0).position; 
            }
            else
            {
                pointB = catchersHolder.transform.GetChild(currentVerticeIndex + 1).position;
            }
           

            counter += 0.1f / lineDrawSpeed;

            float x = Mathf.Lerp(0, dist / numOfVertices, counter);

            //Get the unit vector in the desired direction, 
            //multiply by the desired length and add the starting point
            Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

            if (Vector2.Distance(lineRenderer.GetPosition(currentVerticeIndex) ,pointB) < 0.2f)
            {
                print("current index " + currentVerticeIndex);
                currentVerticeIndex++;
                print("after increment is " + currentVerticeIndex);
                if (currentVerticeIndex == catchersHolder.transform.childCount)
                {
                    currentVerticeIndex = 0;
                    counter = 0;
                }
                
            }


            if (currentVerticeIndex + 1 == catchersHolder.transform.childCount)
            {
                lineRenderer.SetPosition(0, pointAlongLine);
            }
            else
            {
                lineRenderer.SetPosition(currentVerticeIndex + 1, pointAlongLine);
            }
           
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
