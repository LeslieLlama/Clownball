using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Clownball : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private CircleCollider2D circleCollider2D;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Color ropeBoundColor;
    [SerializeField] private Color midAirColor;

    public bool ballConnected = false;
    public bool isInvincible = false;

    public static event Action OnPlayerDeath;

    private void OnEnable()
    {
        Collector.OnPlayerOutOfBounds += InvincibilityTrigger;
        RopeScript.OnSlingshotRelease += FlingBall;
        //RopeScript.OnSlingshotRelease += TriggerIntangibility;
    }
    private void OnDisable()
    {
        Collector.OnPlayerOutOfBounds -= InvincibilityTrigger;
        RopeScript.OnSlingshotRelease -= FlingBall;
        //RopeScript.OnSlingshotRelease -= TriggerIntangibility;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void InvincibilityTrigger()
    {
        StartCoroutine(InvincibilityTimer(2f));
    }

    IEnumerator InvincibilityTimer(float seconds)
    {
        isInvincible = true;
        spriteRenderer.color = Color.grey;
        yield return new WaitForSeconds(seconds);
        spriteRenderer.color = midAirColor;
        isInvincible = false;
    }

    void TriggerIntangibility()
    {
        StartCoroutine(IntangibilityTimer(0.25f));
    }

    private IEnumerator IntangibilityTimer(float time)
    {
        circleCollider2D.enabled = false;
        yield return new WaitForSeconds(time);
        circleCollider2D.enabled = true;
    }

    void DeathAnimation(Transform holeToFallIn)
    {
        //god fucking damn DOTween is the best asset ever made
        rb2d.simulated = false;
        transform.DOLocalRotate(new Vector3(0, 0, 350), 2f);
        transform.DOLocalMove(holeToFallIn.position, 2f);
        transform.DOScale(Vector3.zero, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hole") && isInvincible == false)
        {
            OnPlayerDeath();
            DeathAnimation(collision.transform);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spriteRenderer.color = ropeBoundColor;
            if (isInvincible == true) { spriteRenderer.color = Color.grey; }
            ballConnected = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spriteRenderer.color = midAirColor;
            ballConnected = false;

        }
    }

    public void FlingBall(float num)
    {
        transform.DOShakeScale(0.1f);
    }



}
