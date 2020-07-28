using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, ISpecialPower
{
    public void InvokeSpecialPower(GameObject catcher)
    {
    
        catcher.GetComponent<Animator>().SetTrigger("wrongClick");
    }
}
