using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWon : MonoBehaviour
{
    public int nextScene;

    public AudioSource speaker;
    public Text deathsText;
    public Text coinsText;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;

    }

    // Update is called once per frame
    void Update()
    {

        SetCoinText();
        SetDeathsText();
        SetScoreText();

        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.P))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
     

    }

    void SetCoinText()
    {
        coinsText.text = "COINS: " + GM.coins.ToString();

    }
    void SetDeathsText()
    {
        deathsText.text = "DEATHS: " + GM.deaths.ToString();

    }
    void SetScoreText()
    {
        scoreText.text = "SCORE: " + GM.score.ToString();

    }

}
