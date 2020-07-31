using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, ISpecialPower
{
    public void InvokeSpecialPower(CatcherController catcher)
    {

        catcher.WrongClick();
    }
}
