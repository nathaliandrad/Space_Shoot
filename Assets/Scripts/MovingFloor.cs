using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{

     float min = 2f;
     float max = 3f;

    void Start()
    {
        min = transform.position.x;
        max = transform.position.x + 6;
    }

    // Update is called once per frame
    void Update()
    {
        //ping pong function for moving floor
        transform.position = new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.y, transform.position.z);
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            //make the player follow the moving floor
            collision.collider.transform.SetParent(transform);
            //enable jumping whenever its touching the moving floor
            GM.jumpCounts = GM.maxJumps;
        }
        
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //setting it to null so floor is not parent of player anymore
            collision.collider.transform.SetParent(null);
        }

    }
}
