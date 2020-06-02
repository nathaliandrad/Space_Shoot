using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{

    public static Vector2 WindDirection;
    void Start()
    {
        WindDirection = new Vector2(3, 0);
    }

}
