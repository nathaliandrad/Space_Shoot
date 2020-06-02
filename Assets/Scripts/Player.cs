using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float xSpeed;
    public GameObject masterBullet, portal_obj, blt;
    // GameObject enemy;
    public Animator animator;
    public Transform firepoint, portal;
    public static int num;
    public Text lives_text, score_text;
   
    public AudioSource speaker, speakerMusic;
    public AudioClip whenHittedSound, enemyDies, laserGun, coin, lives_sound, portalSound, falleSound;

    float positionY, pos, apperPortal;
    bool invincible, timeoutActive, isPlayerDead, isRunning, isShooting, facingRight, canShoot;

    public static bool playerIsHit;
    // Start is called before the first frame update
    void Start()
    {
        playerIsHit = false;


        isRunning = false;
        isShooting = false;
        isPlayerDead = false;
        invincible = false;
        timeoutActive = false;
        facingRight = true;
        canShoot = false;
        
        pos = -16.50f;
        apperPortal = 10;
        GM.jumpCounts = GM.maxJumps;
        //setting texts/images
        SetCountLives();
        SetScoreText();
        SetCoinsText();

        FindObjectOfType<GameManager>();
        rb.GetComponent<Rigidbody2D>();
       //speaker = GetComponent<AudioSource>();

        speakerMusic.Play(1);
        portal_obj.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isShooting && isRunning)
        {
            animator.SetBool("isShooting&Running", true);
        }
        else
        {
            animator.SetBool("isShooting&Running", false);
        }

        PlayerMovement();
        SetScoreText();
        SetCoinsText();
        rb.velocity = new Vector2(xSpeed, rb.velocity.y);
      
            if (transform.position.y < pos)
            {

                Restart();
            }
        
     
  
        if (Vector2.Distance(transform.position, portal.position) < apperPortal)
        {
            Debug.Log("do something here");
        }

        if (invincible)
        {
            if (timeoutActive)
            {
                Wait();
            }
        }
        //from any scene if lives are less than 0 then restarts from level1
        if (GM.lives <= 0) {
            GM.lives = 5;
            GM.score = 0;
            SceneManager.LoadScene(2);
            GM.coins = 0;
        }

    }


    public void PlayerMovement()
    {
        //flipping sprite whenever in need
        FlipSprite(xSpeed);
        
        //setting annimation
        animator.SetFloat("Speed",Mathf.Abs(xSpeed));

        if(xSpeed != 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (Input.GetKey(KeyCode.A)) {
            xSpeed = -7;
        }

        if (Input.GetKey(KeyCode.D))
        {
            xSpeed = 7;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (GM.jumpCounts > 0) {
                
                Jump();
            }

        }

        if (Input.GetKeyDown(KeyCode.Space)) {
          
            animator.SetBool("isShooting", true);
            Shoot();
            isShooting = true;
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            animator.SetBool("isShooting", false);
            isShooting = false;
        }

        //when user is not going sideways, stays where you are
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            xSpeed = 0;
        }



    }




    public void Jump()
    {
        animator.SetBool("isJumping", true);
        GM.jumpCounts -= 1;
        Debug.Log("Screen Withd: " + Screen.width);
        //jump force
        rb.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
        //make falling more natural
        rb.gravityScale = 3;
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    public void Shoot()
    {
        speaker.PlayOneShot(laserGun, 1);
        blt = Instantiate(masterBullet, firepoint.transform.position, firepoint.transform.rotation) as GameObject;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor") {
            GM.jumpCounts = GM.maxJumps;
            animator.SetBool("isJumping", false);
      
        }
        if (collision.gameObject.tag == "BrickFloor")
        {
            GM.jumpCounts = GM.maxJumps;
            animator.SetBool("isJumping", false);
          
        }
        if (collision.gameObject.tag == "MovingFloor")
        {
         
            animator.SetBool("isJumping", false);
      
        }
        if (collision.gameObject.CompareTag("Enemy")) {
          
            //run a function when enemy collides from the right
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                playerGotHitRight();
            }
            else if (collision.gameObject.transform.position.x < transform.position.x) {
                playerGotHitLeft();
            }

            invincible = true;
            Respawn();
            StartCoroutine("Wait");


        }
        if (collision.gameObject.tag == "Spikes") {
            //Restart();
            invincible = true;
            Respawn();
            StartCoroutine("Wait");
        
        }
        if (collision.gameObject.tag == "temp")
        {

            Destroy(collision.gameObject);
            speaker.PlayOneShot(portalSound, 1);
            portal_obj.gameObject.SetActive(true);

        }

        if (collision.gameObject.tag == "Live")
        {
            speaker.PlayOneShot(lives_sound, 1);
            Destroy(collision.gameObject);
            GM.lives += 1;
            SetCountLives();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            speaker.PlayOneShot(coin, 1);
            Destroy(collision.gameObject);
            GM.score += 50;
            SetCountLives();
            GM.coins += 1;
        }
        if (collision.CompareTag("barrier_fallen"))
        {
            Destroy(collision.gameObject);
            speaker.PlayOneShot(falleSound, 1);
        }
    }
    //function to flip sprites
    public void FlipSprite(float xSpeed) {
        if (xSpeed > 0 && !facingRight || xSpeed < 0 && facingRight) {
            facingRight = !facingRight;
            //rotate the sprite
            transform.Rotate(0f, 180f, 0f);
        }
    }

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;

    void SetCountLives()
    {
        if (GM.lives == 5)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            heart4.SetActive(true);
            heart5.SetActive(true);
        }

        if (GM.lives == 4)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            heart4.SetActive(true);
            heart5.SetActive(false);
        }

        if (GM.lives == 3)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            heart4.SetActive(false);
            heart5.SetActive(false);
        }

        if (GM.lives == 2)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(false);
            heart4.SetActive(false);
            heart5.SetActive(false);
        }

        if (GM.lives == 1)
        {
            heart1.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);
            heart4.SetActive(false);
            heart5.SetActive(false);
        }

    }
    void SetScoreText()
    {
        score_text.text = "SCORE: " + GM.score.ToString();

    }
    public Text coinsText;
    void SetCoinsText()
    {
        coinsText.text = "COINS: " + GM.coins.ToString();
    }
    void Restart() {
        GM.coins = 0;
        GM.lives -= 1;
        SetCountLives();
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GM.deaths += 1;
        

    }
    void Respawn() {
        //respawn from the same scene
        GM.lives -= 1;
        SetCountLives();
        speaker.PlayOneShot(whenHittedSound,1);

    }
    void playerGotHitRight() {
        //adding small force to the left
        rb.AddForce(new Vector2(-30, 5), ForceMode2D.Impulse);
    }
    void playerGotHitLeft()
    {
        //adding small force to the right
        rb.AddForce(new Vector2(30, 5), ForceMode2D.Impulse);
    }

    public IEnumerator Wait()
    {
        playerIsHit = true;
        timeoutActive = true;
        yield return new WaitForSeconds(1);
        invincible = false;
        timeoutActive = false;
        playerIsHit = false;

    }

}
