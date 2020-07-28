using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour, ISpecialPower
{

    public void InvokeSpecialPower(GameObject catcher)
    {

        //Maybe change color of the button when clicked
        catcher.GetComponent<Animator>().SetTrigger("rightClick");

        CupcakesSpawner cupcakesSpawner = GameObject.FindWithTag("CupcakeSpawner").GetComponent<CupcakesSpawner>();

        if (cupcakesSpawner != null)
        {
            cupcakesSpawner.SetSpawnType(SpawnType.Circular);
        }
    }
}
