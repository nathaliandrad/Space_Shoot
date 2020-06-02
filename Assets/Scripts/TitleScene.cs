using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public int nextScene;

    public AudioSource speaker;

    // Start is called before the first frame update
    void Start()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;

    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.P))
        {
            SceneManager.LoadScene(2);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.I)) {
            SceneManager.LoadScene(1);
        }

    }
}
