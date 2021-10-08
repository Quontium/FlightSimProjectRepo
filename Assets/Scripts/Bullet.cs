using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlightSim.AI;
public class Bullet : MonoBehaviour
{
    public float paperPiercing;
    public GameObject Shooter;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Ally" || collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<PlacesToBeHit>() != null)
            {
                PlacesToBeHit placeHit = collision.gameObject.GetComponent<PlacesToBeHit>();
                //AIType ai = gameObject.GetComponentInParent<AIType>();
                AIType ai = gameObject.GetComponent<AIType>();
                float damage = paperPiercing + paperPiercing * placeHit.procentExtraDamage / 100;
                if (damage <= 0)
                    return;
                ai.changeHealth(-damage);
            }
        }
    }
}
