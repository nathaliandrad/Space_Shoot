using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Animation_Controller : MonoBehaviour
{
    public UnityEvent OnLandEvent; 
    // Start is called before the first frame update
    void Awake()
    {
        if(OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
    }
}
