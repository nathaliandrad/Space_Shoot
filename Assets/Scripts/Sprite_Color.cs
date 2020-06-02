using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite_Color : MonoBehaviour
{
    Renderer rend;
    Color col;
    Vector4 originalColor;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        col.r = 1;
        col.g = 1;
        col.b = 1;
        col.a = 0.3f;
        originalColor = GetComponent<Renderer>().material.color;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.playerIsHit)
        {
            StartCoroutine("ChangeColor");
        }
        else
        {
            rend.material.SetColor("_Color", originalColor);
        }
        
    }

    public IEnumerator ChangeColor()
    {
        float timer = 0;
        float setTimer = 3;
        while(timer < setTimer)
        {
            yield return new WaitForSeconds(0.1f);
            rend.material.SetColor("_Color", col);
            timer++;
        }
    
       
    }
}
