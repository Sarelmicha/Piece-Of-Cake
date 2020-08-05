using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CatcherController : MonoBehaviour
{
    [SerializeField] float dieDelayTime = 0.15f;
    [SerializeField] GameObject rightClick;
    [SerializeField] GameObject bombEffect;
    [SerializeField] GameObject starEffect;
    [SerializeField] GameObject grenadeEffect;
    [SerializeField] GameObject bonusEffect;
    [SerializeField] GameObject rotateEffect;

    GameManager gameManager;

    bool isTriggerd = false;

    Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.name == gameObject.name && !isTriggerd)
                {
                    RegularClick();
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        isTriggerd = true;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.name == gameObject.name)
                {
                    FruitController fruit = collision.gameObject.GetComponent<FruitController>();

                    if (fruit == null)
                    {
                        return;
                    }
                    
                    fruit.Die();
                    AwardScore(fruit);

                    ISpecialPower special = collision.gameObject.GetComponent<ISpecialPower>();

                    if (special != null)
                    {
                        special.InvokeSpecialPower(gameObject.GetComponent<CatcherController>());
                    }
                    else
                    {
                        RightClick();
                    }
                    Destroy(collision.gameObject, dieDelayTime);
                }
            }
        }

        //isHandleFruit = false;
        isTriggerd = false;
    }

    private void AwardScore(FruitController cupcake)
    {
        gameManager.AddToScore(cupcake.GetScorePoints());
    }

    public void Hover()
    {
        animator.SetTrigger("hover");
    }

    public void RightClick()
    {
        animator.SetTrigger("rightClick");
        TriggerRightClickVFX();
    }

    public void RegularClick()
    {
        animator.SetTrigger("click");
    }


    public void BombClick()
    {
        animator.SetTrigger("wrongClick");
        TriggerBombVFX();
    }

    public void GrenadeClick()
    {
        animator.SetTrigger("rightClick");
        TriggerGrenadeVFX();
    }

    public void StarClick()
    {
        animator.SetTrigger("rightClick");
        TriggerStarVFX();
    }

    public void BonusClick()
    {
        animator.SetTrigger("rightClick");
        TriggerBonusVFX();

    }

    public void RotateClick()
    {
        animator.SetTrigger("rightClick");
        TriggerRotateVFX();

    }

    public void TriggerRightClickVFX()
    {
        Instantiate(rightClick,transform.position,transform.rotation);
    }

    public void TriggerBombVFX()
    {

        Instantiate(bombEffect, transform.position, transform.rotation);
    }
    public void TriggerStarVFX()
    {

        Instantiate(starEffect, transform.position, transform.rotation);
    }

    public void TriggerGrenadeVFX()
    {

        Instantiate(grenadeEffect, transform.position, transform.rotation);
    }


    public void TriggerBonusVFX()
    {
        Instantiate(bonusEffect, transform.position, transform.rotation);
    }

    public void TriggerRotateVFX()
    {
        Instantiate(rotateEffect, transform.position, transform.rotation);
    }


}
