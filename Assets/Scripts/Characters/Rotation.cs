using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour, ISpecialPower
{
    RotationManager rotationManager;

    private void Start()
    {
        rotationManager = GameObject.FindWithTag("Rotation Manager").GetComponent<RotationManager>();
    }

    public void InvokeSpecialPower(CatcherController catcher)
    {

        catcher.RotateClick();

        Ring[] rings = GameObject.FindObjectsOfType<Ring>();

        if (rings.Length != 0)
        {
            rotationManager.TriggerRingRotationForSeconds(rings);
        }
    }
}
