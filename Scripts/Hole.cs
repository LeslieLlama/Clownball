using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hole : MonoBehaviour
{
    //public static event Action<GameObject> OnHoleDeath;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.GetComponent<Collector>() != null)
        {
            print("hole death");
            OnHoleDeath(gameObject);
        }*/
    }
}
