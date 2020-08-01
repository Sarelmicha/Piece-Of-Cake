using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteController : MonoBehaviour
{

    [SerializeField] float fadeSpeed = 1f;
    [SerializeField] float delayTime = 0.3f;
    [SerializeField] GameObject pickupHeartEffet;

    Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckHit();
    }

    private void CheckHit()
    {
        if (Input.GetMouseButtonDown(0))
        {
          
            //RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            RaycastHit2D[] hits = RaycaseAllSorted();

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.name == gameObject.name)
                    {
                        //Award price
                        TriggerPickupHeartVFX();
                        animator.SetTrigger("collected");
                        Destroy(gameObject, delayTime);
                    }
                }
            }  
        }
    }

    private RaycastHit2D[] RaycaseAllSorted()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero);

        float[] distances = new float[hits.Length];

        for (int i = 0; i < hits.Length; i++)
        {
            distances[i] = hits[i].distance;
        }
        Array.Sort(distances, hits);

        return hits;
    }

    private void Move()
    {
        transform.Translate(Vector2.down * fadeSpeed * Time.deltaTime);
    }


    public void TriggerPickupHeartVFX()
    {

        Instantiate(pickupHeartEffet, transform.position, transform.rotation);
    }
}


