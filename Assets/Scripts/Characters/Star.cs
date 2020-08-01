using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour, ISpecialPower
{

    public void InvokeSpecialPower(CatcherController catcher)
    {

        //Maybe change color of the button when clicked
        catcher.StarClick();

        FruitsSpawner fruitsSpawner = GameObject.FindWithTag("CupcakeSpawner").GetComponent<FruitsSpawner>();

        if (fruitsSpawner != null)
        {
            fruitsSpawner.SetSpawnType(SpawnType.Circular);
        }
    }
}
