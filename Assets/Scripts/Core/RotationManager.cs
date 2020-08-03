using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f;

    Ring[] currentRings = null;
    Vector3 initalRotation;
    bool rotate = false;
    const int FULL_CIRCLE = 360;
  

    private void Update()
    {
        if (rotate)
        {
            RotateRings();
        }
     
    }

    private void RotateRings()
    {
        foreach(Ring ring in currentRings)
        {
            ring.transform.Rotate((Vector3.forward * rotationSpeed * Time.deltaTime));

            print("current rotation is " + ring.transform.eulerAngles.z);
          

           if (Mathf.RoundToInt(ring.transform.eulerAngles.z) % FULL_CIRCLE == 0)
            {
                print("im here!");
                ring.transform.eulerAngles = Vector3.zero;
                rotate = false;
            }
        }
    }

    public void TriggerRingRotationForSeconds(Ring[] rings)
    {
        if (rotate)
        {
            return;
        }
        currentRings = rings;      
        rotate = true;
    }
}
