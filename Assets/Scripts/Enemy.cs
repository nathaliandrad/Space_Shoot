using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float startFollow;
    public float enemyMovementSpeed;
    public GameObject enemy;
     int enemyHealth;

    public AudioSource speaker;
    public AudioClip enemyKilledSound, enemyHittedSound;

    public Animator enemyAnimator;

    public GameObject explosion;
    Renderer rend;
    Color col;

    // Start is called before the first frame update
    void Start()
    {
        startFollow = 10;
        enemyHealth = 2;
        rend = GetComponent<Renderer>();

        col.r = 0;
        col.g = 0;
        col.b = 0;
        col.a = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //check the distance of the player
        if (Vector2.Distance(transform.position, target.position) < startFollow)
        {
            
            //way of making the sprite flip
            if (target.position.x > transform.position.x)
            {
                //face right
                transform.localScale = new Vector3(0.5f, 0.5f, 1);
                enemyAnimator.SetBool("followingPlayer", false);
            }
            else if (target.position.x < transform.position.x)
            {
                //face left
                enemyAnimator.SetBool("followingPlayer", true);
                transform.localScale = new Vector3(-0.5f, 0.5f, 1);
            }
            //enemy follows player
            transform.position = Vector2.MoveTowards(transform.position, target.position, enemyMovementSpeed * Time.deltaTime);
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Physics.IgnoreLayerCollision(9, 10);
        if (collision.CompareTag("Bullet")) {
            speaker.PlayOneShot(enemyHittedSound, 1);
            Destroy(collision.gameObject);
            
            enemyHealth -= 1;
            if (enemyHealth < 0)
            {
                speaker.PlayOneShot(enemyKilledSound, 1);
                explosion.SetActive(true);
                rend.material.SetColor("_Color", col);
                GM.score += 10;
                Destroy(gameObject,0.5f);
                enemyHealth = 2;
                
            }

        }

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Physics.IgnoreLayerCollision(9, 10);
        }
    }



}
