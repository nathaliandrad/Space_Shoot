using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Rigidbody2D rb;
    public GameObject gun;
    public Vector2 player;
    public float bulletLimitPosition;
    public float bulletPosition_x;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gun = GameObject.Find("Gun");
        if (Input.GetKey(KeyCode.D))
        {
            Destroy(gameObject, 0.80f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Destroy(gameObject, 0.55f);
        }
        else {
            Destroy(gameObject, 0.63f);
        }

            player = gun.transform.right;
            rb.AddForce((player * 26) + Wind.WindDirection, ForceMode2D.Impulse);
    }

}
