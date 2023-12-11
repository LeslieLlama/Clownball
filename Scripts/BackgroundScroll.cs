using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform startPosition;
    [SerializeField] float resetValue = -14.26f;

    /*private void Start()
    {
        startPosition.position = transform.position;
    }*/

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < resetValue)
        {
            transform.position = startPosition.position;
        }
    }
}

