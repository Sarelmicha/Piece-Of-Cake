using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour, ISpecialPower
{
    [SerializeField] float bonusTime = 3f;

    //UIController uiController;
    Canvas bonusCanvas;



    private void Awake()
    {
        bonusCanvas = GameObject.FindWithTag("Bonus Canvas").GetComponent<Canvas>();
      //  uiController = GameObject.FindWithTag("UI Manager").GetComponent<UIController>();
       
    }

    public void InvokeSpecialPower(CatcherController catcher)
    {       
        catcher.BonusClick();
        //uiController.SwitchCanvasForSeconds(bonusCanvas,bonusTime);
    }
}
   
   

   
