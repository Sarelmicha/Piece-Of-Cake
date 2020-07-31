﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CatcherController : MonoBehaviour
{
    [SerializeField] float dieDelayTime = 0.15f;
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
                    CupcakeController cupcake = collision.gameObject.GetComponent<CupcakeController>();

                    if (cupcake != null)
                    {
                        cupcake.Die();
                    }

                    ISpecialPower special = collision.gameObject.GetComponent<ISpecialPower>();

                    if (special != null)
                    {
                        special.InvokeSpecialPower(gameObject);
                    }
                    else
                    {
                        animator.SetTrigger("rightClick");
                    }
                    Destroy(collision.gameObject, dieDelayTime);
                }
            }
        }      
    }
}
