using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CatcherController : MonoBehaviour
{
    [SerializeField] float dieDelayTime = 0.15f;
    [SerializeField] GameObject regularEffect;
    [SerializeField] GameObject bombEffect;
    [SerializeField] GameObject starEffect;
    [SerializeField] GameObject grenadeEffect;
    Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.name == gameObject.name)
                {
                    FruitController cupcake = collision.gameObject.GetComponent<FruitController>();

                    if (cupcake == null)
                    {
                        return;
                    }

                    cupcake.Die();

                    ISpecialPower special = collision.gameObject.GetComponent<ISpecialPower>();

                    if (special != null)
                    {
                        special.InvokeSpecialPower(gameObject.GetComponent<CatcherController>());
                    }
                    else
                    {
                        RegularClick();
                    }
                    Destroy(collision.gameObject, dieDelayTime);
                }
            }
        }      
    }

    public void Hover()
    {
        animator.SetTrigger("hover");
    }

    public void RegularClick()
    {
        animator.SetTrigger("rightClick");
        TriggerRegularVFX();
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

    public void TriggerRegularVFX()
    {
        Instantiate(regularEffect,transform.position,transform.rotation);
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
}
