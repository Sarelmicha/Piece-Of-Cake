using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour, ISpecialPower
{

    [SerializeField] float radius = 1f;
    [SerializeField] float destroyDelayTime = 0.2f;


    public void InvokeSpecialPower(CatcherController catcher)
    {
        //Make bomb partile Effect
        catcher.GrenadeClick();

        //Destory all in a specific radius

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);

        print("length is " + hitColliders.Length);
        foreach (var hitCollider in hitColliders)
        {
            //Objects to destory on screen
            FruitController cupcake = hitCollider.GetComponent<FruitController>();
            ParachuteController parachute = hitCollider.GetComponent<ParachuteController>();
            Path path = hitCollider.GetComponent<Path>();

            if (cupcake != null || parachute != null || path != null)
            {
                Destroy(hitCollider.gameObject,destroyDelayTime);
            }
        }


    }
}
