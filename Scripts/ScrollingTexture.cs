using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingTexture : MonoBehaviour
{
    [SerializeField] bool isUIImage;
    Material materialToAnimate;
    public float speedX, speedY;
    private float curX, curY;

    private void Start()
    {
        if (isUIImage)
        {
            materialToAnimate = GetComponent<Image>().material;
        }
        else 
        {
            materialToAnimate = GetComponent<Renderer>().material;
        }

        curX = materialToAnimate.mainTextureOffset.x;
        curY = materialToAnimate.mainTextureOffset.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        curX += Time.deltaTime * speedX;
        curY += Time.deltaTime * speedY;
        materialToAnimate.SetTextureOffset("_MainTex", new Vector2(curX, curY));
    }
}
