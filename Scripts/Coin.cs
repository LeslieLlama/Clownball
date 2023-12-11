using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    public static event Action<GameObject> OnCoinPickup;
    //[SerializeField] private Color highValueColour;
    private SpriteRenderer spr;
    [SerializeField] private List<CoinColour> colourList;
    public int coinToAward;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnCoinPickup(gameObject);
        }
        if(collision.gameObject.GetComponent<PointCheckBar>() != null)
        {
            print("value changed");
            coinToAward = collision.GetComponent<PointCheckBar>().newCoinValue;
            Color newColour = Color.white;
            for (int i = 0; i < colourList.Count; i++)
            {
                if (colourList[i].coinValue == coinToAward)
                {
                    newColour = colourList[i].coinValueColour;
                }
            }
            spr.DOColor(newColour, 1f);
        }
    }
}

[Serializable]
public struct CoinColour
{
    public int coinValue;
    public Color coinValueColour;
}
