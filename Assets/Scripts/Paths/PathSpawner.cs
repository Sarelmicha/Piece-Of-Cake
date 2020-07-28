using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawner : MonoBehaviour
{
    [SerializeField] Path path;
    [SerializeField] CircularPath circularPath;
  

    public void InstanitatePath(Transform origin,Transform destiniation)
    {
     
        Path newPath = Instantiate(path, transform.position, Quaternion.identity);
        newPath.SetOrigin(origin);
        newPath.SetDestiniation(destiniation);
    }

    public void InstantiateCircularPath(Transform origin, Transform destiniation)
    {
        CircularPath newPath = Instantiate(circularPath, transform.position, Quaternion.identity);
        newPath.SetOrigin(origin);
        newPath.SetDestiniation(destiniation);
    }
}
