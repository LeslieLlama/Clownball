using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Collector : MonoBehaviour
{
    public static event Action OnPlayerOutOfBounds;
    public static event Action<GameObject> OnHoleDeath;
    public static event Action<GameObject> OnGemDeath;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPlayerOutOfBounds();
        }
        if (collision.gameObject.CompareTag("Hole"))
        {
            OnHoleDeath(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Gem"))
        {
            OnGemDeath(collision.gameObject);
        }
    }



}
