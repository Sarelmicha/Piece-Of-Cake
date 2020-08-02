using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour, ISpecialPower
{
    [SerializeField] float bonusTime = 3f;

    UIManager uiManager;
    Canvas bonusCanvas;

    private void Awake()
    {
        bonusCanvas = GameObject.FindWithTag("Bonus Canvas").GetComponent<Canvas>();
        uiManager = GameObject.FindWithTag("UI Manager").GetComponent<UIManager>();
    }

    public void InvokeSpecialPower(CatcherController catcher)
    {
        catcher.BonusClick();
        uiManager.SwitchCanvasForSeconds(bonusCanvas,bonusTime);
    }
}
   
   

   
