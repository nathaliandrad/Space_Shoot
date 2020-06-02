using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Portal : MonoBehaviour
{
    public int nextScene;
    public Transform player;
    public float apperPortal;
   
    // Start is called before the first frame update
    void Start()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        apperPortal = 10;
        
    }

    // Update is called once per frame
    void Update()
    {

        //if (Vector2.Distance(transform.position, player.position) < apperPortal) {
        //    Debug.Log("distance from portal starts here");
        //}


    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            NextLevel();
        }
    }
    public void NextLevel() {

        SceneManager.LoadScene(nextScene);
    }

}
