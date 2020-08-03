using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f;

    Ring[] currentRings = null;
    Vector3 initalRotation;
    bool isRotate = false;
    const int FULL_CIRCLE = 360;
  

    private void Update()
    {
        if (isRotate)
        {
            RotateRings();
        }
     
    }

    private void RotateRings()
    {
        foreach(Ring ring in currentRings)
        {
            ring.transform.Rotate((Vector3.forward * rotationSpeed * Time.deltaTime));
          
           if (Mathf.RoundToInt(ring.transform.eulerAngles.z) % FULL_CIRCLE == 0)
            {
                ring.transform.eulerAngles = Vector3.zero;
                isRotate = false;
            }
        }
    }

    public void TriggerRingRotationForSeconds(Ring[] rings)
    {
        if (isRotate)
        {
            return;
        }
        currentRings = rings;      
        isRotate = true;
    }


    public bool IsRotate()
    {
        return this.isRotate;
    }
}
