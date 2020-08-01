using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour, ISpecialPower
{

    public void InvokeSpecialPower(CatcherController catcher)
    {

        //Maybe change color of the button when clicked
        catcher.StarClick();

        CupcakesSpawner cupcakesSpawner = GameObject.FindWithTag("CupcakeSpawner").GetComponent<CupcakesSpawner>();

        if (cupcakesSpawner != null)
        {
            cupcakesSpawner.SetSpawnType(SpawnType.Circular);
        }
    }
}
