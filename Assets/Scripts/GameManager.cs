using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void EndGame()
    {
            Restart();
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Update()
    {
        if (GM.coins == 30)
        {
            GM.lives++;
        }
        if (GM.coins == 70)
        {
            GM.lives++;
        }
        if(GM.coins == 100)
        {
            GM.lives++;
        }
    }
}